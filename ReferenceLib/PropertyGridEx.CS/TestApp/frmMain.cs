using System.Diagnostics;
using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using Microsoft.VisualBasic;
using System.Data;
using System.Collections.Generic;
using PropertyGridEx;
using System.Xml;
using System.IO;

namespace TestApp
{
    public partial class frmMain
    {
        protected XmlDataDocument document = null;
        protected int iSelectedRow = 0;
        protected int iCountRow = 0;

        public enum FilterPropertyType
        {
            None,
            FilterXmlSerializer,
            FilterBinaryFormatter
        }

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(System.Object sender, System.EventArgs e)
        {
            // Remove the Property Pages button
            Properties.ToolStrip.Items.RemoveAt(4);

            // Run the first example
            ButtonExample1_Click(this, null);
        }

        private void frmMain_Shown(object sender, System.EventArgs e)
        {
            // Apply style to DocComment object
            ApplyCommentsStyle();
        }

        private void ApplyCommentsStyle()
        {
            // Apply new style to DocComment
            Properties.DocCommentTitle.Font = new Font("Tahoma", 14, FontStyle.Bold);
            Properties.DocCommentDescription.Location = new Point(3, (5 + Properties.DocCommentTitle.Font.Height));
        }

        #region "PropertyGrid ToolStripButton Events"

        private void ButtonExample1_Click(object sender, EventArgs e)
        {
            try
            {
                // Select the first example page
                ButtonSelection(1);

                // Load the propertygrid
                FillPropertyGrid1(FilterPropertyType.None);

                // Update the status bar
                StatusLabel.Text = ButtonExample1.Text + " [ OK ]";
            }
            catch (Exception ex)
            {
                StatusLabel.Image = SystemIcons.Error.ToBitmap();
                StatusLabel.Text = ButtonExample1.Text + " [ FAILED ]";
                string text = ex.Message.ToString() + "\n" + ex.StackTrace.ToString();
                Interaction.MsgBox(text, MsgBoxStyle.Critical, null);
            }
        }

        private void ButtonExample2_Click(object sender, EventArgs e)
        {
            try
            {
                // Select the second example page
                ButtonSelection(2);

                // Load the propertygrid
                FillPropertyGrid2();

                // Update the status bar
                StatusLabel.Text = ButtonExample2.Text + " [ OK ]";
            }
            catch (Exception ex)
            {
                StatusLabel.Image = SystemIcons.Error.ToBitmap();
                StatusLabel.Text = ButtonExample2.Text + " [ FAILED ]";
                Interaction.MsgBox(ex.Message, MsgBoxStyle.Critical, null);
            }
        }

        private void ButtonExample3_Click(object sender, EventArgs e)
        {
            try
            {
                // Select the third example page
                ButtonSelection(3);

                // Load the propertygrid
                FillPropertyGrid3();

                // Update the status bar
                StatusLabel.Text = ButtonExample3.Text + " [ OK ]";
            }
            catch (Exception ex)
            {
                StatusLabel.Image = SystemIcons.Error.ToBitmap();
                StatusLabel.Text = ButtonExample3.Text + " [ FAILED ]";
                Interaction.MsgBox(ex.Message, MsgBoxStyle.Critical, null);
            }
        }

        private void ButtonExample4_Click(object sender, EventArgs e)
        {
            try
            {
                // Select the third example page
                ButtonSelection(4);

                // Load the propertygrid
                iSelectedRow = 0;
                FillPropertyGrid4();

                // Update the status bar
                StatusLabel.Text = ButtonExample4.Text + " [ OK ]";
            }
            catch (Exception ex)
            {
                StatusLabel.Image = SystemIcons.Error.ToBitmap();
                StatusLabel.Text = ButtonExample4.Text + " [ FAILED ]";
                Interaction.MsgBox(ex.Message, MsgBoxStyle.Critical, null);
            }
        }

        private void ButtonSerialize_Click(System.Object sender, System.EventArgs e)
        {
            ButtonSerialize.ShowDropDown();
        }

