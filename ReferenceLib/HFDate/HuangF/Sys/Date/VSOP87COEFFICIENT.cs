namespace HuangF.Sys.Date
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct VSOP87COEFFICIENT
    {
        public double dA;
        public double dB;
        public double dC;
        public VSOP87COEFFICIENT(double a, double b, double c)
        {
            this.dA = a;
            this.dB = b;
            this.dC = c;
        }
    }
}

