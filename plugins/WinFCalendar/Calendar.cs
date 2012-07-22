using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YXControl;
using HuangF.Sys.Date;

namespace WinFCalendar
{
    public partial class Calendar : Form
    {
#region 节日信息

        private string[] holidays ={
        "0101 *元旦节", "0202 世界湿地日","0210 国际气象节","0214 情人节","0301 国际海豹日",
"0303 全国爱耳日","0305 学雷锋纪念日","0308 妇女节","0312 植树节\n孙中山逝世纪念日","0314 国际警察日",
"0315 消费者权益日","0317 中国国医节\n国际航海日","0321 世界森林日\n消除种族歧视国际日\n世界儿歌日",
"0322 世界水日","0323 世界气象日","0324 世界防治结核病日","0325 全国中小学生安全教育日","0330 巴勒斯坦国土日",
"0401 愚人节\n全国爱国卫生运动月(四月)\n税收宣传月(四月)","0407 世界卫生日","0422 世界地球日",
"0423 世界图书和版权日","0424 亚非新闻工作者日","0501 *劳动节","0504 青年节","0505 碘缺乏病防治日",
"0508 世界红十字日","0512 国际护士节","0515 国际家庭日","0517 国际电信日","0518 国际博物馆日",
"0520 全国学生营养日","0523 国际牛奶日","0531 世界无烟日", "0601 国际儿童节","0605 世界环境保护日",
"0606 全国爱眼日","0617 防治荒漠化和干旱日","0623 国际奥林匹克日","0625 全国土地日","0626 国际禁毒日",
"0701 香港回归纪念日\n中共诞辰\n世界建筑日","0702 国际体育记者日","0707 抗日战争纪念日","0711 世界人口日",
"0730 非洲妇女日","0801 建军节","0808 中国男子节(爸爸节)","0815 抗日战争胜利纪念","0908 国际扫盲日\n国际新闻工作者日",
"0909 毛泽东逝世纪念","0910 中国教师节", "0914 世界清洁地球日","0916 国际臭氧层保护日","0918 九·一八事变纪念日",
"0920 国际爱牙日","0927 世界旅游日","0928 孔子诞辰","1001 *国庆节\n世界音乐日\n国际老人节","1002 *国庆节假日\n国际和平与民主自由斗争日",
"1003 *国庆节假日","1004 世界动物日","1006 老人节","1008 全国高血压日\n世界视觉日","1009 世界邮政日\n万国邮联日",
"1010 辛亥革命纪念日\n世界精神卫生日","1013 世界保健日\n国际教师节","1014 世界标准日",
"1015 国际盲人节(白手杖节)","1016 世界粮食日","1017 世界消除贫困日","1022 世界传统医药日",
"1024 联合国日","1031 世界勤俭日","1107 十月社会主义革命纪念日","1108 中国记者日",
"1109 全国消防安全宣传教育日","1110 世界青年节","1111 国际科学与和平周(本日所属的一周)",
"1112 孙中山诞辰纪念日","1114 世界糖尿病日","1117 国际大学生节\n世界学生节","1120 *彝族年",
"1121 *彝族年\n世界问候日\n世界电视日","1122 *彝族年","1129 国际声援巴勒斯坦人民国际日","1201 世界艾滋病日",
"1203 世界残疾人日","1205 国际经济和社会发展志愿人员日","1208 国际儿童电视日","1209 世界足球日",
"1210 世界人权日","1212 西安事变纪念日","1213 南京大屠杀(1937年)纪念日！\n谨记血泪史！",
"1220 澳门回归纪念","1221 国际篮球日","1224 平安夜","1225 圣诞节","1226 毛泽东诞辰纪念"
                                   };

        private string[] lunarHolidays = { "0101 *春节","0102 *正月初二","0103 *正月初三","0104 *正月初四",
                                             "0105 *正月初五","0106 *正月初六","0115 元宵节","0505 *端午节",
"0707 七夕情人节","0715 中元节","0815 *中秋节","0909 重阳节","1208 腊八节","1223 小年","0100 *除夕"};

