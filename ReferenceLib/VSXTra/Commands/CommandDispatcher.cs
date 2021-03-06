// ================================================================================================
// CommandDispatcher.cs
//
// Created: 2008.07.06, by Istvan Novak (DeepDiver)
// ================================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.Shell;
using VSXtra.Diagnostics;
using VSXtra.Package;
using VSXtra.Properties;

namespace VSXtra.Commands
{
  // ================================================================================================
  /// <summary>
  /// This interface marks a type as one that can provide command GUID for a type.
  /// </summary>
  // ================================================================================================
  public interface ICommandGuidProvider
  {
    // --------------------------------------------------------------------------------------------
    /// <summary>
    /// Gets the GUID for command event handlers which do not declare a GUID explicitly.
    /// </summary>
    // --------------------------------------------------------------------------------------------
    Guid CommandGuid { get; }  
  }

  // ================================================================================================
  /// <summary>
  /// This delegate represents menu handler methods.
  /// </summary>
  /// <param name="command">Command managed by the handler</param>
  // ================================================================================================
  public delegate void CommandHandlerMethod(OleMenuCommand command);

  // ================================================================================================
  /// <summary>
  /// This class is responsible to dispatch events received by IOleCommandTarget objects.
  /// </summary>
  // ================================================================================================
  public sealed class CommandDispatcher<TPackage>
    where TPackage: PackageBase
  {
    #region Private fields

    /// <summary>Cache for command target information</summary>
    private static readonly Dictionary<Type, CommandTargetInfo> _Targets =
      new Dictionary<Type, CommandTargetInfo>();

    /// <summary>Package hosting the command dispatcher</summary>
    private readonly TPackage _Package;

    #endregion

    #region Lifecycle methods

    // --------------------------------------------------------------------------------------------
    /// <summary>
    /// Creates a new instance of CommandDispatcher using the specified event target and the 
    /// optional command GUID provider.
    /// </summary>
    /// <param name="eventTarget">Event target instance.</param>
    /// <param name="guidProvider">GUID provider.</param>
    // --------------------------------------------------------------------------------------------
    public CommandDispatcher(object eventTarget, ICommandGuidProvider guidProvider)
    {
      if (eventTarget == null)
        throw new ArgumentNullException("eventTarget");
      EventTarget = eventTarget;
      GuidProvider = guidProvider;
      _Package = PackageBase.GetPackageInstance<TPackage>();
      ScanDispatchInfo();
    }

    // --------------------------------------------------------------------------------------------
    /// <summary>
    /// Creates a new instance of CommandDispatcher using the specified event target.
    /// </summary>
    /// <param name="eventTarget">Event target instance.</param>
    // --------------------------------------------------------------------------------------------
    public CommandDispatcher(object eventTarget) : 
      this(eventTarget, eventTarget as ICommandGuidProvider)
    {
    }

    // --------------------------------------------------------------------------------------------
    /// <summary>
    /// Creates a new instance of CommandDispatcher using the specified event target and 
    /// package instance
    /// </summary>
    /// <param name="eventTarget">Event target instance.</param>
    /// <param name="package">Package instance</param>
    // --------------------------------------------------------------------------------------------
    internal CommandDispatcher(object eventTarget, TPackage package) :
      this(eventTarget, eventTarget as ICommandGuidProvider)
    {
      _Package = package;
    }

    #endregion

    #region Public properties

    // --------------------------------------------------------------------------------------------
    /// <summary>
    /// Gets the event target object of this command dispatcher.
    /// </summary>
    /// <value>Event target object of this command dispatcher.</value>
    // --------------------------------------------------------------------------------------------
    public object EventTarget { get; private set; }

    // --------------------------------------------------------------------------------------------
    /// <summary>
    /// Gets the GUID provider of this command dispatcher.
    /// </summary>
    /// <value>Event target object of this command dispatcher.</value>
    // --------------------------------------------------------------------------------------------
    public ICommandGuidProvider GuidProvider { get; private set; }

    #endregion

    #region Public methods

    // --------------------------------------------------------------------------------------------
    /// <summary>
    /// Gets the OleMenuCommand instance registered for the specified command.
    /// </summary>
    /// <param name="commandId">ID of the command.</param>
    /// <returns>
    /// OleMenuCommand instance, if there is any event handler for the specified command;
    /// otherwise, null.
    /// </returns>
    // --------------------------------------------------------------------------------------------
    public MenuCommandInfo GetMenuCommandInfo(CommandID commandId)
    {
      var targetInfo = GetTargetInfo();
      return targetInfo == null ? null : targetInfo.FindMenuInfo(commandId);
    }

    // --------------------------------------------------------------------------------------------
    /// <summary>
    /// Registers command handlers with the appropriate OleMenuCommandService.
    /// </summary>
    /// <param name="local">Local OleMenuCommandService instance.</param>
    /// <param name="parent">Global OleMenuCommandService instance.</param>
    /// <remarks>
    /// Promoted command handlers are merged with the parent OleMenuService instance.
    /// </remarks>
    // --------------------------------------------------------------------------------------------
    public void RegisterCommandHandlers(OleMenuCommandService local, 
      OleMenuCommandService parent)
    {
      var targetInfo = GetTargetInfo();
      foreach (var menuGroup in targetInfo.Values)
      {
        foreach (var menuInfo in menuGroup.Values)
        {
          // --- Register the command with the local OleMenuCommandService
          var id = new CommandID(menuInfo.Guid, (int)menuInfo.Id);
          var command = new OleMenuCommand(
            ExecEventHandler,
            ChangeEventHandler,
            QueryStatusEventHandler,
            id);
          menuInfo.OleMenuCommand = command;
          local.AddCommand(command);

          // --- Promote to the parent OleMenuCommandService, if required.
          if (menuInfo.Promote && parent != null)
          {
            if (parent.FindCommand(id) == null)
              parent.AddCommand(command);
          }
        }
      }
    }

    // --------------------------------------------------------------------------------------------
    /// <summary>
    /// Gets the status value.
    /// </summary>
    /// <param name="result">The result returned by the IOleCommandTarget method</param>
    /// <param name="context">The command context</param>
    /// <returns>Status value calculated from the input parameters</returns>
    // --------------------------------------------------------------------------------------------
    public int GetStatusValue(int result, CommandContext context)
    {
      return context != null && context.ExplicitReturnStatusSet
               ? context.ReturnStatus
               : result;
    }

    // --------------------------------------------------------------------------------------------
    /// <summary>
    /// Gets and sets the current command context.
    /// </summary>
    // --------------------------------------------------------------------------------------------
    public CommandContext Context { get; set; }

    #endregion

    #region Private methods

    // --------------------------------------------------------------------------------------------
    /// <summary>
    /// Scans through all the methods of the EventTarget's type to collect information about 
    /// command handler methods.
    /// </summary>
    // --------------------------------------------------------------------------------------------
    private void ScanDispatchInfo()
    {
      Type targetType = EventTarget.GetType();
      lock (_Targets)
      {
        // --- We scan only the type info, if we have not scanned it yet.
        if (_Targets.ContainsKey(targetType)) return;

        var target = new CommandTargetInfo();

        // --- Obtain the default command Guid for the event target
        var defaultGuidAttr = targetType.GetAttribute<DefaultCommandGroupAttribute>();
        Guid defaultGuid = defaultGuidAttr == null
          ? (GuidProvider == null ? targetType.GUID : GuidProvider.CommandGuid)
          : defaultGuidAttr.Value.GUID;
        target.DefaultGuid = defaultGuid;

        // --- Obtain command mapping information
        var mappings = targetType.AttributesOfType<CommandMapAttribute>();
        foreach (var attr in mappings)
        {
          target.Mappings.Add(attr);
        }

        // --- Obtain all methods that can be used as command methods
        var commandMethods =
          from method in targetType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | 
            BindingFlags.Instance | BindingFlags.Static)
          where method.ReturnType == typeof (void) &&
                (
                  // --- void MyCommandExec()
                  method.GetParameters().Count() == 0 &&
                  Attribute.IsDefined(method, typeof(CommandExecMethodAttribute))
                  ||
                  // --- void MyCommandExec(CommandContext)
                  method.GetParameters().Count() == 1 &&
                  (
                    method.GetParameters()[0].ParameterType == typeof(CommandContext) ||
                    method.GetParameters()[0].ParameterType.IsSubclassOf(typeof(CommandContext))
                  ) &&
                  Attribute.IsDefined(method, typeof(CommandExecMethodAttribute))
                  ||
                  // --- void MyCommandMethod(OleMenuCommand)
                  method.GetParameters().Count() == 1 &&
                  method.GetParameters()[0].ParameterType == typeof (OleMenuCommand) &&
                  Attribute.IsDefined(method, typeof (CommandMethodAttribute))
                  ||
                  // --- void MyCommandMethod(OleMenuCommand, CommandExec)
                  method.GetParameters().Count() == 2 &&
                  method.GetParameters()[0].ParameterType == typeof(OleMenuCommand) &&
                  (
                    method.GetParameters()[1].ParameterType == typeof(CommandContext) ||
                    method.GetParameters()[1].ParameterType.IsSubclassOf(typeof(CommandContext))
                  ) &&
                  Attribute.IsDefined(method, typeof(CommandMethodAttribute))
                ) &&
                Attribute.IsDefined(method, typeof(CommandIdAttribute))
          select method;
        foreach (var method in commandMethods)
        {
          foreach (CommandIdAttribute idAttr in 
            method.GetCustomAttributes(typeof(CommandIdAttribute), false))
          {
            var menuInfo = ObtainCommandMethodAttributes(idAttr, method, defaultGuid);
            MergeCommandMethodInfo(target, menuInfo);
          }
        }
        _Targets.Add(targetType, target);
      }
    }

