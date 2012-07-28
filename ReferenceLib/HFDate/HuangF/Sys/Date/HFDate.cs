namespace HuangF.Sys.Date
{
    using System;

    public class HFDate
    {
        private static byte[] CnData = new byte[] { 
            11, 0x52, 0xba, 0, 0x16, 0xa9, 0x5d, 0, 0x83, 0xa9, 0x37, 5, 14, 0x74, 0x9b, 0, 
            0x1a, 0xb6, 0x55, 0, 0x87, 0xb5, 0x55, 4, 0x11, 0x55, 170, 0, 0x1c, 0xa6, 0xb5, 0, 
            0x8a, 0xa5, 0x75, 2, 20, 0x52, 0xba, 0, 0x81, 0x52, 110, 6, 13, 0xe9, 0x37, 0, 
            0x18, 0x74, 0x97, 0, 0x86, 0xea, 150, 5, 0x10, 0x6d, 0x55, 0, 0x1a, 0x35, 170, 0, 
            0x88, 0x4b, 0x6a, 2, 0x13, 0xa5, 0x6d, 0, 30, 210, 110, 7, 11, 210, 0x5e, 0, 
            0x17, 0xe9, 0x2e, 0, 0x84, 0xd9, 0x2d, 5, 15, 0xda, 0x95, 0, 0x19, 0x5b, 0x52, 0, 
            0x87, 0x56, 0xd4, 4, 0x11, 0x4a, 0xda, 0, 0x1c, 0xa5, 0x5d, 0, 0x89, 0xa4, 0xbd, 2, 
            0x15, 210, 0x5d, 0, 130, 0xb2, 0x5b, 6, 13, 0xb5, 0x2b, 0, 0x18, 0xba, 0x95, 0, 
            0x86, 0xb6, 0xa5, 5, 0x10, 0x56, 180, 0, 0x1a, 0x4a, 0xda, 0, 0x87, 0x49, 0xba, 3, 
            0x13, 0xa4, 0xbb, 0, 30, 0xb2, 0x5b, 7, 11, 0x72, 0x57, 0, 0x16, 0x75, 0x2b, 0, 
            0x84, 0x6d, 0x2a, 6, 15, 0xad, 0x55, 0, 0x19, 0x55, 170, 0, 0x86, 0x55, 0x6c, 4, 
            0x12, 0xc9, 0x76, 0, 0x1c, 100, 0xb7, 0, 0x8a, 0xe4, 0xae, 2, 0x15, 0xea, 0x56, 0, 
            0x83, 0xda, 0x55, 7, 13, 0x5b, 0x2a, 0, 0x18, 0xad, 0x55, 0, 0x85, 170, 0xd5, 5, 
            0x10, 0x53, 0x6a, 0, 0x1b, 0xa9, 0x6d, 0, 0x88, 0xa9, 0x5d, 3, 0x13, 0xd4, 0xae, 0, 
            0x81, 0xd4, 0xab, 8, 12, 0xba, 0x55, 0, 0x16, 90, 170, 0, 0x83, 0x56, 170, 6, 
            15, 170, 0xd5, 0, 0x19, 0x52, 0xda, 0, 0x86, 0x52, 0xba, 4, 0x11, 0xa9, 0x5d, 0, 
            0x1d, 0xd4, 0x9b, 0, 0x8a, 0x74, 0x9b, 3, 0x15, 0xb6, 0x55, 0, 130, 0xad, 0x55, 7, 
            13, 0x55, 170, 0, 0x18, 0xa5, 0xb5, 0, 0x85, 0xa5, 0x75, 5, 15, 0x52, 0xb6, 0, 
            0x1b, 0x69, 0x37, 0, 0x89, 0xe9, 0x37, 4, 0x13, 0x74, 0x97, 0, 0x81, 0xea, 150, 8, 
            12, 0x6d, 0x52, 0, 0x16, 0x2d, 170, 0, 0x83, 0x4b, 0x6a, 6, 14, 0xa5, 0x6d, 0, 
            0x1a, 210, 110, 0, 0x87, 210, 0x5e, 4, 0x12, 0xe9, 0x2e, 0, 0x1d, 0xec, 150, 10, 
            11, 0xda, 0x95, 0, 0x15, 0x5b, 0x52, 0, 130, 0x56, 210, 6, 12, 0x2a, 0xda, 0, 
            0x18, 0xa4, 0xdd, 0, 0x85, 0xa4, 0xbd, 5, 0x10, 210, 0x5d, 0, 0x1b, 0xd9, 0x2d, 0, 
            0x89, 0xb5, 0x2b, 3, 20, 0xba, 0x95, 0, 0x81, 0xb5, 0x95, 8, 11, 0x56, 0xb2, 0, 
            0x16, 0x2a, 0xda, 0, 0x83, 0x49, 0xb6, 5, 14, 100, 0xbb, 0, 0x19, 0xb2, 0x5b, 0, 
            0x87, 0x6a, 0x57, 4, 0x12, 0x75, 0x2b, 0, 0x1d, 0xb6, 0x95, 0, 0x8a, 0xad, 0x55, 2, 
            0x15, 0x55, 170, 0, 130, 0x55, 0x6c, 7, 13, 0xc9, 0x76, 0, 0x17, 100, 0xb7, 0, 
            0x86, 0xe4, 0xae, 5, 0x11, 0xea, 0x56, 0, 0x1b, 0x6d, 0x2a, 0, 0x88, 90, 170, 4, 
            20, 0xad, 0x55, 0, 0x81, 170, 0xd5, 9, 11, 0x52, 0xea, 0, 0x16, 0xa9, 0x6d, 0, 
            0x84, 0xa9, 0x5d, 6, 15, 0xd4, 0xae, 0, 0x1a, 0xea, 0x4d, 0, 0x87, 0xba, 0x55, 4, 
            0x12, 90, 170, 0, 0x1d, 0xab, 0x55, 0, 0x8a, 0xa6, 0xd5, 2, 20, 0x52, 0xda, 0, 
            130, 0x52, 0xba, 6, 13, 0xa9, 0x3b, 0, 0x18, 180, 0x9b, 0, 0x85, 0x74, 0x9b, 5, 
            0x11, 0xb5, 0x4d, 0, 0x1c, 0xd6, 0xa9, 0, 0x88, 0x35, 170, 3, 0x13, 0xa5, 0xb5, 0, 
            0x81, 0xa5, 0x75, 11, 11, 0x52, 0xb6, 0, 0x16, 0x69, 0x37, 0, 0x84, 0xe9, 0x2f, 6, 
            0x10, 0xf4, 0x97, 0, 0x1a, 0x75, 0x4b, 0, 0x87, 0x6d, 0x52, 5, 0x11, 0x2d, 0x69, 0, 
            0x1d, 0x95, 0xb5, 0, 0x8a, 0xa5, 0x6d, 2, 0x15, 210, 110, 0, 130, 210, 0x5e, 7, 
            14, 0xe9, 0x2e, 0, 0x19, 0xea, 150, 0, 0x86, 0xda, 0x95, 5, 0x10, 0x5b, 0x4a, 0, 
            0x1c, 0xab, 0x69, 0, 0x88, 0x2a, 0xd8, 3
         };
        private static string[] CNDays = new string[] { 
            "初一", "初二", "初三", "初四", "初五", "初六", "初七", "初八", "初九", "初十", "十一", "十二", "十三", "十四", "十五", "十六", 
            "十七", "十八", "十九", "二十", "廿一", "廿二", "廿三", "廿四", "廿五", "廿六", "廿七", "廿八", "廿九", "三十"
         };
        private static string[] CNMonths = new string[] { "正", "二", "三", "四", "五", "六", "七", "八", "九", "十", "冬", "腊" };
        private static string[] CNNumber = new string[] { "○", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
        private DateTime date;

        public HFDate()
        {
            this.date = DateTime.Now;
        }

        public HFDate(DateTime dt)
        {
            this.date = dt;
        }

        public HFDate(int y, int m, int d)
        {
            this.date = new DateTime(y, m, d);
        }

        public HFDate(int y, int mon, int d, int h, int m, int s, int ms)
        {
            this.date = new DateTime(y, m, d, h, m, s, ms);
        }

        public HFDate AddDays(int d)
        {
            return new HFDate(this.Value.AddDays((double) d));
        }

        private static string GetCNWeekStr(int i)
        {
            string[] strArray = new string[] { "日", "一", "二", "三", "四", "五", "六" };
            return strArray[i];
        }

        private TianGan GetDayGan(DateTime dt)
        {
            DateTime time = new DateTime(0x7d0, 2, 5, 12, 0, 0, 0);
            DateTime time2 = new DateTime(dt.Year, dt.Month, dt.Day, 12, 0, 0, 0);
            TimeSpan span = (TimeSpan) (time2 - time);
            int days = span.Days;
            TianGan gan = new TianGan(TianGanTypes.Gui);
            return gan.Inc(days);
        }

        private DiZhi GetDayZhi(DateTime dt)
        {
            DateTime time = new DateTime(0x7d0, 2, 5, 12, 0, 0, 0);
            DateTime time2 = new DateTime(dt.Year, dt.Month, dt.Day, 12, 0, 0, 0);
            TimeSpan span = (TimeSpan) (time2 - time);
            int days = span.Days;
            DiZhi zhi = new DiZhi(DiZhiTypes.Si);
            return zhi.Inc(days);
        }

        private int GetLunarCalendarDayInt()
        {
            return Math.Abs((int) (LunarCalendarDate(this.Value) % 100));
        }

        private int GetLunarCalendarMonthInt()
        {
            return (LunarCalendarDate(this.Value) / 100);
        }

        private GanZhi GetLunarDay()
        {
            TianGan dayGan = this.GetDayGan(this.Value);
            return new GanZhi(dayGan, this.GetDayZhi(this.Value));
        }

        private GanZhi GetLunarMonth()
        {
            int lunarCalendarMonth = this.LunarCalendarMonth;
            GanZhi lunarYear = this.LunarYear;
            TianGan monthGanByYear = this.GetMonthGanByYear(lunarYear.Gan.ID, lunarCalendarMonth);
            return new GanZhi(monthGanByYear, this.MonthToDiZhi(lunarCalendarMonth));
        }

        private GanZhi GetLunarTime()
        {
            GanZhi lunarDay = this.LunarDay;
            DiZhi adz = this.HourToDiZhi(this.Value.Hour);
            return new GanZhi(this.GetTimeGan(lunarDay.Gan.ID, adz.ID), adz);
        }

        private GanZhi GetLunarYear()
        {
            int year = this.Value.Year;
            DateTime springFestival = GetSpringFestival(this.Value.Year);
            DateTime time2 = new DateTime(this.Value.Year, this.Value.Month, this.Value.Day, 0, 0, 0, 0);
            if (time2 < springFestival)
            {
                year--;
            }
            TianGan yearTianGan = this.GetYearTianGan(year);
            return new GanZhi(yearTianGan, this.GetYearDiZhi(year));
        }

        private TianGan GetMonthGanByYear(TianGanTypes ng, int m1)
        {
            TianGanTypes bing;
            int num = Math.Abs(m1);
            if ((ng == TianGanTypes.Jia) || (ng == TianGanTypes.Ji))
            {
                bing = TianGanTypes.Bing;
            }
            else if ((ng == TianGanTypes.Yi) || (ng == TianGanTypes.Geng))
            {
                bing = TianGanTypes.Wu;
            }
            else if ((ng == TianGanTypes.Bing) || (ng == TianGanTypes.Xin))
            {
                bing = TianGanTypes.Geng;
            }
            else if ((ng == TianGanTypes.Ding) || (ng == TianGanTypes.Ren))
            {
                bing = TianGanTypes.Ren;
            }
            else
            {
                bing = TianGanTypes.Jia;
            }
            TianGan gan = new TianGan(bing);
            return gan.Inc(num - 1);
        }

        public static DateTime GetSpringFestival(int y)
        {
            DateTime time = new DateTime(y, 1, 1, 0, 0, 0, 0);
            while (true)
            {
                int num = Math.Abs((int) (LunarCalendarDate(time) / 100));
                int num2 = Math.Abs(LunarCalendarDate(time)) % 100;
                if ((num == 1) && (num2 == 1))
                {
                    return time;
                }
                time = time.AddDays(1.0);
            }
        }

        private TianGan GetTimeGan(TianGanTypes rg, DiZhiTypes sc)
        {
            TianGanTypes jia;
            int i = ((int) sc) - 1;
            if ((rg == TianGanTypes.Jia) || (rg == TianGanTypes.Ji))
            {
                jia = TianGanTypes.Jia;
            }
            else if ((rg == TianGanTypes.Yi) || (rg == TianGanTypes.Geng))
            {
                jia = TianGanTypes.Bing;
            }
            else if ((rg == TianGanTypes.Bing) || (rg == TianGanTypes.Xin))
            {
                jia = TianGanTypes.Wu;
            }
            else if ((rg == TianGanTypes.Ding) || (rg == TianGanTypes.Ren))
            {
                jia = TianGanTypes.Geng;
            }
            else
            {
                jia = TianGanTypes.Ren;
            }
            TianGan gan = new TianGan(jia);
            return gan.Inc(i);
        }

        private DiZhi GetYearDiZhi(int y)
        {
            int i = y - 0x7d0;
            DiZhi zhi = new DiZhi(DiZhiTypes.Chen);
            if (i == 0)
            {
                return zhi;
            }
            return zhi.Inc(i);
        }

        private TianGan GetYearTianGan(int y)
        {
            int i = y - 0x7d0;
            TianGan gan = new TianGan(TianGanTypes.Geng);
            if (i == 0)
            {
                return gan;
            }
            return gan.Inc(i);
        }

        private DiZhi HourToDiZhi(int h)
        {
            DiZhiTypes chou;
            if ((h >= 1) && (h < 3))
            {
                chou = DiZhiTypes.Chou;
            }
            else if ((h >= 3) && (h < 5))
            {
                chou = DiZhiTypes.Yin;
            }
            else if ((h >= 5) && (h < 7))
            {
                chou = DiZhiTypes.Mao;
            }
            else if ((h >= 7) && (h < 9))
            {
                chou = DiZhiTypes.Chen;
            }
            else if ((h >= 9) && (h < 11))
            {
                chou = DiZhiTypes.Si;
            }
            else if ((h >= 11) && (h < 13))
            {
                chou = DiZhiTypes.Wu;
            }
            else if ((h >= 13) && (h < 15))
            {
                chou = DiZhiTypes.Wei;
            }
            else if ((h >= 15) && (h < 0x11))
            {
                chou = DiZhiTypes.Shen;
            }
            else if ((h >= 0x11) && (h < 0x13))
            {
                chou = DiZhiTypes.You;
            }
            else if ((h >= 0x13) && (h < 0x15))
            {
                chou = DiZhiTypes.Xu;
            }
            else if ((h >= 0x15) && (h < 0x17))
            {
                chou = DiZhiTypes.Hai;
            }
            else
            {
                chou = DiZhiTypes.Zi;
            }
            return new DiZhi(chou);
        }

        private static int LunarCalendarDate(DateTime dt1)
        {
            DateTime time;
            int num6;
            int num7;
            if (dt1.Hour == 0x17)
            {
                time = dt1.AddDays(1.0);
            }
            else
            {
                time = dt1;
            }
            int[] numArray = new int[0x10];
            int[] numArray2 = new int[0x10];
            byte[] buffer = new byte[4];
            if ((time.Year < 0x76d) || (time.Year > 0x802))
            {
                return 0;
            }
            buffer[0] = CnData[(time.Year - 0x76d) * 4];
            buffer[1] = CnData[((time.Year - 0x76d) * 4) + 1];
            buffer[2] = CnData[((time.Year - 0x76d) * 4) + 2];
            buffer[3] = CnData[((time.Year - 0x76d) * 4) + 3];
            if ((buffer[0] & 0x80) != 0)
            {
                numArray[0] = 12;
            }
            else
            {
                numArray[0] = 11;
            }
            int num = buffer[0] & 0x7f;
            int num3 = buffer[1];
            num3 = num3 << 8;
            num3 |= buffer[2];
            int num2 = buffer[3];
            for (int i = 15; i > 0; i--)
            {
                numArray2[15 - i] = 0x1d;
                if (((((int) 1) << i) & num3) != 0)
                {
                    numArray2[15 - i]++;
                }
                if (numArray[15 - i] == num2)
                {
                    numArray[(15 - i) + 1] = -1 * num2;
                }
                else
                {
                    if (numArray[15 - i] < 0)
                    {
                        numArray[(15 - i) + 1] = (-1 * numArray[15 - i]) + 1;
                    }
                    else
                    {
                        numArray[(15 - i) + 1] = numArray[15 - i] + 1;
                    }
                    if (numArray[(15 - i) + 1] > 12)
                    {
                        numArray[(15 - i) + 1] = 1;
                    }
                }
            }
            int num4 = time.DayOfYear - 1;
            if (num4 <= (numArray2[0] - num))
            {
                if ((time.Year > 0x76d) && (LunarCalendarDate(new DateTime(time.Year - 1, 12, 0x1f)) < 0))
                {
                    num6 = -1 * numArray[0];
                }
                else
                {
                    num6 = numArray[0];
                }
                num7 = num + num4;
            }
            else
            {
                int num5 = numArray2[0] - num;
                int index = 1;
                while ((num5 < num4) && ((num5 + numArray2[index]) < num4))
                {
                    num5 += numArray2[index];
                    index++;
                }
                num6 = numArray[index];
                num7 = num4 - num5;
            }
            if (num6 > 0)
            {
                return ((num6 * 100) + num7);
            }
            return ((num6 * 100) - num7);
        }

        private DiZhi MonthToDiZhi(int m)
        {
            int num = Math.Abs(m) + 2;
            if (num > 12)
            {
                num -= 12;
            }
            return new DiZhi((DiZhiTypes) num);
        }

        public string LongCNWeek
        {
            get
            {
                return ("星期" + this.ShortCNWeek);
            }
        }

        public string LongLunarCalendarDate
        {
            get
            {
                return (this.LunarYear.Name + '年' + this.ShortLunarCalendarDate);
            }
        }

        public int LunarCalendarDay
        {
            get
            {
                return this.GetLunarCalendarDayInt();
            }
        }

        public string LunarCalendarDayString
        {
            get
            {
                int lunarCalendarDayInt = this.GetLunarCalendarDayInt();
                return CNDays[lunarCalendarDayInt - 1];
            }
        }

        public int LunarCalendarMonth
        {
            get
            {
                return this.GetLunarCalendarMonthInt();
            }
        }

        public string LunarCalendarMonthString
        {
            get
            {
                int lunarCalendarMonth = this.LunarCalendarMonth;
                if (lunarCalendarMonth < 0)
                {
                    return ("闰" + CNMonths[(-1 * lunarCalendarMonth) - 1] + "月");
                }
                return (CNMonths[lunarCalendarMonth - 1] + "月");
            }
        }

        public GanZhi LunarDay
        {
            get
            {
                return this.GetLunarDay();
            }
        }

        public GanZhi LunarMonth
        {
            get
            {
                return this.GetLunarMonth();
            }
        }

        public GanZhi LunarTime
        {
            get
            {
                return this.GetLunarTime();
            }
        }

        public GanZhi LunarYear
        {
            get
            {
                return this.GetLunarYear();
            }
        }

        public string ShortCNWeek
        {
            get
            {
                return GetCNWeekStr((int) this.Value.DayOfWeek);
            }
        }

        public string ShortLunarCalendarDate
        {
            get
            {
                return (this.LunarCalendarMonthString + this.LunarCalendarDayString);
            }
        }

        public string SolarTermInfo
        {
            get
            {
                return SolarTerm24.l_GetLunarHolDay(this.Value);
            }
        }

        public DateTime Value
        {
            get
            {
                return this.date;
            }
        }
    }
}

