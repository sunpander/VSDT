namespace HuangF.Sys.Date
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct NUTATIONCOEFFICIENT
    {
        public int nD;
        public int nM;
        public int nMprime;
        public int nF;
        public int nOmega;
        public int nSincoeff1;
        public double dSincoeff2;
        public int nCoscoeff1;
        public double dCoscoeff2;
        public NUTATIONCOEFFICIENT(int d, int m, int mp, int nf, int no, int ns, double ds, int nc, double dc)
        {
            this.nD = d;
            this.nM = m;
            this.nMprime = mp;
            this.nF = nf;
            this.nOmega = no;
            this.nSincoeff1 = ns;
            this.dSincoeff2 = ds;
            this.nCoscoeff1 = nc;
            this.dCoscoeff2 = dc;
        }
    }
}