        private string[] nDayHolidays={"0150 世界麻风日","0520 母亲节","0530 全国助残日","0630 父亲节"
,"0730 被奴役国家周","0932 国际和平日","0940 国际聋人节\n世界儿童日","0950 世界海事日","1011 国际住房日",
"1013 国际减灾日","1144 感恩节"};

        private string[] animalNames = { "鼠", "牛", "虎", "兔", "龙", "蛇", "马", "羊", "猴", "鸡", "狗", "猪" };
#endregion
#region 自定义结构
        private struct DayInfo
        {
            public DateTime date;
            public string ganZhi;
            public string ganZhiM;
            public string ganZhiD;
            public string lunarMS;
            public string lunarDS;
            public int lunarM;
            public int lunarD;
            public string animal;
            public string term;
            public string holiday;
            public string lunarHoliday;
            public bool isHoliday;
            public bool isLunarHoliday;
        }

        private struct HolidayInfo
        {
            public string name;
            public int month;
            public int day;
            public int count;
        }
#endregion
#region 全局变量
        private DateTime displayDate = new DateTime();
        private DateTime oldDate = new DateTime();
        private int cColumn = -1; private int cRow = -1;
#endregion

        
        #region 构造函数

        public Calendar()
        {
            this.Visible = false;
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.AllPaintingInWmPaint , true);
            this.DoubleBuffered = true;
            
            SetBtnBGPic();
            foreach(CalendarCell c in MainPanel.Controls)
            {
                c.Click += new EventHandler(CellClicked);
                c.label1.Font = new Font("Tahoma", 26, FontStyle.Bold);
            }
            displayDate = DateTime.Today;
            CheckDateChange();

        }
        #endregion
        #region 各种方法
        #region 设置按钮背景
        private void SetBtnBGPic()
        {
            try
            {
                YearDown.BackgroundImage = new Bitmap( @"Calendar\BtnYP.png");
                YearUp.BackgroundImage = new Bitmap(@"Calendar\BtnYN.png");
                MonthDown.BackgroundImage = new Bitmap(@"Calendar\BtnMP.png");
                MonthUp.BackgroundImage = new Bitmap(@"Calendar\BtnMN.png");
                ReturnToday.BackgroundImage = new Bitmap(@"Calendar\BtnToday.png");
                ButtonClose.BackgroundImage = new Bitmap(@"Calendar\BtnClose.png");

                YearDown.NormalImage = new Bitmap(@"Calendar\BtnYP.png");
                YearUp.NormalImage = new Bitmap(@"Calendar\BtnYN.png");
                MonthDown.NormalImage = new Bitmap(@"Calendar\BtnMP.png");
                MonthUp.NormalImage = new Bitmap(@"Calendar\BtnMN.png");
                ReturnToday.NormalImage = new Bitmap(@"Calendar\BtnToday.png");
                ButtonClose.NormalImage = new Bitmap(@"Calendar\BtnClose.png");

                YearDown.MoveImage = new Bitmap(@"Calendar\BtnYPHover.png");
                YearUp.MoveImage = new Bitmap(@"Calendar\BtnYNHover.png");
                MonthDown.MoveImage = new Bitmap(@"Calendar\BtnMPHover.png");
                MonthUp.MoveImage = new Bitmap(@"Calendar\BtnMNHover.png");
                ReturnToday.MoveImage = new Bitmap(@"Calendar\BtnTodayHover.png");
                ButtonClose.MoveImage = new Bitmap(@"Calendar\BtnCloseHover.png");

                YearDown.MoveImage = new Bitmap(@"Calendar\BtnYPHover.png");
                YearUp.MoveImage = new Bitmap(@"Calendar\BtnYNHover.png");
                MonthDown.MoveImage = new Bitmap(@"Calendar\BtnMPHover.png");
                MonthUp.MoveImage = new Bitmap(@"Calendar\BtnMNHover.png");
                ReturnToday.MoveImage = new Bitmap(@"Calendar\BtnTodayHover.png");
                ButtonClose.MoveImage = new Bitmap(@"Calendar\BtnCloseHover.png");

                tableLayoutPanel5.BackgroundImage = new Bitmap(@"Calendar\Right.png");
                tableLayoutPanel2.BackgroundImage = new Bitmap(@"Calendar\Head.png");
                tableLayoutPanel10.BackgroundImage = new Bitmap(@"Calendar\Foot.png");
                tableLayoutPanel9.BackgroundImage = new Bitmap(@"Calendar\NeckFirst.png");
                tableLayoutPanel7.BackgroundImage = new Bitmap(@"Calendar\NeckSecond.png");

            }
            catch
            {
                MessageBox.Show("YXIS.SystemTools.Calendar.SetBtnBGPic", "万年历图片路径不正确！");
            }
        }
#endregion

