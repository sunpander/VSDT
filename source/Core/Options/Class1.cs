using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace VSDT.Options
{
    public abstract class AdminOptionsBase
    {
        protected AdminOptionsBase() { }

        protected abstract UserControl OnCreatePropertyPage();
        protected abstract string OnGetFriendlyName();
        protected abstract string OnGetRootXMLNodeName();
        protected abstract void OnLoad(XmlNode xmlParentNode);
        protected abstract bool OnSave(XmlTextWriter xmlTextWriter);
    }
    public interface IAdminOptionsPropertyPage
    {
        void Cleanup();
        void Initialize(AdminOptionsBase adminOptions, bool readOnlyState);
        void SetOptions(AdminOptionsBase adminOptions, ref bool cancel, ref string cancellationReason);
    }
    public abstract class OperationBase
    {
        public OperationBase() { }

 
        //protected bool AddNodeToResultsTree(string resultLineText, object customValue);
        ////protected bool AddNodeToResultsTree(EnvDTE.Project project, string resultLineText, object customValue);
        ////protected bool AddNodeToResultsTree(EnvDTE.ProjectItem projectItem, string resultLineText, object customValue);
        ////protected bool AddNodeToResultsTree(EnvDTE.ProjectItem projectItem, EnvDTE.CodeElement leafCodeElement, string resultLineText, object customValue);
        ////protected bool AddNodeToResultsTree(EnvDTE.ProjectItem projectItem, IComponent component, string resultLineText, object customValue);
        ////protected bool AddNodeToResultsTree(EnvDTE.ProjectItem projectItem, EnvDTE.CodeElement leafCodeElement, string resultLineText, string foundText, int startLineNumber, int startOffset, int endLineNumber, int endOffset, object customValue);
        //protected virtual void OnAfterExecuteOperation(AdminOptionsBase adminOptions, UserOptions userOptions);
        ////protected virtual void OnAfterExecuteOperationInProject(AdminOptionsBase adminOptions, UserOptions userOptions, EnvDTE.Project project, ref bool cancel);
        //protected virtual void OnAfterExecuteOperationInSolution(AdminOptionsBase adminOptions, UserOptions userOptions, ref bool cancel);
        ////protected virtual void OnAfterLocateCodeElementResult(AdminOptionsBase adminOptions, UserOptions userOptions, EnvDTE.CodeElement codeElement, object customValue);
        ////protected virtual void OnAfterLocateComponentResult(AdminOptionsBase adminOptions, UserOptions userOptions, IComponent component, object customValue);
        ////protected virtual void OnAfterLocateFoundTextResult(AdminOptionsBase adminOptions, UserOptions userOptions, EnvDTE.TextDocument textDocument, string foundText, int startLineNumber, int startOfftset, int endLineNumber, int endOffset, object customValue);
        ////protected virtual void OnAfterLocateProjectItemResult(AdminOptionsBase adminOptions, UserOptions userOptions, EnvDTE.ProjectItem projectItem, object customValue);
        ////protected virtual void OnAfterLocateProjectResult(AdminOptionsBase adminOptions, UserOptions userOptions, EnvDTE.Project project, object customValue);
        //protected virtual void OnAfterLocateSolutionResult(AdminOptionsBase adminOptions, UserOptions userOptions, object customValue);
        //protected virtual void OnBeforeExecuteOperation(AdminOptionsBase adminOptions, UserOptions userOptions, ref bool cancel);
        ////protected virtual void OnBeforeExecuteOperationInProject(AdminOptionsBase adminOptions, UserOptions userOptions, EnvDTE.Project project, ref bool cancel);
        //protected virtual void OnBeforeExecuteOperationInSolution(AdminOptionsBase adminOptions, UserOptions userOptions, ref bool cancel);
        //protected virtual void OnCancelOperation(AdminOptionsBase adminOptions, UserOptions userOptions);
        //protected virtual void OnCleanup();
        //protected virtual AdminOptionsBase OnCreateAdminOptions();
        //protected virtual void OnCreateUserOptionsForExecution(AdminOptionsBase adminOptions, ref UserOptions userOptions, ref bool cancel);
        //protected virtual void OnCreateUserOptionsForExecutionBeforeBuild(AdminOptionsBase adminOptions, ref UserOptions userOptions, ref bool cancel);
        //protected virtual void OnExecuteOperationInFolderProjectItem(AdminOptionsBase adminOptions, UserOptions userOptions, EnvDTE.ProjectItem projectItem, ref bool cancel);
        //protected abstract void OnExecuteOperationInProjectItem(AdminOptionsBase adminOptions, UserOptions userOptions, EnvDTE.ProjectItem projectItem, EnvDTE.EditPoint startEditPoint, EnvDTE.EditPoint endEditPoint, IDesignerHost designerHost, ref bool cancel);
        //protected virtual void OnExecuteOperationInSolutionFolder(AdminOptionsBase adminOptions, UserOptions userOptions, EnvDTE.Project solutionFolder, ref bool cancel);
        //protected virtual void OnExecuteOperationInTextRange(AdminOptionsBase adminOptions, UserOptions userOptions, EnvDTE.ProjectItem projectItem, EnvDTE.EditPoint startEditPoint, EnvDTE.EditPoint endEditPoint, ref bool cancel);
        protected abstract void OnInitialize(OperationInfo operationInfo);
        //protected void RemoveSelectedNodeFromResultsTree();
    }

    public class OperationInfo
    {
        public string CommandCaption { get; set; }
        public int CommandImageIndex { get; set; }
        public string CommandName { get; set; }
        public string ShowResultsCommandName { get; set; }
        //public IDE TargetIDEs { get; set; }
        public ProjectItemKind TargetProjectItemKinds { get; set; }
        public bool UseMultipleResultTabs { get; set; }
    }

    public interface IUserOptionsPropertyPage
    {
        void Cleanup();
        void Initialize(OperationBase operation, AdminOptionsBase adminOptions, UserOptions userOptions);
        void SetOptions(UserOptions userOptions, ref bool cancel, ref string cancellationReason);
    }
    public class UserOptions
    {
        public UserOptions(OperationScopeAction operationScopeAction) { }

        public ModifiedDocumentAction ModifiedDocumentAction { get; set; }
      

        //protected virtual UserControl OnCreatePropertyPage();
        //protected virtual string OnGetFriendlyName();
        //protected virtual string OnGetOptionsDescription();
    }
    public enum ModifiedDocumentAction
    {
        KeepOpen = 1,
        CloseSaving = 2,
        CloseWithoutSaving = 3,
    }
    public enum ProjectItemKind
    {
        Other = 1,
        Designer = 2,
        SourceCode = 4,
        Text = 8,
    }
    public enum OperationScopeAction
    {
        UseSolutionScope = 1,
        AskUserExcludingProcedureAndSelectionScopes = 2,
        AskUserIncludingProcedureAndSelectionScopes = 3,
        AskUserIncludingProcedureScopeAndExcludingSelectionScope = 4,
    }
}