    // --------------------------------------------------------------------------------------------
    /// <summary>
    /// Merges menu information with the specified target information.
    /// </summary>
    /// <param name="target">Target to merge the menu into.</param>
    /// <param name="info">Menu command information to merge.</param>
    // --------------------------------------------------------------------------------------------
    private static void MergeCommandMethodInfo(CommandTargetInfo target, MenuCommandInfo info)
    {
      // --- Apply mappings
      foreach (var mapping in target.Mappings)
      {
        // --- Check for mapping GUID
        if ((mapping.Guid == Guid.Empty && info.Guid == target.DefaultGuid) ||
            mapping.Guid != info.Guid)
        {
          // --- Mapping GUID matches, check for ID range
          if (info.Id >= mapping.IdFrom && info.Id <= mapping.IdTo)
          {
            // --- ID in the mapping range, apply offset
            info.Id = (uint) ((int) info.Id + mapping.Offset);
            break;
          }
        }
      }

      // --- Actual merging starts here
      Dictionary<uint, MenuCommandInfo> commandsForGroup;
      if (!target.TryGetValue(info.Guid, out commandsForGroup))
      {
        // --- No item exists for the command group.
        commandsForGroup = new Dictionary<uint, MenuCommandInfo> {{info.Id, info}};
        target.Add(info.Guid, commandsForGroup);
        return;
      }
      MenuCommandInfo mergedInfo;
      if (!commandsForGroup.TryGetValue(info.Id, out mergedInfo))
      {
        // --- No item with info.id in the command group.
        commandsForGroup.Add(info.Id, info);
        return;
      }

      // --- Merge with the former items
      mergedInfo.Promote |= info.Promote;
      if (info.ChangeMethod != null)
      {
        if (mergedInfo.ChangeMethod == null)
          mergedInfo.ChangeMethod = info.ChangeMethod;
        else
          throw new InvalidOperationException(Resources.CommandDispatcher_DuplicateChange);
      }
      if (info.ExecMethod != null)
      {
        if (mergedInfo.ExecMethod == null)
        {
          mergedInfo.ExecMethod = info.ExecMethod;
          mergedInfo.Action = info.Action;
        }
        else
          throw new InvalidOperationException(Resources.CommandDispatcher_DuplicateExec);
      }
      if (info.QueryStatusMethod != null)
      {
        if (mergedInfo.QueryStatusMethod == null)
          mergedInfo.QueryStatusMethod = info.QueryStatusMethod;
        else
          throw new InvalidOperationException(Resources.CommandDispatcher_DuplicateStatus);
      }
    }

