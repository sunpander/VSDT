using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
 
namespace VSDT.WinPlatformMS.UI
{
    public class BaseDock : DockContent
    {
        private IContainer components = null;

        public BaseDock()
        {
            this.InitializeComponent();
        }

        private void BaseForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                this.FormOnLoad();
            }
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {
            if (!base.DesignMode)
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    this.FormOnLoad();
                }
                catch (Exception exception)
                {
                    this.ProcessException(exception);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public virtual void FormOnLoad()
        {
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BaseDock
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "BaseDock";
            this.Load += new System.EventHandler(this.BaseDock_Load);
            this.ResumeLayout(false);

        }

        protected override void OnLoad(EventArgs e)
        {
            if (!base.DesignMode)
            {
                ComponentResourceManager manager = new ComponentResourceManager(typeof(BaseDock));
                //base.Icon = Resources.app;
                base.StartPosition = FormStartPosition.CenterScreen;
                base.OnLoad(e);
            }
        }

        public void ProcessException(Exception ex)
        {
            this.WriteException(ex);
            MessageBox.Show(ex.Message);
        }

        public void WriteException(Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }

        private void BaseDock_Load(object sender, EventArgs e)
        {

        }
    }
}

