using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.Windows;
using System.Drawing;
using System.Collections;
namespace RichTextBox文本变色
{
    public partial class MyCompareFrame : UserControl
    {
        public MyCompareFrame()
        {
            InitializeComponent();
            this.AllowDrop = true;
            markControl1.Location = new System.Drawing.Point(initMarkLeft, 0);
            // markControl1.Refresh();
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Width = initMarkLeft;
            panel1.Height = initMarkHight + 4;

            label1.Text = "用来比较内容.\r\n将要比较的data从左边grid拖到该页面";
        }
        List<Control> listControl = new List<Control>(); //所有MyPanel集合
 
        int panelHeight = 42;

        int initMarkLeft = 100;
        int initMarkHight = 27;

        List<Control> listMoveTogetherControl = new List<Control>();
        private  Panel efPanel1;
        private Label label1;
        private  Panel panel1;
     

        public void OnAddNewBox(string tc_id, string tc_data, ArrayList textSplitList,object tag)
        { 
            try
            {
                MyPanel panel = new MyPanel();
                panel.LabelInfo = "TC_ID:" + tc_id;
                panel.btnDel.Click += new EventHandler(btnDel_Click);
                panel.myRichTextBox1.Tag = tag;

                panel.myRichTextBox1.SelectionChanged -= new EventHandler(myRichTextBox1_SelectionChanged);
                panel.myRichTextBox1.HScroll -= new EventHandler(myRichTextBox1_HScroll);

                SetRichTextBoxText(panel.myRichTextBox1, textSplitList, tc_data);

                panel.myRichTextBox1.MouseClick += new MouseEventHandler(tbTeleData_MouseDown);
                panel.myRichTextBox1.Click += new EventHandler(myRichTextBox1_Click);
                panel.myRichTextBox1.MouseCaptureChanged += new EventHandler(myRichTextBox1_MouseCaptureChanged);
                panel.myRichTextBox1.SelectionChanged += new EventHandler(myRichTextBox1_SelectionChanged);
                panel.myRichTextBox1.HScroll+=new EventHandler(myRichTextBox1_HScroll);
                this.Controls.Add(panel);
                listControl.Add(panel);
                listMoveTogetherControl.Add(panel.myRichTextBox1);
                //调整位置
                ReLayoutCtrl(listControl);
                ReLayoutCtrl(listControl);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        void myRichTextBox1_MouseCaptureChanged(object sender, EventArgs e)
        {
            RichTextBox richbox = sender as RichTextBox;
            if (richbox == null)
                return;
            richbox.Focus();
            //throw new NotImplementedException();
        }

        void myRichTextBox1_Click(object sender, EventArgs e)
        {
                RichTextBox richbox =  sender as RichTextBox;
               // if (richbox == null)
                int kk =    richbox.Width ;
                kk = richbox.Parent.Width;
                kk = richbox.Parent.Location.Y;
            //throw new NotImplementedException();
        }
 
        private void myRichTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                RichTextBox richbox =  sender as RichTextBox;
                if (richbox == null)
                    return;
                string tmp = richbox.Text.Substring(0, richbox.SelectionStart);
                byte[] sarr = System.Text.Encoding.Default.GetBytes(tmp);
                int lenPre = sarr.Length;

                markControl1.PointX = lenPre;
            }
            catch { }
        }
        private bool blFirstClick = false;
        private void myRichTextBox1_HScroll(object sender, EventArgs e)
        {
            try
            {
                RichTextBox richbox = sender as RichTextBox;
                if (richbox == null)
                    return;
             
                if (!richbox.Focused)
                    return;
                int lenPre = 0;
                int index = richbox.GetCharIndexFromPosition(new System.Drawing.Point(1, 1));
                if (index > 0)
                {
                    string tmp = richbox.Text.Substring(0, index);
                    byte[] sarr = System.Text.Encoding.Default.GetBytes(tmp);
                    lenPre = sarr.Length;
                }
                markControl1.Location = new System.Drawing.Point(0 + initMarkLeft - lenPre * 6, 0);
                markControl1.Width = richbox.Width + richbox.Left + lenPre * 6;
            }
            catch
            {
            }
            finally
            {
            
            }
        }

