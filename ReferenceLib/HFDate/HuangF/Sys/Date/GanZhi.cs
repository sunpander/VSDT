namespace HuangF.Sys.Date
{
    using System;

    [Serializable]
    public class GanZhi
    {
        private readonly DiZhi _dz;
        private readonly TianGan _tg;

        public GanZhi()
        {
            this._tg = new TianGan(TianGanTypes.Jia);
            this._dz = new DiZhi(DiZhiTypes.Zi);
        }

        public GanZhi(TianGan atg, DiZhi adz)
        {
            this._tg = atg;
            this._dz = adz;
        }

        public GanZhi(TianGanTypes atg, DiZhiTypes adz)
        {
            this._tg = new TianGan(atg);
            this._dz = new DiZhi(adz);
        }

        public GanZhi Inc(int i)
        {
            TianGan atg = this._tg.Inc(i);
            return new GanZhi(atg, this._dz.Inc(i));
        }

        public TianGan Gan
        {
            get
            {
                return this._tg;
            }
        }

        public string Name
        {
            get
            {
                return (this._tg.Name + this._dz.Name);
            }
        }

        public DiZhi Zhi
        {
            get
            {
                return this._dz;
            }
        }
    }
}

