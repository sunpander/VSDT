///----------------------------------------------------------------------------
/// Class     : MagnifierMainForm
/// Purpose   : Provide simple magnifier. 
/// Written by: Ogun TIGLI
/// History   : 31 May 2006/Wed starting date.
///             22 Dec 2006/Fri minor code fixes and hotsot support addition.
///             01 Apr 2007/Sun XML serialization support added.
///             
/// Notes: 
/// This software is provided 'as-is', without any express or implied 
/// warranty. In no event will the author be held liable for any damages 
/// arising from the use of this software.
/// 
/// Permission is granted to anyone to use this software for any purpose, 
/// including commercial applications, and to alter it and redistribute it 
/// freely, subject to the following restrictions:
///     1. The origin of this software must not be misrepresented; 
///        you must not claim that you wrote the original software. 
///        If you use this software in a product, an acknowledgment 
///        in the product documentation would be appreciated. 
///     2. Altered source versions must be plainly marked as such, and 
///        must not be misrepresented as being the original software.
///     3. This notice cannot be removed, changed or altered from any source 
///        code distribution.
/// 
///        (c) 2006-2007 Ogun TIGLI. All rights reserved. 
///----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Magnifier20070401
{
    public partial class MagnifierMainForm : Form
    {
        public MagnifierMainForm()
        {
            InitializeComponent();

            GetConfiguration();

            //--- My Init ---
            FormBorderStyle = FormBorderStyle.None;
            TopMost = true;
            StartPosition = FormStartPosition.CenterScreen;

            mImageMagnifierMainControlPanel = Properties.Resources.magControlPanel;

            if (mImageMagnifierMainControlPanel == null)
                throw new Exception("Resource cannot be found!");

            Width = mImageMagnifierMainControlPanel.Width;
            Height = mImageMagnifierMainControlPanel.Height;

            HotSpot hsConfiguration = new HotSpot(new Rectangle(50, 15, 35, 30));
            hsConfiguration.OnMouseDown += new HotSpot.MouseEventDelegate(hsConfiguration_OnMouseDown);
            hsConfiguration.OnMouseUp += new HotSpot.MouseEventDelegate(hsConfiguration_OnMouseUp);
            hsConfiguration.OnMouseMove += new HotSpot.MouseEventDelegate(hsConfiguration_OnMouseMove);

            HotSpot hsMagnfier = new HotSpot(new Rectangle(10, 15, 30, 30));
            hsMagnfier.OnMouseMove += new HotSpot.MouseEventDelegate(hsMagnfier_OnMouseMove);
            hsMagnfier.OnMouseDown += new HotSpot.MouseEventDelegate(hsMagnfier_OnMouseDown);
            hsMagnfier.OnMouseUp += new HotSpot.MouseEventDelegate(hsMagnfier_OnMouseUp);
            
            HotSpot hsExit = new HotSpot(new Rectangle(95, 20, 15, 15));
            hsExit.OnMouseUp += new HotSpot.MouseEventDelegate(hsExit_OnMouseUp);
            
            mHotSpots.Add(hsConfiguration);
            mHotSpots.Add(hsMagnfier);
            mHotSpots.Add(hsExit);

            ShowInTaskbar = false;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (mConfiguration.LocationX != -1 && mConfiguration.LocationY != -1)
            {
                Location = new Point(mConfiguration.LocationX, mConfiguration.LocationY);
            }            
        }

        private string mConfigFileName = "configData.xml";

        private void GetConfiguration() 
        {
            try
            {                
                mConfiguration = (Configuration)XmlUtility.Deserialize(mConfiguration.GetType(), mConfigFileName);                
            }
            catch 
            {
                mConfiguration = new Configuration();
            }
        }

        private void SaveConfiguration()
        {
            try
            {
                XmlUtility.Serialize(mConfiguration, mConfigFileName);
            }
            catch (Exception e)
            {
                Console.WriteLine("Serialization problem: " + e.Message);
            }
        }       


        private void hsConfiguration_OnMouseMove(Object sender)
        {

        }

        private void hsConfiguration_OnMouseUp(Object sender)
        {
            ConfigurationForm configForm = new ConfigurationForm(mConfiguration);
            configForm.ShowDialog(this);
        }

        private void hsConfiguration_OnMouseDown(Object sender)
        {

        }

        private void hsMagnfier_OnMouseUp(object sender)
        {
             
        }
        
        private void hsMagnfier_OnMouseDown(object sender)
        {
            int x = mLastCursorPosition.X;
            int y = mLastCursorPosition.Y;
            MagnifierForm magnifier = new MagnifierForm(mConfiguration, mLastCursorPosition);
            magnifier.Show();
        }
        

        private void hsMagnfier_OnMouseMove(object sender)
        {
            
        }        
        
        private void hsExit_OnMouseUp(Object sender)
        {
            SaveConfiguration();
            Application.Exit();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (mImageMagnifierMainControlPanel != null)
            {
                g.DrawImage(mImageMagnifierMainControlPanel, 0, 0, Width, Height);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;

            mPointMouseDown = new Point(e.X, e.Y);
            mLastCursorPosition = Cursor.Position;

            foreach (HotSpot hotSpot in mHotSpots)
            {
                // If mouse event handled by this hot-stop then return!
                if (hotSpot.ProcessMouseDown(e)) return;                
            }            
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            foreach (HotSpot hotSpot in mHotSpots)
            {
                // If mouse event handled by this hot-stop then return!
                if (hotSpot.ProcessMouseUp(e)) return;
            } 
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {                        
            foreach (HotSpot hotSpot in mHotSpots)
            {
                // If mouse event handled by this hot-stop then return!
                if (hotSpot.ProcessMouseMove(e))
                {
                    Cursor = Cursors.Hand;
                    return;
                }
            }
            Cursor = Cursors.SizeAll;

            if (e.Button == MouseButtons.Left)
            {
                int dx = e.X - mPointMouseDown.X;
                int dy = e.Y - mPointMouseDown.Y;

                Left += dx;
                Top += dy;

                mConfiguration.LocationX = Left;
                mConfiguration.LocationY = Top;
            }
        }

        private Image mImageMagnifierMainControlPanel = null;
        private List<HotSpot> mHotSpots = new List<HotSpot>();
        private Point mPointMouseDown;
        private Point mLastCursorPosition;
        private Configuration mConfiguration = new Configuration();
    }
}