        private void UsingXmlSerializerToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                string filename = (new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase()).Info.DirectoryPath + "\\Items.xml";

                // Select the third example page
                ButtonSerialize.Text = UsingXmlSerializerToolStripMenuItem.Text;
                ButtonSelection(5);

                Properties.Item.Clear();
                if (!Properties.Item.LoadXml(filename))
                {
                    FillPropertyGrid1(FilterPropertyType.FilterXmlSerializer);
                    Properties.Item.SaveXml(filename);
                }
                Properties.Refresh();

                // Update the status bar
                StatusLabel.Text = "Load Items " + ButtonSerialize.Text + " [ OK ]";
            }
            catch (Exception ex)
            {
                StatusLabel.Image = SystemIcons.Error.ToBitmap();
                StatusLabel.Text = "Load Items " + ButtonSerialize.Text + " [ FAILED ]";
                Interaction.MsgBox(ex.Message, MsgBoxStyle.Critical, null);
            }
        }

        private void UsingBinaryFormatterToolStripMenuItem_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                string filename = (new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase()).Info.DirectoryPath + "\\Items.dat";

                // Select the third example page
                ButtonSerialize.Text = UsingBinaryFormatterToolStripMenuItem.Text;
                ButtonSelection(5);

                // Load the propertygrid
                Properties.Item.Clear();
                if (!Properties.Item.LoadBinary(filename))
                {
                    FillPropertyGrid1(FilterPropertyType.FilterBinaryFormatter);
                    Properties.Item.SaveBinary(filename);
                }
                Properties.Refresh();

                // Update the status bar
                StatusLabel.Text = "Load Items " + ButtonSerialize.Text + " [ OK ]";
            }
            catch (Exception ex)
            {
                StatusLabel.Image = SystemIcons.Error.ToBitmap();
                StatusLabel.Text = "Load Items " + ButtonSerialize.Text + " [ FAILED ]";
                Interaction.MsgBox(ex.Message, MsgBoxStyle.Critical, null);
            }
        }

        private void ButtonNext_Click(object sender, EventArgs e)
        {

            DataRow Row = null;

            if (document != null)
            {
                DataRowCollection Rows;
                Rows = document.DataSet.Tables[1].Rows;

                iSelectedRow++;
                if (iSelectedRow == Rows.Count - 1)
                {
                    ButtonNext.Enabled = false;
                }
                if (ButtonPrevious.Enabled == false)
                {
                    ButtonPrevious.Enabled = true;
                }

                Row = Rows[iSelectedRow];

                foreach (CustomProperty CustomProp in new ArrayList(Properties.Item))
                {
                    if (CustomProp.Category == "Dynamic view of a DataTable")
                    {
                        Properties.Item.Remove(CustomProp.Name);
                    }
                }

                foreach (DataColumn Column in document.DataSet.Tables[1].Columns)
                {
                    object oRow = Row;
                    Properties.Item.Add(Column.ColumnName.ToString(), ref oRow, Column.ColumnName.ToString(), false, "Dynamic view of a DataTable", "", true);
                }
                Properties.Refresh();
            }

        }

        private void ButtonPrevious_Click(object sender, EventArgs e)
        {
            DataRow Row = null;

            if (document != null)
            {
                DataRowCollection Rows;
                Rows = document.DataSet.Tables[1].Rows;
                iSelectedRow--;
                if (iSelectedRow == 0)
                {
                    ButtonPrevious.Enabled = false;
                }
                if (ButtonNext.Enabled == false)
                {
                    ButtonNext.Enabled = true;
                }

                Row = Rows[iSelectedRow];

                foreach (CustomProperty CustomProp in new ArrayList(Properties.Item))
                {
                    if (CustomProp.Category == "Dynamic view of a DataTable")
                    {
                        Properties.Item.Remove(CustomProp.Name);
                    }
                }

                foreach (DataColumn Column in document.DataSet.Tables[1].Columns)
                {
                    object oRow = Row;
                    Properties.Item.Add(Column.ColumnName.ToString(), ref oRow, Column.ColumnName.ToString(), false, "Dynamic view of a DataTable", "", true);
                }
                Properties.Refresh();
            }

        }

        #endregion

        private void ButtonSelection(int iState)
        {

            // This routine plays with the Toolbar of the PropertyGrid
            switch (iState)
            {
                case 1: // Single property Page

                    ButtonExample1.Checked = true;
                    ButtonExample1.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                    Properties.ContextMenuStrip = null;
                    ButtonExample2.DisplayStyle = ToolStripItemDisplayStyle.Image;
                    ButtonExample2.Checked = false;
                    ButtonExample3.DisplayStyle = ToolStripItemDisplayStyle.Image;
                    ButtonExample3.Checked = false;
                    ButtonExample4.DisplayStyle = ToolStripItemDisplayStyle.Image;
                    ButtonExample4.Checked = false;
                    ToolStripSeparator1.Visible = false;
                    ButtonNext.Visible = false;
                    ButtonPrevious.Visible = false;
                    ButtonSerialize.DisplayStyle = ToolStripItemDisplayStyle.Image;
                    break;

                case 2: // Multi object property Page

                    ButtonExample2.Checked = true;
                    ButtonExample2.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                    Properties.ContextMenuStrip = null;
                    ButtonExample1.DisplayStyle = ToolStripItemDisplayStyle.Image;
                    ButtonExample1.Checked = false;
                    ButtonExample3.DisplayStyle = ToolStripItemDisplayStyle.Image;
                    ButtonExample3.Checked = false;
                    ButtonExample4.DisplayStyle = ToolStripItemDisplayStyle.Image;
                    ButtonExample4.Checked = false;
                    ToolStripSeparator1.Visible = false;
                    ButtonNext.Visible = false;
                    ButtonPrevious.Visible = false;
                    ButtonSerialize.DisplayStyle = ToolStripItemDisplayStyle.Image;
                    break;


                case 3: // Databinding

                    ButtonExample3.Checked = true;
                    ButtonExample3.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                    Properties.ContextMenuStrip = null;
                    ButtonExample1.DisplayStyle = ToolStripItemDisplayStyle.Image;
                    ButtonExample1.Checked = false;
                    ButtonExample2.DisplayStyle = ToolStripItemDisplayStyle.Image;
                    ButtonExample2.Checked = false;
                    ButtonExample4.DisplayStyle = ToolStripItemDisplayStyle.Image;
                    ButtonExample4.Checked = false;
                    ToolStripSeparator1.Visible = false;
                    ButtonNext.Visible = false;
                    ButtonPrevious.Visible = false;
                    ButtonSerialize.DisplayStyle = ToolStripItemDisplayStyle.Image;
                    break;


                case 4: // XML Sample

                    ButtonExample4.Checked = true;
                    ButtonExample4.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                    ToolStripSeparator1.Visible = true;
                    ButtonNext.Visible = true;
                    ButtonPrevious.Visible = true;
                    ButtonPrevious.Enabled = false;
                    ButtonNext.Enabled = true;
                    Properties.ContextMenuStrip = ContextMenuSaveBooks;

                    ButtonExample1.DisplayStyle = ToolStripItemDisplayStyle.Image;
                    ButtonExample1.Checked = false;
                    ButtonExample2.DisplayStyle = ToolStripItemDisplayStyle.Image;
                    ButtonExample2.Checked = false;
                    ButtonExample3.DisplayStyle = ToolStripItemDisplayStyle.Image;
                    ButtonExample3.Checked = false;
                    ButtonSerialize.DisplayStyle = ToolStripItemDisplayStyle.Image;
                    break;

                case 5:

                    ButtonSerialize.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                    Properties.ContextMenuStrip = ContextMenuSaveItems;
                    ButtonExample1.DisplayStyle = ToolStripItemDisplayStyle.Image;
                    ButtonExample1.Checked = false;
                    ButtonExample2.DisplayStyle = ToolStripItemDisplayStyle.Image;
                    ButtonExample2.Checked = false;
                    ButtonExample3.DisplayStyle = ToolStripItemDisplayStyle.Image;
                    ButtonExample3.Checked = false;
                    ButtonExample4.DisplayStyle = ToolStripItemDisplayStyle.Image;
                    ButtonExample4.Checked = false;
                    ToolStripSeparator1.Visible = false;
                    ButtonNext.Visible = false;
                    ButtonPrevious.Visible = false;
                    break;


            }
        }

        private void FillPropertyGrid1(FilterPropertyType filter)
        {
            string[] Languages = new string[] { "English", "Italian", "Spanish", "Dutch" };
            MyOwnClass[] ListValues = new MyOwnClass[] { new MyOwnClass("English", 0), new MyOwnClass("Italian", 1), new MyOwnClass("Spanish", 2), new MyOwnClass("Dutch", 3) };
            int[] Values = new int[] { 1, 2, 3, 4 };
            MyOwnClass oInstance = new MyOwnClass("String value", 0);

            // The variable filter is used in the "Serialization Example"
            // The filter remove from the grid the properties not correctly supported
            // or not supported at all.

            Properties.ShowCustomProperties = true;
            Properties.Item.Clear();

            // Simple properties
            Properties.Item.Add("My Integer", 100, false, "Simple properties", "This is an integer", true);
            Properties.Item.Add("My Double", 10.4, false, "Simple properties", "This is a double", true);
            Properties.Item.Add("My String", "My Value", false, "Simple properties", "This is a string", true);
            if (filter != FilterPropertyType.FilterXmlSerializer)
            {
                Properties.Item.Add("My Font", new Font("Arial", 9), false, "Simple properties", "This is a font class", true);
                Properties.Item.Add("My Color", new Color(), false, "Simple properties", "This is a color class", true);
                Properties.Item.Add("My Point", new Point(10, 10), false, "Simple properties", "This is point class", true);
            }
            Properties.Item.Add("My Date", new DateTime(DateAndTime.Today.Ticks), true, "Simple properties", "This is date class", true);
            Properties.Item.Add("My Enum", MyEnum.FirstEntry, false, "Simple properties", "Work with Enum too!", true);

            // IsPassword attribute
            Properties.Item.Add("My Password", "password", false, ".NET v2.0 only", "This is a masked string." + "\r\n" + "(This feature is available only under .NET v2.0)", true);
            Properties.Item[Properties.Item.Count - 1].IsPassword = true;

            // Filename editor
            Properties.Item.Add("Filename", "", false, "Properties with custom UITypeEditor", "This property is a filename path. It define a custom UITypeConverter that show a OpenFileDialog or a SaveFileDialog when the user press the 3 dots button to edit the value.", true);
            Properties.Item[Properties.Item.Count - 1].UseFileNameEditor = true;
            Properties.Item[Properties.Item.Count - 1].FileNameDialogType = UIFilenameEditor.FileDialogType.LoadFileDialog;
            Properties.Item[Properties.Item.Count - 1].FileNameFilter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (filter != FilterPropertyType.FilterBinaryFormatter && filter != FilterPropertyType.FilterXmlSerializer)
            {
                // Custom Editor
                Properties.Item.Add("My Custom Editor", "", false, "Properties with custom UITypeEditor", "The component accept custom UITypeEditor.", true);
                Properties.Item[Properties.Item.Count - 1].CustomEditor = new MyEditor();

                // Custom Event Editor
                Properties.Item.Add("My Custom Event", "Click me", false, "Properties with custom UITypeEditor", "The component accept custom UITypeEditor.", true);
                Properties.Item[Properties.Item.Count - 1].OnClick += this.CustomEventItem_OnClick;

                // Custom TypeConverter
                Properties.Item.Add("Integer", 1, false, "Properties with custom TypeConverter", "This property have a custom type converter that show a custom error message.", true);
                Properties.Item[Properties.Item.Count - 1].CustomTypeConverter = new MyTypeConverter();
            }

            // Custom Choices Type Converter
            if (filter != FilterPropertyType.FilterXmlSerializer)
            {
                Properties.Item.Add("Language", "", false, "Properties with custom TypeConverter", "This property uses a TypeConverter to dropdown a list of values.", true);
                Properties.Item[Properties.Item.Count - 1].Choices = new CustomChoices(Languages, true);

                Properties.Item.Add("Values", 1, false, "Properties with custom TypeConverter", "This property uses a TypeConverter to dropdown a list of values.", true);
                Properties.Item[Properties.Item.Count - 1].Choices = new CustomChoices(Values, false);
            }

            if (filter != FilterPropertyType.FilterBinaryFormatter && filter != FilterPropertyType.FilterXmlSerializer)
            {
                // Expandable Type Converter			
                Properties.Item.Add("My object", oInstance, false, "Properties with custom TypeConverter", "This property make a \'MyOwnClass\' instance browsable.", true);
                Properties.Item[Properties.Item.Count - 1].IsBrowsable = true;
                Properties.Item[Properties.Item.Count - 1].BrowsableLabelStyle = BrowsableTypeConverter.LabelStyle.lsEllipsis;
            }

            // Dynamic properties
            if (filter != FilterPropertyType.FilterBinaryFormatter && filter != FilterPropertyType.FilterXmlSerializer)
            {
                object grid = Properties;
                Properties.Item.Add("Autosize properties", ref grid, "AutoSizeProperties", false, "Dynamic Properties", "This is a dynamic bound property. It changes the autosize property of this grid. Try it!", true);
                Properties.Item.Add("Draw flat toolbar", ref grid, "DrawFlatToolbar", false, "Dynamic Properties", "This is a dynamic bound property. It changes the DrawFlatToolbar property of this grid. Try it!", true);

                object form = this;
                Properties.Item.Add("Form opacity", ref form, "Opacity", false, "Dynamic Properties", "This is a dynamic bound property. It changes the Opacity property of this form. Try it!", true);
                Properties.Item[Properties.Item.Count - 1].IsPercentage = true;

                // PropertyGridEx
                Properties.Item.Add("Item", ref grid, "Item", false, "PropertyGridEx", "Represent the PropertyGridEx Item collection.",true);
                Properties.Item[Properties.Item.Count - 1].Parenthesize = true;

                Properties.Item.Add("DocComment", ref grid, "DocComment", false, "PropertyGridEx", "Represent the DocComment usercontrol of the PropertyGrid.", true);
                Properties.Item[Properties.Item.Count - 1].IsBrowsable = true;

                Properties.Item.Add("Image", ref grid, "DocCommentImage", false, "PropertyGridEx", "Represent the DocComment usercontrol of the PropertyGrid.", true);
                Properties.Item[Properties.Item.Count - 1].DefaultValue = null;
                Properties.Item[Properties.Item.Count - 1].DefaultType = typeof(Image);

                Properties.Item.Add("Toolstrip", ref grid, "Toolstrip", false, "PropertyGridEx", "Represent the toolstrip of the PropertyGrid.", true);
                Properties.Item[Properties.Item.Count - 1].IsBrowsable = true;

            }
            if (filter == FilterPropertyType.FilterBinaryFormatter)
            {

                // Databinding works with serialization
                Properties.Item.Add("Array of objects", ListValues[2].Text, false, "Databinding", "This is a UITypeEditor that implement a listbox", true);
                Properties.Item[Properties.Item.Count - 1].ValueMember = "Value";
                Properties.Item[Properties.Item.Count - 1].DisplayMember = "Text";
                Properties.Item[Properties.Item.Count - 1].Datasource = ListValues;
            }

            Properties.Refresh();

        }

        private object CustomEventItem_OnClick(object sender, EventArgs e)
        {
            CustomProperty prop = (CustomProperty)((CustomProperty.CustomPropertyDescriptor)sender).CustomProperty;
            Interaction.MsgBox("You clicked on property \'" + prop.Name + "\'", MsgBoxStyle.Information, "Custom Events as UITypeEditor");
            return "Click me again";
        }

        private void FillPropertyGrid2()
        {

            Properties.ShowCustomPropertiesSet = true;
            Properties.ItemSet.Clear();

            Properties.ItemSet.Add();
            Properties.ItemSet[0].Add("My Point", new Point(10, 10), false, "Appearance", "This is a font class", true);
            Properties.ItemSet[0].Add("My Date", new DateTime(2006, 1, 1), false, "Appearance", "This is a datetime", true);

            Properties.ItemSet.Add();
            Properties.ItemSet[1].Add("My Point", new Point(10, 10), false, "Appearance", "This is a font class", true);
            Properties.ItemSet[1].Add("My Date", new DateTime(2007, 1, 1), false, "Appearance", "This is a datetime", true);
            Properties.ItemSet[1].Add("My Color", new Color(), false, "Appearance", "This is a color class", true);

            Properties.Refresh();
        }

        private void FillPropertyGrid3()
        {
            string[] Languages = new string[] { "English", "Italian", "Spanish", "Dutch" };
            MyOwnClass[] ListValues = new MyOwnClass[] { new MyOwnClass("English", 0), new MyOwnClass("Italian", 1), new MyOwnClass("Spanish", 2), new MyOwnClass("Dutch", 3) };

            DataTable LookupTable = null;
            bool IsXmlSampleLoaded = false;
            document = new XmlDataDocument();

            // Load a DataTable from XML
            ParseSchema(ref document, (new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase()).Info.DirectoryPath + "\\books.xsd");
            try
            {
                document.Load((new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase()).Info.DirectoryPath + "\\books.xml");
                if (document.DataSet != null)
                {
                    LookupTable = document.DataSet.Tables[1];
                }
                if (LookupTable != null)
                {
                    iCountRow = LookupTable.Rows.Count;
                    IsXmlSampleLoaded = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception during XML Load: " + ex.Message);
                IsXmlSampleLoaded = false;
            }

            Properties.ShowCustomProperties = true;
            Properties.Item.Clear();

            Properties.Item.Add("Array of objects", ListValues[2].Text, false, "Databinding", "This is a UITypeEditor that implement a listbox", true);
            Properties.Item[Properties.Item.Count - 1].ValueMember = "Value";
            Properties.Item[Properties.Item.Count - 1].DisplayMember = "Text";
            Properties.Item[Properties.Item.Count - 1].Datasource = ListValues;

            Properties.Item.Add("Array of strings", Languages[1], false, "Databinding", "This is a UITypeEditor that implement a listbox", true);
            Properties.Item[Properties.Item.Count - 1].Datasource = Languages;
            Properties.Item[Properties.Item.Count - 1].IsDropdownResizable = true;

            // If the XML Samples was loaded
            if (IsXmlSampleLoaded)
            {

                // Bind a property to a DataTable
                Properties.Item.Add("Datatable", "", false, "Databinding", "This is a UITypeEditor that implement a listbox", true);
                Properties.Item[Properties.Item.Count - 1].ValueMember = "book_Id";
                Properties.Item[Properties.Item.Count - 1].DisplayMember = "title";
                Properties.Item[Properties.Item.Count - 1].Datasource = LookupTable;
            }

            Properties.MoveSplitterTo(120);
            Properties.Refresh();
        }

        private void FillPropertyGrid4()
        {
            DataTable LookupTable = null;
            DataRow Row = null;
            bool IsXmlSampleLoaded = false;
            document = new XmlDataDocument();

            // Load a DataTable from XML
            ParseSchema(ref document, (new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase()).Info.DirectoryPath + "\\books.xsd");
            try
            {
                document.Load((new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase()).Info.DirectoryPath + "\\books.xml");
                if (document.DataSet != null)
                {
                    LookupTable = document.DataSet.Tables[1];
                }
                if (LookupTable != null)
                {
                    IsXmlSampleLoaded = true;
                    iCountRow = LookupTable.Rows.Count;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception during XML Load: " + ex.Message);
                IsXmlSampleLoaded = false;
            }

            Properties.ShowCustomProperties = true;
            Properties.Item.Clear();

            // If the XML Samples was loaded
            if (IsXmlSampleLoaded)
            {
                // Get a row for demo purposes
                Row = LookupTable.Rows[iSelectedRow];

                // Bind the row to the grid (create a property for each column)
                foreach (DataColumn Column in LookupTable.Columns)
                {
                    object oRow = Row;
                    Properties.Item.Add(Column.ColumnName.ToString(), ref oRow, Column.ColumnName.ToString(), false, "Dynamic view of a DataTable", "", true);
                }
            }

            Properties.MoveSplitterTo(120);
            Properties.Refresh();

        }


        public void ParseSchema(ref XmlDataDocument document, string schema)
        {
            StreamReader myStreamReader = null;
            try
            {
                myStreamReader = new StreamReader(schema);
                document.DataSet.ReadXmlSchema(myStreamReader);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception during XSD Parsing: " + e.Message);
            }
            finally
            {
                if (myStreamReader != null)
                {
                    myStreamReader.Close();
                }
            }
        }

        private void Properties_PropertyValueChanged(System.Object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
        {

            string message;
            StatusLabel.Image = SystemIcons.Information.ToBitmap();

            switch (e.ChangedItem.PropertyDescriptor.GetType().Name)
            {
                case "CustomPropertyDescriptor":

                    CustomProperty.CustomPropertyDescriptor cpd = e.ChangedItem.PropertyDescriptor as CustomProperty.CustomPropertyDescriptor;
                    if (cpd != null)
                    {
                        CustomProperty cp = (PropertyGridEx.CustomProperty)cpd.CustomProperty;
                        if (cp == null)
                        {
                            return;
                        }
                        if (cp.Value != null)
                        {
                            message = " Value: " + cp.Value.ToString();
                            if (e.OldValue != null)
                            {
                                message = message + "; Previous: " + e.OldValue.ToString();
                            }
                            if (cp.SelectedItem != null)
                            {
                                message = message + "; SelectedItem: " + cp.SelectedItem.ToString();
                            }
                            if (cp.SelectedValue != null)
                            {
                                message = message + "; SelectedValue: " + cp.SelectedValue.ToString();
                            }
                            StatusLabel.Text = message;
                        }
                    }
                    break;

                case "MergePropertyDescriptor":


                    message = " {MultiProperty [" + e.ChangedItem.Label + "]} " + e.ChangedItem.Value.ToString();
                    if (e.OldValue == null)
                    {
                        message = message + "; Nothing";
                    }
                    else
                    {

                        message = message + "; " + e.OldValue.ToString();
                    }
                    StatusLabel.Text = message;
                    break;

                case "ReflectPropertyDescriptor":

                    message = " {NestedProperty [" + e.ChangedItem.Label + "]} " + e.ChangedItem.Value.ToString();
                    if (e.OldValue == null)
                    {
                        message = message + "; Nothing";
                    }
                    else
                    {

                        message = message + "; " + e.OldValue.ToString();
                    }
                    StatusLabel.Text = message;
                    break;


                default:

                    StatusLabel.Image = SystemIcons.Error.ToBitmap();
                    StatusLabel.Text = " {Unknown PropertyDescriptor}";
                    break;
            }
        }

        private void SaveBooksXml_Click(System.Object sender, System.EventArgs e)
        {
            document.Save((new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase()).Info.DirectoryPath + "\\books.xml");
        }

        private void SaveItems_Click(System.Object sender, System.EventArgs e)
        {
            if (ButtonSerialize.Text == UsingXmlSerializerToolStripMenuItem.Text)
            {
                string filename = (new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase()).Info.DirectoryPath + "\\Items.xml";
                Properties.Item.SaveXml(filename);
            }
            if (ButtonSerialize.Text == UsingBinaryFormatterToolStripMenuItem.Text)
            {
                string filename = (new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase()).Info.DirectoryPath + "\\Items.dat";
                Properties.Item.SaveBinary(filename);
            }
        }

    }

}
