namespace HuangF.Sys.Date
{
    using System;

    [Serializable]
    public sealed class DiZhi
    {
        private readonly DiZhiTypes id;
        private static string[] names = new string[] { "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥" };

        public DiZhi()
        {
            this.id = DiZhiTypes.Zi;
        }

        public DiZhi(DiZhiTypes dz)
        {
            this.id = dz;
        }

        private static string GetName(DiZhiTypes adz)
        {
            int num = (int) adz;
            return names[num - 1];
        }

        private DiZhiTypes inc(DiZhiTypes adz, int i)
        {
            int num = i % 12;
            if (num == 0)
            {
                return adz;
            }
            int num2 = ((int) adz) + num;
            if (num2 > 12)
            {
                num2 -= 12;
            }
            else if (num2 < 1)
            {
                num2 += 12;
            }
            return (DiZhiTypes) num2;
        }

        public DiZhi Inc(int i)
        {
            return new DiZhi(this.inc(this.id, i));
        }

        public DiZhi Next()
        {
            return this.Inc(1);
        }

        public DiZhi Prior()
        {
            return this.Inc(-1);
        }

        public DiZhiTypes ID
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

