namespace HuangF.Sys.Date
{
    using System;

    [Serializable]
    public sealed class TianGan
    {
        private readonly TianGanTypes id;

        public TianGan()
        {
            this.id = TianGanTypes.Jia;
        }

        public TianGan(TianGanTypes aid)
        {
            this.id = aid;
        }

        public TianGan(int aid)
        {
            int num = aid % 10;
            if (num > 0)
            {
                this.id = (TianGanTypes) num;
            }
            else if (num < 0)
            {
                this.id = (TianGanTypes) (10 + num);
            }
            else
            {
                this.id = TianGanTypes.Gui;
            }
        }

        private static string GetName(TianGanTypes atg)
        {
            string[] strArray = new string[] { "甲", "乙", "丙", "丁", "戊", "己", "庚", "辛", "壬", "癸" };
            int num = (int) atg;
            return strArray[num - 1];
        }

        private TianGanTypes inc(TianGanTypes atg, int i)
        {
            int num = i % 10;
            if (num == 0)
            {
                return atg;
            }
            int num2 = ((int) atg) + num;
            if (num2 > 10)
            {
                num2 -= 10;
            }
            else if (num2 < 1)
            {
                num2 += 10;
            }
            return (TianGanTypes) num2;
        }

        public TianGan Inc(int i)
        {
            return new TianGan(this.inc(this.id, i));
        }

        public TianGan Next()
        {
            return this.Inc(1);
        }

        public TianGan Prior()
        {
            return this.Inc(-1);
        }

        public TianGanTypes ID
        {
            get
            {
                return this.id;
            }
        }

        public int IntValue
        {
            get
            {
                return (int) this.id;
            }
        }

        public string Name
        {
            get
            {
                return GetName(this.id);
            }
        }
    }
}

