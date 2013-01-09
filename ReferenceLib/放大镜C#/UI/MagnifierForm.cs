///----------------------------------------------------------------------------
/// Class     : MagnifierForm
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
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;

namespace Magnifier20070401
{
    public partial class MagnifierForm : Form
    {
        public MagnifierForm(Configuration configuration, Point startPoint)
        {
            InitializeComponent();

            //--- My Init ---
            mConfiguration = configuration;
            FormBorderStyle = FormBorderStyle.None;

            ShowInTaskbar = mConfiguration.ShowInTaskbar;
            TopMost = mConfiguration.TopMostWindow;
            Width = mConfiguration.MagnifierWidth;
            Height = mConfiguration.MagnifierHeight;

            // Make the window (the form) circular
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(ClientRectangle);
            Region = new Region(gp);

            mImageMagnifier = Properties.Resources.magnifierGlass;

            mTimer = new Timer();
            mTimer.Enabled = true;
            mTimer.Interval = 20;
            mTimer.Tick += new EventHandler(HandleTimer);

            mScreenImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                     Screen.PrimaryScreen.Bounds.Height);

            mStartPoint = startPoint;
            mTargetPoint = startPoint;

            if (mConfiguration.ShowInTaskbar)
                ShowInTaskbar = true;
            else
                ShowInTaskbar = false;
        }

        protected override void OnShown(EventArgs e)
        {
            RepositionAndShow();
        }

        private delegate void RepositionAndShowDelegate();

        private void RepositionAndShow()
        {
            if (InvokeRequired)
            {
                Invoke(new RepositionAndShowDelegate(RepositionAndShow));
            }
            else
            {
                // Capture the screen image now!
                Graphics g = Graphics.FromImage(mScreenImage);
                g.CopyFromScreen(0, 0, 0, 0, new Size(mScreenImage.Width, mScreenImage.Height));
                g.Dispose();

                mScreenImage.Save("D:\\test.bmp");

                if (mConfiguration.HideMouseCursor)
                    Cursor.Hide();
                else
                    Cursor = Cursors.Cross;

                Capture = true;
                
                if (mConfiguration.RememberLastPoint)
                {
                    mCurrentPoint = mLastMagnifierPosition;
                    Cursor.Position = mLastMagnifierPosition;
                    Left = (int)mCurrentPoint.X - Width / 2;
                    Top = (int)mCurrentPoint.Y - Height / 2;
                }
                else
                {
                    mCurrentPoint = Cursor.Position;
                }
                Show();
            }
        }

        void HandleTimer(object sender, EventArgs e)
        {
            float dx = mConfiguration.SpeedFactor * (mTargetPoint.X - mCurrentPoint.X);
            float dy = mConfiguration.SpeedFactor * (mTargetPoint.Y - mCurrentPoint.Y);

            if (mFirstTime)
            {
                mFirstTime = false;

                mCurrentPoint.X = mTargetPoint.X;
                mCurrentPoint.Y = mTargetPoint.Y;

                Left = (int)mCurrentPoint.X - Width / 2;
                Top = (int)mCurrentPoint.Y - Height / 2;
                
                return;
            }

            mCurrentPoint.X += dx;
            mCurrentPoint.Y += dy;

            if (Math.Abs(dx) < 1 && Math.Abs(dy) < 1)
            {
                mTimer.Enabled = false;
            }
            else
            {
                // Update location
                Left = (int)mCurrentPoint.X - Width / 2;
                Top = (int)mCurrentPoint.Y - Height / 2;
                mLastMagnifierPosition = new Point((int)mCurrentPoint.X, (int)mCurrentPoint.Y);
            }

            Refresh();
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            mOffset = new Point(Width / 2 - e.X, Height / 2 - e.Y);
            mCurrentPoint = PointToScreen(new Point(e.X + mOffset.X, e.Y + mOffset.Y));
            mTargetPoint = mCurrentPoint;
            mTimer.Enabled = true;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (mConfiguration.CloseOnMouseUp)
            {
                Close();
                mScreenImage.Dispose();
            }

            Cursor.Show();
            Cursor.Position = mStartPoint;            
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mTargetPoint = PointToScreen(new Point(e.X + mOffset.X, e.Y + mOffset.Y));
                mTimer.Enabled = true;
            } 
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (mConfiguration.DoubleBuffered)
            {
                // Do not paint background (required for double buffering)!
            }
            else
            {
                base.OnPaintBackground(e);
            }
        }

        protected override void  OnPaint(PaintEventArgs e)
        {
            if (mBufferImage == null)
            {
                mBufferImage = new Bitmap(Width, Height);
            }
            Graphics bufferGrf = Graphics.FromImage(mBufferImage);

            Graphics g;

            if (mConfiguration.DoubleBuffered)
            {
                g = bufferGrf;
            }
            else
            {
                g = e.Graphics;
            }

            if (mScreenImage != null)
            {
                Rectangle dest = new Rectangle(0, 0, Width, Height);
                int w = (int)(Width / mConfiguration.ZoomFactor);
                int h = (int)(Height / mConfiguration.ZoomFactor);
                int x = Left - w / 2 + Width / 2;
                int y = Top - h / 2 + Height / 2;

                g.DrawImage(
                    mScreenImage,
                    dest,
                    x, y,
                    w, h,
                    GraphicsUnit.Pixel);
            }

            if (mImageMagnifier != null)
            {
                g.DrawImage(mImageMagnifier, 0, 0, Width, Height);
            }

            if (mConfiguration.DoubleBuffered)
            {
                e.Graphics.DrawImage(mBufferImage, 0, 0, Width, Height);
                //hexin
                //mBufferImage.Save("d:\\test3.bmp");
            }      
        }


        //--- Data Members ---
        #region Data Members
        private Timer mTimer;
        private Configuration mConfiguration;
        private Image mImageMagnifier;
        private Image mBufferImage = null;
        private Image mScreenImage = null;
        private Point mStartPoint;
        private PointF mTargetPoint;
        private PointF mCurrentPoint;
        private Point mOffset;
        private bool mFirstTime = true;
        private static Point mLastMagnifierPosition = Cursor.Position;
        #endregion
    }
}