        /// <summary>
        /// 检查是否需要重绘单元格
        /// </summary>
        /// <returns></returns>
        private bool CheckDateChange()
        {
            if (displayDate!=oldDate) 
            {
                if (displayDate.Year<1901 || displayDate.Year>2049)
                {
                    MessageBox.Show("计算器", "超出范围，年份范围在1901-2049之间!");
                    //YXIS.SystemForms.WariningMask.CreateInstance().ShowMessage("计算器", "超出范围，年份范围在1901-2049之间!");
                    displayDate = oldDate;
                    return false;
                }
                MainPanel.Visible = false;
                InitCells();
                oldDate = displayDate;
                CellClicked(MainPanel.GetControlFromPosition(cColumn, cRow), new EventArgs());
                MainPanel.Visible = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 有单元格被点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CellClicked(object sender, EventArgs e)
        {
            CalendarCell cell = (CalendarCell)sender;
            DayInfo cellInfo = new DayInfo();
            cellInfo = (DayInfo)cell.Tag;
            if(cellInfo.date.Month!=displayDate.Month)
            {
                displayDate = cellInfo.date;
                CheckDateChange();
                return;
            }
            else
            {
                DisplayCellInfo(cellInfo);
            }
            displayDate = cellInfo.date;

            foreach(CalendarCell cCell in MainPanel.Controls)
            {
                try
                {
                    if (cCell == cell)
                    {
                        cCell.Clicked = true;
                    }
                    else
                    {
                        cCell.Clicked = false;
                    }
                    cCell.ResetColor();
                }
                catch
                { }

            }
            cell.ResetColor();
            cColumn = MainPanel.GetColumn(cell);
            cRow = MainPanel.GetRow(cell);
        }

        /// <summary>
        /// 将单击的单元格的信息显示到右边和上边
        /// </summary>
        /// <param name="Cellinfo"></param>
        private void DisplayCellInfo(DayInfo cellInfo)
        {
            LblYearInfo.Text = cellInfo.date.ToString("yyyy年M月d日 dddd");
            LblLunarYM.Text = "农历 " +  cellInfo.animal + "年\n" + cellInfo.lunarMS;
            LblLunarDay.Text = cellInfo.lunarDS;
            LblLunarGanZhi.Text = cellInfo.ganZhi + " " + cellInfo.ganZhiM + " " + cellInfo.ganZhiD;
            LblHoliday.Text = "";
            if (cellInfo.term!="")
            {
                LblHoliday.Text = "今日" + cellInfo.term + "\n";
            }
            LblHoliday.Text += cellInfo.holiday;
            LblHoliday.Text += cellInfo.lunarHoliday;
        }

        /// <summary>
        /// 重新设置单元格
        /// </summary>
        private void InitCells()
        {
            int column = 0; int row = 0;
            MainPanel.RowCount = 6;
            DateTime currMonth = new DateTime(displayDate.Year, displayDate.Month, 1);
            DateTime currDay = new DateTime();
            currDay = currMonth.AddDays((double)(0 - currMonth.DayOfWeek));
            CalendarCell currCell = new CalendarCell();
#region 填充单元格
            while (column<7 && row<6)
            {


                HFDate lunarProcessor = new HFDate(currDay);
                DayInfo currDayInfo = new DayInfo();
                currDayInfo.date = currDay;
                currDayInfo.animal = animalNames[lunarProcessor.LunarYear.Zhi.IntValue - 1];
                currDayInfo.lunarM = lunarProcessor.LunarCalendarMonth;
                currDayInfo.lunarD = lunarProcessor.LunarCalendarDay;
                currDayInfo.ganZhi = lunarProcessor.LunarYear.Name + "年"; 
                currDayInfo.ganZhiM = lunarProcessor.LunarMonth.Name + "月";
                currDayInfo.ganZhiD = lunarProcessor.LunarDay.Name + "日";
                currDayInfo.lunarMS = lunarProcessor.LunarCalendarMonthString;
                currDayInfo.lunarDS = lunarProcessor.LunarCalendarDayString;
                currDayInfo.term = lunarProcessor.SolarTermInfo;

                currDayInfo.isHoliday = IsHoliday(ref currDayInfo); //加入节假日判断
                currDayInfo.isLunarHoliday = IsLunarHoliday(ref currDayInfo);

                currCell = (CalendarCell)MainPanel.GetControlFromPosition(column, row);
                currCell.Visible = true;

                SetCellText(currCell, currDayInfo);
                SetCellColor(currCell);
                if (currDayInfo.date.Date==displayDate.Date)
                {
                    cColumn = column; cRow = row;
                    CellClicked(currCell, new EventArgs());
                }

                column++;
                if (column > 6)
                {
                    column = 0;
                    row++;
                }

                currDay = currDay.AddDays(1);
            }

            CalendarCell tmpCell = (CalendarCell)MainPanel.GetControlFromPosition(0, 5);
            if (((DayInfo)tmpCell.Tag).date.Month != displayDate.Date.Month)
            {

                for (int i = 0; i < 7; i++)
                {
                    tmpCell = (CalendarCell)MainPanel.GetControlFromPosition(i, 5);
                    tmpCell.Visible = false;
                }

            }

#endregion
            oldDate = displayDate;
            return;
        }


        /// <summary>
        /// 设置显示文字和TAG
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="info"></param>
        private void SetCellText(CalendarCell cell,DayInfo info)
        {
            cell.label1.Text = info.date.Day.ToString();
            cell.label2.Text = info.lunarMS+info.lunarDS;
            if (info.holiday!=null && info.holiday.Trim()!="")
            {
                cell.label2.Text = info.holiday;
            }
            if (info.term != "")
            {
                cell.label2.Text = info.term;
            }
            if (info.lunarHoliday!=null && info.lunarHoliday!="")
            {
                cell.label2.Text = info.lunarHoliday;
            }
            cell.Tag = (object)info;
            if (cell.label2.Text.IndexOf("\n")>0)
            {
                cell.label2.Text = cell.label2.Text.Remove(cell.label2.Text.Length - (cell.label2.Text.Length - cell.label2.Text.IndexOf("\n")));
            }
        }

        /// <summary>
        /// 判断是否为阳历的节假日,包括周末
        /// </summary>
        /// <param name="currentDayInfo"></param>
        /// <returns></returns>
        private bool IsHoliday(ref DayInfo currentDayInfo)
        {
            bool result = false;
            if (currentDayInfo.date.DayOfWeek == DayOfWeek.Saturday || currentDayInfo.date.DayOfWeek == DayOfWeek.Sunday)
            {
                result = true;
            }
            HolidayInfo info=new HolidayInfo();
            foreach (string cHoliday in holidays)
            {
                info = GetHoliday(1, cHoliday);
                if (info.month==currentDayInfo.date.Month && info.day ==currentDayInfo.date.Day)
                {
                    currentDayInfo.holiday += info.name.Replace("*", "") + "\n";
                    if (info.name.Contains("*"))
                    {
                        result = true;
                    }
                }
            }
            foreach (string cHoliday in nDayHolidays)
            {
                info = GetHoliday(3, cHoliday);
                if (info.month != currentDayInfo.date.Month)
                {
                    continue;
                }

                if (Convert.ToInt32(currentDayInfo.date.DayOfWeek)!=info.day)
                {
                    continue;
                }
                else
                {
                    DateTime tmpDate = currentDayInfo.date;
                    int count = 0;

                    if (info.count==5)
                    {
                        do
                        {
                            tmpDate = tmpDate.AddDays(7);
                            if (tmpDate.Month != currentDayInfo.date.Month)
                            {
                                break;
                            }
                            count++;
                        } while (true);
                        if (count == 0)
                        {
                            currentDayInfo.holiday += info.name.Replace("*", "") + "\n";
                            if (info.name.Contains("*"))
                            {
                                result = true;
                            }
                        }
                        return result;
                    }
                    
                    do
                    {
                        count++;
                        tmpDate = tmpDate.AddDays(-7);
                    } while (tmpDate.Month == displayDate.Month);
                    if (count==info.count)
                    {
                        currentDayInfo.holiday += info.name.Replace("*", "") + "\n";
                        if (info.name.Contains("*"))
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 判断是否为农历节假日,包括节气
        /// </summary>
        /// <param name="currentDayInfo"></param>
        /// <returns></returns>
        private bool IsLunarHoliday(ref DayInfo currentDayInfo)
        {
            bool result = false;
            if (currentDayInfo.term!=null)
            {
                if (currentDayInfo.term.Contains("清明"))
                {
                    return true;
                }
            }
            HolidayInfo info = new HolidayInfo();
            foreach (string cHoliday in lunarHolidays)
            {
                info = GetHoliday(2, cHoliday);
                if (currentDayInfo.lunarM==info.month && currentDayInfo.lunarD==info.day)
                {
                    currentDayInfo.lunarHoliday = info.name.Replace("*", "") + "\n";
                    if (info.name.Contains("*"))
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 拆解Holiday,type种类:1.阳历固定;2.农历固定;3.某月第几个星期几;
        /// </summary>
        /// <param name="currentDayInfo"></param>
        /// <returns></returns>
        private HolidayInfo GetHoliday(int type,string holiday)
        {
            HolidayInfo info = new HolidayInfo();
            string[] tempStr = holiday.Split(" ".ToCharArray());
            switch (type)
            {
                case 3:
                    info.name=tempStr[1];
                    info.month = Convert.ToInt32(tempStr[0].Substring(0, 2));
                    info.count = Convert.ToInt32(tempStr[0].Substring(2, 1));
                    info.day = Convert.ToInt32(tempStr[0].Substring(3, 1));
                    break;
                default:
                    info.name = tempStr[1];
                    info.month = Convert.ToInt32(tempStr[0].Substring(0, 2));
                    info.day = Convert.ToInt32(tempStr[0].Substring(2, 2));
                    break;
            }
            return info;
        }

        /// <summary>
        /// 判断节假日情况,设置单元格颜色
        /// </summary>
        /// <param name="currCell"></param>
        /// <param name="currDayInfo"></param>
        /// <param name="currMonth"></param>
        private void SetCellColor(CalendarCell currCell)
        {
            DayInfo currDayInfo = (DayInfo)currCell.Tag;
            currCell.BGColor = Color.Transparent;
            currCell.DateColor = Color.Black;
            currCell.LunarColor = Color.DimGray;

            if (currDayInfo.date.Month!=displayDate.Month)
            {
                currCell.BGColor = Color.Transparent;
                currCell.DateColor = Color.FromArgb(0xF0F0F0);
                currCell.LunarColor = Color.FromArgb(0xF0F0F0);
                currCell.HoverBGColor = Color.Transparent;
                currCell.HoverDateColor = currCell.DateColor = Color.FromArgb(0xF0F0F0);
                currCell.HoverLunarColor = currCell.LunarColor = Color.FromArgb(0xF0F0F0) ;
            }
            else
            {
                currCell.HoverBGColor = Color.FromArgb(128, Color.PeachPuff);
                currCell.HoverDateColor = Color.Gray;
                currCell.HoverLunarColor = Color.Gray;
                if (currDayInfo.lunarHoliday != null && currDayInfo.lunarHoliday != "")
                { currCell.LunarColor = Color.DarkGreen; }
                if (currDayInfo.holiday != null && currDayInfo.holiday != "")
                { currCell.LunarColor = Color.DarkGreen; }
                if (currDayInfo.holiday != null && currDayInfo.holiday != "" && currDayInfo.isHoliday==false)
                { currCell.DateColor = Color.DarkGreen; }
                if (currDayInfo.term!="")
                { currCell.LunarColor = Color.Purple; }
                if (currDayInfo.isLunarHoliday || currDayInfo.isHoliday)
                { 
                    currCell.LunarColor = Color.Red;
                    currCell.DateColor = Color.Red;
                }
            }
        }

        /// <summary>
        /// 更新时间显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ticker_Tick(object sender, EventArgs e)
        {
            LblTimeInfo.Text = DateTime.Now.ToString("HH:mm:ss");
        }
#region 按钮事件
        private void YearDown_Click(object sender, EventArgs e)
        {
            displayDate = displayDate.AddYears(-1);
            CheckDateChange();
        }

        private void YearUp_Click(object sender, EventArgs e)
        {
            displayDate = displayDate.AddYears(1);
            CheckDateChange();
        }

        private void MonthDown_Click(object sender, EventArgs e)
        {
            displayDate = displayDate.AddMonths(-1);
            CheckDateChange();
        }

        private void MonthUp_Click(object sender, EventArgs e)
        {
            displayDate = displayDate.AddMonths(1);
            CheckDateChange();
        }
#endregion
#region 键盘事件

        /// <summary>
        /// 方向键被按下
        /// </summary>
        /// <param name="direction">方向:1.上;2.右;3.下;4.左</param>
        private void ArrowKeyPressed(int direction)
        {
            DayInfo info = new DayInfo();
            CalendarCell cell = new CalendarCell();
            switch (direction)
            {
                case 1:
                    if (cRow == 0)
                    {
                        cell = (CalendarCell)MainPanel.GetControlFromPosition(cColumn, cRow);
                        info = (DayInfo)cell.Tag;
                        displayDate = info.date.Date.AddDays(-7);
                        CheckDateChange();
                        return;
                    }
                    else { cRow--; }
                    break;
                case 2:
                    if (cColumn == 6)
                    {
                        if (cRow == 5)
                        {
                            cell = (CalendarCell)MainPanel.GetControlFromPosition(cColumn, cRow);
                            info = (DayInfo)cell.Tag;
                            displayDate = info.date.Date.AddDays(1);
                            CheckDateChange();
                            return;
                        }
                        else
                        { cColumn = 0; cRow++; }
                    }
                    else
                    { cColumn++; }
                    break;
                case 3:
                    if (cRow == 5)
                    {
                        cell = (CalendarCell)MainPanel.GetControlFromPosition(cColumn, cRow);
                        info = (DayInfo)cell.Tag; 
                        displayDate = info.date.Date.AddDays(7);
                        CheckDateChange();
                        return;
                    }
                    else
                    { cRow++; }
                    break;
                case 4:
                    if (cColumn == 0)
                    {
                        if (cRow == 0)
                        {
                            cell = (CalendarCell)MainPanel.GetControlFromPosition(cColumn, cRow);
                            info = (DayInfo)cell.Tag;
                            displayDate = info.date.Date.AddDays(-1);
                            CheckDateChange();
                            return;
                        }
                        else
                        { cColumn = 6; cRow--; }
                    }
                    else
                    { cColumn--; }
                    break;
            }
            cell = (CalendarCell)MainPanel.GetControlFromPosition(cColumn, cRow);
            CellClicked(cell, new EventArgs());
        }
#endregion

#region 全局热键的消息处理(暂时不用)
//         protected override void WndProc(ref Message m)
//         {
//             const int WM_HOTKEY = 0x0312;
//             switch (m.Msg)
//             {
//                 case WM_HOTKEY:
//                     switch (m.WParam.ToInt32())
//                     {
//                         case 101:
//                             ArrowKeyPressed(1);
//                             break;
//                         case 102:
//                             ArrowKeyPressed(2);
//                             break;
//                         case 103:
//                             ArrowKeyPressed(3);
//                             break;
//                         case 104:
//                             ArrowKeyPressed(4);
//                             break;
//                     }
//                     break;
//             }
//             base.WndProc(ref m);
//         }
#endregion

        private void ReturnToday_Click(object sender, EventArgs e)
        {
            cColumn = -1; cRow = -1;
            displayDate = DateTime.Now;
            CheckDateChange();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                    ArrowKeyPressed(1);
                    break;
                case Keys.Right:
                    ArrowKeyPressed(2);
                    break;
                case Keys.Down:
                    ArrowKeyPressed(3);
                    break;
                case Keys.Left:
                    ArrowKeyPressed(4);
                    break;
                default:
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
            //YXIS.TaskBar.RuningTask.CreateInstance().Remove(YXIS.TaskBar.RuningTask.CreateInstance().GetTaskName(2), "Calendar"); 
        }
        #region 绘制背景
        private void Calendar_Paint(object sender, PaintEventArgs e)
        {

            if (this.displayDate != null)
            {
                Font f = new Font("黑体", 100, FontStyle.Bold);
                SolidBrush brush = new SolidBrush(Color.FromArgb(200, Color.FromArgb(0xE0E0E0)));
                Point pt = new Point(0, 0);
                e.Graphics.DrawString(this.displayDate.ToString("yyyy年"), f, brush, pt);
                pt.Y += 150; pt.X += 200;
                e.Graphics.DrawString(this.displayDate.ToString("M月"), f, brush, pt);
                brush.Color = Color.FromArgb(160, 160, 160);
                Pen broder = new Pen(brush, 1);
                Point ptEnd = new Point();

                //开始画行线
                pt.X = 0; pt.Y = 0;
                ptEnd.X = pt.X + MainPanel.Width; ptEnd.Y = 0;
                e.Graphics.DrawLine(broder, pt, ptEnd);
                pt.Y += 61; ptEnd.Y += 61;
                e.Graphics.DrawLine(broder, pt, ptEnd);
                pt.Y += 61; ptEnd.Y += 61;
                e.Graphics.DrawLine(broder, pt, ptEnd);
                pt.Y += 61; ptEnd.Y += 61;
                e.Graphics.DrawLine(broder, pt, ptEnd);
                pt.Y += 61; ptEnd.Y += 61;
                e.Graphics.DrawLine(broder, pt, ptEnd);
                pt.Y += 61; ptEnd.Y += 61;
                e.Graphics.DrawLine(broder, pt, ptEnd);
                pt.Y += 54; ptEnd.Y += 54;
                e.Graphics.DrawLine(broder, pt, ptEnd);
                //开始画列线
                pt.X = 0; pt.Y = 0;
                ptEnd.X = 0; ptEnd.Y = 0 + MainPanel.Height;
                e.Graphics.DrawLine(broder, pt, ptEnd);
                pt.X += 70; ptEnd.X += 70;
                e.Graphics.DrawLine(broder, pt, ptEnd);
                pt.X += 72; ptEnd.X += 72;
                e.Graphics.DrawLine(broder, pt, ptEnd);
                pt.X += 72; ptEnd.X += 72;
                e.Graphics.DrawLine(broder, pt, ptEnd);
                pt.X += 72; ptEnd.X += 72;
                e.Graphics.DrawLine(broder, pt, ptEnd);
                pt.X += 72; ptEnd.X += 72;
                e.Graphics.DrawLine(broder, pt, ptEnd);
                pt.X += 72; ptEnd.X += 72;
                e.Graphics.DrawLine(broder, pt, ptEnd);
                pt.X += 66; ptEnd.X += 66;
                e.Graphics.DrawLine(broder, pt, ptEnd);
            }
        }
        #endregion

        private void calendarCell20_MouseEnter(object sender, EventArgs e)
        {

        }

        #endregion

        private void Calendar_MouseDown(object sender, MouseEventArgs e)
        {
            this.Activate();
            this.Focus();
        }


    }
}
