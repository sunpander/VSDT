using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Magnifier20070401
{
    public partial class ConfigurationForm : Form
    {
        public ConfigurationForm(Configuration configuration)
        {
            InitializeComponent();
            
            InitConfigutationSettings(configuration);
        }

        private void InitConfigutationSettings(Configuration configuration)
        {
            mConfiguration = configuration;
            tb_ZoomFactor.Maximum = (int)Configuration.ZOOM_FACTOR_MAX;
            tb_ZoomFactor.Minimum = (int)Configuration.ZOOM_FACTOR_MIN;
            tb_ZoomFactor.Value = (int)mConfiguration.ZoomFactor;

            tb_SpeedFactor.Maximum = (int)(100 * Configuration.SPEED_FACTOR_MAX);
            tb_SpeedFactor.Minimum = (int)(100 * Configuration.SPEED_FACTOR_MIN);
            tb_SpeedFactor.Value = (int)(100 * mConfiguration.SpeedFactor);

            tb_Width.Maximum = 1000;
            tb_Width.Minimum = 100;
            tb_Width.Value = mConfiguration.MagnifierWidth;

            tb_Height.Maximum = 1000;
            tb_Height.Minimum = 100;
            tb_Height.Value = mConfiguration.MagnifierHeight;

            lbl_ZoomFactor.Text = mConfiguration.ZoomFactor.ToString();
            lbl_SpeedFactor.Text = mConfiguration.SpeedFactor.ToString();
            lbl_Width.Text = mConfiguration.MagnifierWidth.ToString();
            lbl_Height.Text = mConfiguration.MagnifierHeight.ToString();


            //--- Init Boolean Settings ---
            cb_CloseOnMouseUp.Checked = mConfiguration.CloseOnMouseUp;
            cb_DoubleBuffered.Checked = mConfiguration.DoubleBuffered;
            cb_HideMouseCursor.Checked = mConfiguration.HideMouseCursor;
            cb_RememberLastPoint.Checked = mConfiguration.RememberLastPoint;
            cb_ReturnToOrigin.Checked = mConfiguration.ReturnToOrigin;
            cb_ShowInTaskbar.Checked = mConfiguration.ShowInTaskbar;
            cb_TopMostWindow.Checked = mConfiguration.TopMostWindow;

            ShowInTaskbar = false;
        }

        private void tb_ZoomFactor_Scroll(object sender, EventArgs e)
        {
            TrackBar tb = (TrackBar)sender;
            mConfiguration.ZoomFactor = tb.Value;
            lbl_ZoomFactor.Text = mConfiguration.ZoomFactor.ToString();
        }

        private void tb_SpeedFactor_Scroll(object sender, EventArgs e)
        {
            TrackBar tb = (TrackBar)sender;
            mConfiguration.SpeedFactor = tb.Value / 100.0f;
            lbl_SpeedFactor.Text = mConfiguration.SpeedFactor.ToString();
        }

        private void tb_Width_Scroll(object sender, EventArgs e)
        {
            TrackBar tb = (TrackBar)sender;
            mConfiguration.MagnifierWidth = tb.Value;
            lbl_Width.Text = mConfiguration.MagnifierWidth.ToString();

            if (cb_Symmetry.Checked)
            {
                tb_Height.Value = tb.Value;
                mConfiguration.MagnifierHeight = tb.Value;
                lbl_Height.Text = mConfiguration.MagnifierHeight.ToString();
            }
        }

        private void tb_Height_Scroll(object sender, EventArgs e)
        {
            TrackBar tb = (TrackBar)sender;
            mConfiguration.MagnifierHeight = tb.Value;
            lbl_Height.Text = mConfiguration.MagnifierHeight.ToString();
        }

        private void cb_Symmetry_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (cb.Checked)
            {
                // Symmetric: Don't enable
                tb_Height.Enabled = false;
            }
            else
            {
                // Non-symmetric: Allow height to be controlled independently.
                tb_Height.Enabled = true;
            }
        }

        private void pb_About_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void pb_About_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void pb_About_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog(this);
        }


        private void btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void cb_CloseOnMouseUp_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            mConfiguration.CloseOnMouseUp = cb.Checked;
        }

        private void cb_DoubleBuffered_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            mConfiguration.DoubleBuffered = cb.Checked;
        }

        private void cb_HideMouseCursor_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            mConfiguration.HideMouseCursor = cb.Checked;
        }

        private void cb_RememberLastPoint_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            mConfiguration.RememberLastPoint = cb.Checked;
        }

        private void cb_ReturnToOrigin_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            mConfiguration.ReturnToOrigin = cb.Checked;
        }

        private void cb_ShowInTaskbar_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            mConfiguration.ShowInTaskbar = cb.Checked;
        }

        private void cb_TopMostWindow_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            mConfiguration.TopMostWindow = cb.Checked;
        }


        //--- Data Members ---
        private Configuration mConfiguration;

    }
}