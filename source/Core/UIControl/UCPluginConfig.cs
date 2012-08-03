using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VSDT.UIControl
{
    public partial class UCPluginConfig : UserControl
    {
        public UCPluginConfig()
        {
            InitializeComponent();
        }

        private void UCPlugInConfig_Load(object sender, EventArgs e)
        {
            RefreshPlugins();
        }

        private void RefreshPlugins()
        {
            try
            {
                List<PlugIns.IPlugIn> list = Framework.Inistace.PlugIns;

                DataTable dt = new DataTable();
                dt.Columns.Add("RunTimeState");
                dt.Columns.Add("SymbolicName");
                dt.Columns.Add("Description");
                string name = "";
                string state = "";
                string descript = "";
                foreach (var item in list)
                {
                    name = item.SymbolicName;
                    state = item.RunTimeState.ToString();
                    descript = item.PlugInInfo.PlugInData.PlugInInfo.Description;
                    dt.Rows.Add(state, name, descript);
                }

                this.gridViewPlugins.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridViewPlugins_SelectionChanged(object sender, EventArgs e)
        {
            if (1 == gridViewPlugins.SelectedRows.Count)
            {
                textBoxdescirpt.Text= gridViewPlugins.SelectedRows[0].Cells["Description"].Value.ToString();
            }
        }

        private void gridViewPlugins_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewPlugins.SelectedRows != null && gridViewPlugins.SelectedRows.Count == 1)
                {
                    string symName = gridViewPlugins.SelectedRows[0].Cells["SymbolicName"].ToString();
                    List<PlugIns.IPlugIn> list = Framework.Inistace.PlugIns;

                    foreach (var item in list)
                    {
                        if (item.SymbolicName == symName)
                        {
                            item.Start();
                            RefreshPlugins();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewPlugins.SelectedRows != null && gridViewPlugins.SelectedRows.Count == 1)
                {
                    string symName = gridViewPlugins.SelectedRows[0].Cells["SymbolicName"].Value.ToString();
                    List<PlugIns.IPlugIn> list = Framework.Inistace.PlugIns;

                    foreach (var item in list)
                    {
                        if (item.SymbolicName == symName)
                        {
                            item.Stop();
                            RefreshPlugins();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
}