    // --------------------------------------------------------------------------------------------
    /// <summary>
    /// Obtains the menu information for a command handler method.
    /// </summary>
    /// <param name="idAttr">CommandID attribute belonging to the command.</param>
    /// <param name="method">Method information to scan for attributes.</param>
    /// <param name="defaultGuid">
    /// Default GUID to be used if not defined with the command handler.
    /// </param>
    /// <returns>MenuCommandInfo instance about the method.</returns>
    // --------------------------------------------------------------------------------------------
    private static MenuCommandInfo ObtainCommandMethodAttributes(CommandIdAttribute idAttr, MethodInfo method, Guid defaultGuid)
    {
      var menuInfo = new MenuCommandInfo();
      bool commandAttrFound = false;
      foreach (var attr in method.GetCustomAttributes(true))
      {
        // --- Check for command method attributes
        var cmdAttr = attr as CommandMethodAttribute;
        if (cmdAttr != null)
        {
          if (commandAttrFound)
          {
            VsDebug.Fail(String.Format("Only one CommandMethodAttribute is allowed for {0}.{1}", 
              method.DeclaringType.Name, method.Name));
            continue;
          }
          commandAttrFound = true;
          if (cmdAttr is CommandStatusMethodAttribute)
            menuInfo.QueryStatusMethod = method;
          else if (cmdAttr is CommandExecMethodAttribute)
            menuInfo.ExecMethod = method;
          else if (cmdAttr is CommandChangeMethodAttribute)
            menuInfo.ChangeMethod = method;
        }

        // --- Check command action
        var actionAttr = attr as ActionAttribute;
        if (actionAttr != null)
        {
          menuInfo.Action = actionAttr;  
        }

        // --- Check promote flag
        var promoteAttr = attr as PromoteAttribute;
        if (promoteAttr != null)
        {
          menuInfo.Promote = true;
        }

        // --- Set command ID and handle the defaultvalue of the command GUID
        menuInfo.Guid = idAttr.Guid == Guid.Empty
                          ? defaultGuid
                          : idAttr.Guid;
        menuInfo.Id = idAttr.Id;
      }
      return menuInfo;
    }