        void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                MyPanel ctrl = (sender as Control).Parent as MyPanel;
                if (ctrl != null)
                {
                    if (listControl.Contains(ctrl))
                    {
                        listMoveTogetherControl.Remove(ctrl.myRichTextBox1);
                        listControl.Remove(ctrl);

                        this.Controls.Remove(ctrl);
                        ReLayoutCtrl(listControl);
                        ReLayoutCtrl(listControl);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        private void ReLayoutCtrl(List<Control> list)
        {
            try
            {
               // this.SuspendLayout();
                if (markControl1.Width < this.Width - initMarkLeft)
                {
                    markControl1.Width = this.Width - initMarkLeft;
                }
                int topY = efPanel1.Location.Y;
                int marginRight = 0;
                if (list.Count * panelHeight + initMarkHight > this.Height)
                {
                    marginRight = 20;
                }
                if (list.Count == 0)
                {
                    label1.Visible = true;
                }
                else
                {
                    label1.Visible = false;
                }
                for (int i = 0; i < list.Count; i++)
                {
                    MyPanel panel = list[i] as MyPanel;

                    panel.Height = panelHeight;
                    panel.Width = this.Width - marginRight;
                    panel.Location = new System.Drawing.Point(0, topY + initMarkHight + panelHeight * i);
                    panel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

                    panel.myRichTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    panel.myRichTextBox1.Width = panel.Width - initMarkLeft - 37;
                    panel.myRichTextBox1.Location = new System.Drawing.Point(initMarkLeft, 0);
                    panel.myRichTextBox1.ScrollBars = RichTextBoxScrollBars.Horizontal;
                    panel.myRichTextBox1.OtherRichTextBox = listMoveTogetherControl;
                    panel.myRichTextBox1.WordWrap = false;

                    panel.btnDel.Location = new System.Drawing.Point(initMarkLeft + panel.myRichTextBox1.Width, 0);
                }
                markControl1.Location = new System.Drawing.Point(initMarkLeft, 0);
                // markControl1.Refresh();
                panel1.Location = new System.Drawing.Point(0, 0);
                panel1.Width = initMarkLeft;
                panel1.Height = initMarkHight + 4;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
               // this.ResumeLayout(false);
            }
        }

 
        #region 设置电文内容颜色
        public  ColorObjCollection hotColor;
        /// <summary>
        /// 设置电文内容颜色
        /// </summary>
        /// <param name="cellData"></param>
        /// <param name="RowNO"></param>
        private void SetRichBoxTextBackColor(RichTextBox richBox, string cellData, int RowNO)
        {
            if (hotColor == null || hotColor.COLORLIST.Length < 5)
                return;
            int colorNO = RowNO % 5;
            int startPoint = richBox.Text.Length - cellData.Length;
            if (startPoint < 0)
                startPoint = 0;
            int textLength = cellData.Length;
            string redStr = hotColor.COLORLIST[colorNO].R.ToString();
            string greenStr = hotColor.COLORLIST[colorNO].G.ToString();
            string blueStr = hotColor.COLORLIST[colorNO].B.ToString();
            string colorname = "\r\n{\\colortbl ;\\red" + redStr + "\\green" + greenStr + "\\blue" + blueStr + ";}";
            richBox.Select(startPoint, textLength);
            //			this.tbTeleData.SelectionColor = colorList[colorNO];
            string customRtf = richBox.SelectedRtf;
            customRtf = customRtf.Replace(";}}", ";}}" + colorname);
            customRtf = customRtf.Replace("\\pard", "\\pard\\highlight1");
            richBox.SelectedRtf = customRtf;
        }
        /// <summary>
        /// 颜色下拉事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void ColorComboBox_TextChanged(object sender, System.EventArgs e)
        //{
        //    string colorName = this.ColorComboBox.Text.Trim();
        //    if (this.myColorList.COLORLIST.Contains(colorName))
        //    {
        //        hotColor = (ColorObjCollection)this.myColorList.COLORLIST[colorName];
        //        SetRichTextBoxText("");
        //    }
        //}
        private void SetRichTextBoxText( RichTextBox tbTeleData,  ArrayList textSplitList, string teleOld)
        {
            //设置电文内容
            tbTeleData.SelectionChanged -= new EventHandler(tbTeleData_SelectionChanged);
            markControl1.Location = new System.Drawing.Point(tbTeleData.Location.X, 0);
            markControl1.Width = tbTeleData.Width;
            markControl1.PointX = 0;
            if (string.IsNullOrEmpty(teleOld))
            {
                teleOld = tbTeleData.Text;
            }
            if (textSplitList.Count > 0)
            {
                tbTeleData.Clear();
                for (int i = 0; i < textSplitList.Count; i++)
                {
                    string mytext = textSplitList[i].ToString();

                    if (mytext.Contains("\r") || mytext.Contains("\n"))
                    {
                        mytext = mytext.Replace("\r", "_");
                        mytext = mytext.Replace("\n", "_");
                    }

                    tbTeleData.AppendText(mytext);
                    SetRichBoxTextBackColor(tbTeleData, mytext, i);
                }
            }
   
            if (tbTeleData.Text.Length < teleOld.Length)
            {
                 tbTeleData.AppendText(teleOld.Substring(tbTeleData.Text.Length));
            }
            tbTeleData.SelectionChanged += new EventHandler(tbTeleData_SelectionChanged);
        }

        void tbTeleData_SelectionChanged(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region 单击电文内容,显示所单击电文字段信息
        ToolTip ttTip = new ToolTip();
        private void tbTeleData_MouseEnter(object sender, System.EventArgs e)
        {
            if ((sender as Control).Text.Length == 0)
                return;
            ttTip.AutoPopDelay = 2000;
            ttTip.SetToolTip(sender as Control, "单击可查询字段名!");
        }
 
        public delegate void   ShowInfo(string info,Control ctrl);
        public event ShowInfo EventShowInfo;
        private void tbTeleData_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            RichTextBox ctrl = sender as RichTextBox;

             
            if (ctrl.Text.Length == 0)
                return;
            if (ctrl.Tag !=null)
            {
               
                int selectPoint = ctrl.SelectionStart;
                string selectionStr = ctrl.Text.Substring(0, selectPoint);
                string tooltipText = "";
                tooltipText = GetToolTipText(1,  1);
                if (EventShowInfo != null)
                {
                    EventShowInfo(tooltipText, ctrl);
                }
                //if (tooltipText.Trim() != string.Empty)
                //{
                //     ttTip.SetToolTip(ctrl, tooltipText);
                //}
            }
        }

        private string GetToolTipText(int selectLength, int m_nFieldCount )
        {
            string tooltipText = "";
 
            return tooltipText;
        }
        #endregion

        #region 组件设计器生成的代码
        private MarkControl markControl1;      /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new  Panel( );
            this.efPanel1 = new  Panel( );
            this.markControl1 = new RichTextBox文本变色.MarkControl();
            this.label1 = new System.Windows.Forms.Label();
             this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(102, 27);
            this.panel1.TabIndex = 1;
            // 
            // efPanel1
            // 
            this.efPanel1.Location = new System.Drawing.Point(0, 0);
            this.efPanel1.Name = "efPanel1";
            this.efPanel1.Size = new System.Drawing.Size(11, 14);
            this.efPanel1.TabIndex = 2;
            // 
            // markControl1
            // 
            this.markControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.markControl1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.markControl1.Location = new System.Drawing.Point(97, 0);
            this.markControl1.Name = "markControl1";
            this.markControl1.Size = new System.Drawing.Size(500, 27);
            this.markControl1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("新宋体", 13F);
            this.label1.Location = new System.Drawing.Point(53, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // MyCompareFrame
            // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.efPanel1);
            this.Controls.Add(this.markControl1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0, 3, 6, 3);
            this.Name = "MyCompareFrame";
            this.Size = new System.Drawing.Size(600, 388);
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.MyCompareFrame_Scroll);
             this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private void MyCompareFrame_Scroll(object sender, ScrollEventArgs e)
        {
            ReLayoutCtrl(listControl);
        }
    }
}
