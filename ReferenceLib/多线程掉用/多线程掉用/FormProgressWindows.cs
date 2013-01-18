using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace 多线程掉用
{
    public partial class FormProgressWindows : Form
    {
        private struct ReportStat
        {
            public int step;
            public int currentIndex;
            public string msg;
        }
        private bool _bCancel;
        private int _bPause;
        private bool _bIgnoreError;
        private System.Threading.Semaphore _semaPause;
        private DataRow[] _tcRows;
        private System.Collections.Generic.HashSet<int> _setErrorList;
 
        public System.Collections.Generic.HashSet<int> ErrorList { get { return _setErrorList; } }
        public DataTable TableTcList
        {
            set
            {
                _tcRows = value.Select("", "ID");
                for (int k = 0; k < _tcRows.Length; k++)
                {
                    _setErrorList.Add((int)_tcRows[k]["ID"]);
                }
            }
        }
        public FormProgressWindows()
        {
            _setErrorList = new HashSet<int>();
            _bIgnoreError = false;
            _semaPause = new System.Threading.Semaphore(0, 1);
            InitializeComponent();
        }

        private bool CheckState()
        {
            if (_bPause == 1)
            {
                ReportStat rs2 = new ReportStat();
                rs2.step = 2;
                backgroundWorker1.ReportProgress(0 * 100 / _tcRows.Length, rs2);
                _semaPause.WaitOne();
                rs2 = new ReportStat();
                rs2.step = 3;
                backgroundWorker1.ReportProgress(0 * 100 / _tcRows.Length, rs2);
            }
            if (_bCancel)
                return true;

            return false;

        }

        private int i;
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //_tcRows 存放要处理的数据
            for (i = 0; i < _tcRows.Length; Interlocked.Increment(ref i))
            {
                if (CheckState()) { e.Cancel = true; return; }

                ReportStat rs0 = new ReportStat();
                rs0.step = 0;
                rs0.currentIndex = i;
                rs0.msg = string.Format("正在处理：{0}。 获取数据...", _tcRows[i]["NAME"]);
                backgroundWorker1.ReportProgress(i * 100 / _tcRows.Length, rs0);

                ReportStat rs1;
                string strTcData = string.Empty;
                if (CheckState()) { e.Cancel = true; return; }
                try
                {
                    //正常处理信息，错误throw 抛出异常
                    //////
                    DoSomething(i, _tcRows[i]["ID"].ToString(), _tcRows[i]["NAME"].ToString());
                }
                catch (Exception ex)
                {
                    rs1 = new ReportStat();
                    if (!_bIgnoreError && i != _tcRows.Length)
                    {
                        int bOldPause = Interlocked.CompareExchange(ref _bPause, 1, 0);
                        rs1.step = 4;
                    }
                    else
                    {
                        rs1.step = 1;
                    }
                    rs1.currentIndex = i;
                    rs1.msg = ex.Message;
                    backgroundWorker1.ReportProgress((i + 1) * 100 / _tcRows.Length, rs1);
                    if (CheckState()) { e.Cancel = true; return; }

                    continue;
                }
  
                //成功
                rs1 = new ReportStat();
                rs1.step = 1;
                rs1.currentIndex = i;
                rs1.msg = "成功!";
                backgroundWorker1.ReportProgress((i + 1) * 100 / _tcRows.Length, rs1);

                _setErrorList.Remove((int)_tcRows[i]["ID"]);
            }
        }


        private bool DoSomething(int nIndex,string id,string name)
        {

            ReportStat rs0 = new ReportStat();
            rs0.step = 0;
            rs0.currentIndex = nIndex;
            rs0.msg = string.Format("正在处理电文：{0}。 名称{1}...", id, name);
            backgroundWorker1.ReportProgress(nIndex * 100 / _tcRows.Length, rs0);
            bool error = false;
            if (name == "NAME8")
            {
                error = true;
            }
            if (error)
            {
                ApplicationException ae = new ApplicationException("发送错误...");
                throw ae;
            }
            return true;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (this.txtLog.Text.Length / (float)this.txtLog.MaxLength > 0.95)
                this.txtLog.Clear();
            this.txtLog.AppendText("\r\n");
            ReportStat rs = (ReportStat)e.UserState;
            if (rs.step == 0) //开始处理
            {
                this.lblWork.Text = rs.msg;
                this.lblPercent.Text = string.Format("{0}%", e.ProgressPercentage);
                this.lblCount.Text = string.Format("{0}/{1}", rs.currentIndex + 1, _tcRows.Length); ;
            }
            else if (rs.step == 1) //正常进行中----
            {
                this.progressBarControl1.Value = e.ProgressPercentage;
                this.lblPercent.Text = string.Format("{0}%", e.ProgressPercentage);
                this.txtLog.AppendText("正处理。第------"+rs.currentIndex +"条记录"+rs.msg);
            }
            else if (rs.step == 2)//点了暂停按钮 后
            {
                this.btnPause.Enabled = true;
                this.btnPause.Text = "继续";
            }
            else if (rs.step == 3)//点了继续 按钮后
            {
                this.btnPause.Enabled = true;
                this.btnPause.Text = "暂停";
            }
            else if (rs.step == 4)
            {
                DialogResult dr = MessageBox.Show(this, "出错，忽略？终止？重试", "错误", MessageBoxButtons.AbortRetryIgnore);
                if (dr == DialogResult.Ignore)
                {
                    //忽略继续
                    _bPause = 0;
                    _semaPause.Release();

                }
                else if (dr == DialogResult.Abort)
                {
                    //设为暂停状态
                    this.btnPause.Enabled = true;
                    this.btnPause.Text = "继续";
                }
                else
                {
                    //重试 i 减一 继续
                    Interlocked.Decrement(ref i);
                    _bPause = 0;
                    _semaPause.Release();
                }
                this.progressBarControl1.Value = e.ProgressPercentage;
                this.lblPercent.Text = string.Format("{0}%", e.ProgressPercentage);
                this.txtLog.AppendText("出错:"+rs.msg);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                this.txtLog.AppendText(string.Format("取消。"));
            else
                this.txtLog.AppendText(string.Format("完毕。"));
            this.lblWork.Text = string.Empty;
            this.btnPause.Enabled = false;
            this.btnCancel.Text = "关闭";
            this.btnCancel.Enabled = true;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            this.btnPause.Enabled = false;
            int bOldPause = Interlocked.CompareExchange(ref _bPause, 1, 0);

            if (bOldPause == 1)
            {
                _bPause = 0;
                _semaPause.Release();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                this.btnPause.Enabled = false;
                this.btnCancel.Enabled = false;
                if (_bPause == 1)
                {
                    _semaPause.Release();
                }
                this._bCancel = true;
            }
            else
            {
                this.Close();
            }
        }


        private void FormEPEXProgressWindows_Load(object sender, EventArgs e)
        {
            _bPause = 0;
            _bCancel = false;
            this.lblWork.Text = string.Empty;
            this.lblPercent.Text = "0%";
            this.lblCount.Text = "0/0";
            this.backgroundWorker1.RunWorkerAsync();
        }

        private void FormEPEXProgressWindows_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.backgroundWorker1.IsBusy)
                e.Cancel = true;
        }

        private void chkIgnoreError_CheckedChanged(object sender, EventArgs e)
        {
            _bIgnoreError = chkIgnoreError.Checked;
        }
    }
}