    // --------------------------------------------------------------------------------------------
    /// <summary>
    /// Delegate for mapping a MenuCommandInfo to a method tobe executed.
    /// </summary>
    // --------------------------------------------------------------------------------------------
    private delegate MethodInfo MethodMapper(MenuCommandInfo menuInfo);

    // --------------------------------------------------------------------------------------------
    /// <summary>
    /// Handles the change event of an OleMenuCommand.
    /// </summary>
    // --------------------------------------------------------------------------------------------
    private void ChangeEventHandler(object sender, EventArgs e)
    {
      HandleMenuCommandEvent(sender, info => info.ChangeMethod, false);
    }

    // --------------------------------------------------------------------------------------------
    /// <summary>
    /// Handles the status query event of an OleMenuCommand.
    /// </summary>
    // --------------------------------------------------------------------------------------------
    private void QueryStatusEventHandler(object sender, EventArgs e)
    {
      HandleMenuCommandEvent(sender, info => info.QueryStatusMethod, false);
    }

    // --------------------------------------------------------------------------------------------
    /// <summary>
    /// Handles the execute event of an OleMenuCommand.
    /// </summary>
    // --------------------------------------------------------------------------------------------
    private void ExecEventHandler(object sender, EventArgs e)
    {
      HandleMenuCommandEvent(sender, info => info.ExecMethod, true);
    }

    // --------------------------------------------------------------------------------------------
    /// <summary>
    /// Calls the appropriate event method.
    /// </summary>
    /// <param name="sender">Object initiating the event.</param>
    /// <param name="mapper">Mapper to define the method to execute</param>
    /// <param name="useAction">Allows using the menu command action</param>
    // --------------------------------------------------------------------------------------------
    private void HandleMenuCommandEvent(object sender, MethodMapper mapper, bool useAction)
    {
      // --- Check for command
      var command = sender as OleMenuCommand;
      if (command == null) return;

      // --- Obtain infiormation for command
      var targetInfo = GetTargetInfo();
      var menuInfo = targetInfo.FindMenuInfo(command.CommandID);
      if (menuInfo == null) return;

      // --- Get the apropriate commmand method
      MethodInfo commandMethod = mapper(menuInfo);
      if (commandMethod == null) return;

      // --- Execute the default command action
      if (useAction && menuInfo.Action != null)
      {
        menuInfo.Action.ExecuteAction(_Package, command.CommandID);
      }

      // --- Execute the command
      var paramCount = commandMethod.GetParameters().Count();
      var parameters = new object[paramCount];

      // --- Create a simple context if no one exists
      if (Context == null) Context = new CommandContext(_Package);
      if (paramCount > 0)
      {
        parameters[0] = 
          commandMethod.GetParameters()[0].ParameterType == typeof(OleMenuCommand)
            ? (object)command : Context;
      }
      if (paramCount == 2)
      {
        parameters[1] = Context;
      }
      commandMethod.Invoke(EventTarget, parameters);
    }

    // --------------------------------------------------------------------------------------------
    /// <summary>
    /// Retrieves the CommandTargetInfo belonging to the target type.
    /// </summary>
    // --------------------------------------------------------------------------------------------
    private CommandTargetInfo GetTargetInfo()
    {
      CommandTargetInfo targetInfo;
      if (!_Targets.TryGetValue(EventTarget.GetType(), out targetInfo))
      {
        throw new InvalidOperationException(
          (String.Format(Resources.CommandDispatcher_NoType, EventTarget.GetType())));
      }
      return targetInfo;
    }

    #endregion

    #region MenuCommandInfo class

    // ================================================================================================
    /// <summary>
    /// This class represents the information for a command handler
    /// </summary>
    // ================================================================================================
    public class MenuCommandInfo
    {
      public Guid Guid { get; internal set; }
      public uint Id { get; internal set; }
      public MethodInfo QueryStatusMethod { get; internal set; }
      public MethodInfo ExecMethod { get; internal set; }
      public MethodInfo ChangeMethod { get; internal set; }
      public ActionAttribute Action { get; internal set; }
      public bool Promote { get; internal set; }
      public OleMenuCommand OleMenuCommand { get; internal set; }
    }

    #endregion

    #region CommandTargetInfo

    // ================================================================================================
    /// <summary>
    /// This class is responsible for holding menu command information for a specific type.
    /// </summary>
    /// <remarks>
    /// Commands are held in a two-level hierarchy. First level is the command group level by
    /// the GUID of commands, second level is by the uint part of the commands within the group.
    /// </remarks>
    // ================================================================================================
    private class CommandTargetInfo : Dictionary<Guid, Dictionary<uint, MenuCommandInfo>>
    {
      private readonly List<CommandMapAttribute> _Mappings = new List<CommandMapAttribute>();
      private Guid _DefaultGuid = Guid.Empty;

      // --------------------------------------------------------------------------------------------
      /// <summary>
      /// Finds the command information in the dictionary.
      /// </summary>
      /// <param name="commandId">ID of command to find.</param>
      /// <returns>
      /// Menu command information if the command found; otherwise, null
      /// </returns>
      // --------------------------------------------------------------------------------------------
      public MenuCommandInfo FindMenuInfo(CommandID commandId)
      {
        // --- Find the command group
        Dictionary<uint, MenuCommandInfo> commandGroup;
        if (!TryGetValue(commandId.Guid, out commandGroup)) return null;

        // --- Find the command within the group
        MenuCommandInfo result;
        commandGroup.TryGetValue((uint) commandId.ID, out result);
        return result;
      }

      // --------------------------------------------------------------------------------------------
      /// <summary>
      /// Gets the list of mappings related to the menu command info.
      /// </summary>
      // --------------------------------------------------------------------------------------------
      public List<CommandMapAttribute> Mappings
      {
        get { return _Mappings; }
      }

      // --------------------------------------------------------------------------------------------
      /// <summary>
      /// Gets or sets the default Guid belonging to this menu command info.
      /// </summary>
      // --------------------------------------------------------------------------------------------
      public Guid DefaultGuid
      {
        get { return _DefaultGuid; }
        set { _DefaultGuid = value; }
      }
    }

    #endregion
  }
}