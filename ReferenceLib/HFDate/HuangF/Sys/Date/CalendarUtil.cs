using System;
using System.Runtime.InteropServices;

namespace HuangF.Sys.Date
{
    internal static class CalendarUtil
    {
        private const double dbUnitRadian = 0.017453292519943295;
        private static VSOP87COEFFICIENT[] Earth_SLG0 = new VSOP87COEFFICIENT[] { 
            new VSOP87COEFFICIENT(175347046.0, 0.0, 0.0), new VSOP87COEFFICIENT(3341656.0, 4.6692568, 6283.07585), new VSOP87COEFFICIENT(34894.0, 4.6261, 12566.1517), new VSOP87COEFFICIENT(3497.0, 2.7441, 5753.3849), new VSOP87COEFFICIENT(3418.0, 2.8289, 3.5231), new VSOP87COEFFICIENT(3136.0, 3.6277, 77713.7715), new VSOP87COEFFICIENT(2676.0, 4.4181, 7860.4194), new VSOP87COEFFICIENT(2343.0, 6.1352, 3930.2097), new VSOP87COEFFICIENT(1324.0, 0.7425, 11506.7698), new VSOP87COEFFICIENT(1273.0, 2.0371, 529.691), new VSOP87COEFFICIENT(1199.0, 1.1096, 1577.3435), new VSOP87COEFFICIENT(990.0, 5.233, 5884.927), new VSOP87COEFFICIENT(902.0, 2.045, 26.298), new VSOP87COEFFICIENT(857.0, 3.508, 398.149), new VSOP87COEFFICIENT(780.0, 1.179, 5223.694), new VSOP87COEFFICIENT(753.0, 2.533, 5507.553), 
            new VSOP87COEFFICIENT(505.0, 4.583, 18849.228), new VSOP87COEFFICIENT(492.0, 4.205, 775.523), new VSOP87COEFFICIENT(357.0, 2.92, 0.067), new VSOP87COEFFICIENT(317.0, 5.849, 11790.629), new VSOP87COEFFICIENT(284.0, 1.899, 796.288), new VSOP87COEFFICIENT(271.0, 0.315, 10977.079), new VSOP87COEFFICIENT(243.0, 0.345, 5486.778), new VSOP87COEFFICIENT(206.0, 4.806, 2544.314), new VSOP87COEFFICIENT(205.0, 1.869, 5573.143), new VSOP87COEFFICIENT(202.0, 2.458, 6069.777), new VSOP87COEFFICIENT(156.0, 0.833, 213.299), new VSOP87COEFFICIENT(132.0, 3.411, 2942.463), new VSOP87COEFFICIENT(126.0, 1.083, 20.775), new VSOP87COEFFICIENT(115.0, 0.645, 0.98), new VSOP87COEFFICIENT(103.0, 0.636, 4694.003), new VSOP87COEFFICIENT(102.0, 0.976, 15720.839), 
            new VSOP87COEFFICIENT(102.0, 4.267, 7.114), new VSOP87COEFFICIENT(99.0, 6.21, 2146.17), new VSOP87COEFFICIENT(98.0, 0.68, 155.42), new VSOP87COEFFICIENT(86.0, 5.98, 161000.69), new VSOP87COEFFICIENT(85.0, 1.3, 6275.96), new VSOP87COEFFICIENT(85.0, 3.67, 71430.7), new VSOP87COEFFICIENT(80.0, 1.81, 17260.15), new VSOP87COEFFICIENT(79.0, 3.04, 12036.46), new VSOP87COEFFICIENT(75.0, 1.76, 5088.63), new VSOP87COEFFICIENT(74.0, 3.5, 3154.69), new VSOP87COEFFICIENT(74.0, 4.68, 801.82), new VSOP87COEFFICIENT(70.0, 0.83, 9437.76), new VSOP87COEFFICIENT(62.0, 3.98, 8827.39), new VSOP87COEFFICIENT(61.0, 1.82, 7084.9), new VSOP87COEFFICIENT(57.0, 2.78, 6286.6), new VSOP87COEFFICIENT(56.0, 4.39, 14143.5), 
            new VSOP87COEFFICIENT(56.0, 3.47, 6279.55), new VSOP87COEFFICIENT(52.0, 0.19, 12139.55), new VSOP87COEFFICIENT(52.0, 1.33, 1748.02), new VSOP87COEFFICIENT(51.0, 0.28, 5856.48), new VSOP87COEFFICIENT(49.0, 0.49, 1194.45), new VSOP87COEFFICIENT(41.0, 5.37, 8429.24), new VSOP87COEFFICIENT(41.0, 2.4, 19651.05), new VSOP87COEFFICIENT(39.0, 6.17, 10447.39), new VSOP87COEFFICIENT(37.0, 6.04, 10213.29), new VSOP87COEFFICIENT(37.0, 2.57, 1059.38), new VSOP87COEFFICIENT(36.0, 1.71, 2352.87), new VSOP87COEFFICIENT(36.0, 1.78, 6812.77), new VSOP87COEFFICIENT(33.0, 0.59, 17789.85), new VSOP87COEFFICIENT(30.0, 0.44, 83996.85), new VSOP87COEFFICIENT(30.0, 2.74, 1349.87), new VSOP87COEFFICIENT(25.0, 3.16, 4690.48)
         };
        private static VSOP87COEFFICIENT[] Earth_SLG1 = new VSOP87COEFFICIENT[] { 
            new VSOP87COEFFICIENT(628331966747, 0.0, 0.0), new VSOP87COEFFICIENT(206059.0, 2.678235, 6283.07585), new VSOP87COEFFICIENT(4303.0, 2.6351, 12566.1517), new VSOP87COEFFICIENT(425.0, 1.59, 3.523), new VSOP87COEFFICIENT(119.0, 5.796, 26.298), new VSOP87COEFFICIENT(109.0, 2.966, 1577.344), new VSOP87COEFFICIENT(93.0, 2.59, 18849.23), new VSOP87COEFFICIENT(72.0, 1.14, 529.69), new VSOP87COEFFICIENT(68.0, 1.87, 398.15), new VSOP87COEFFICIENT(67.0, 4.41, 5507.55), new VSOP87COEFFICIENT(59.0, 2.89, 5223.69), new VSOP87COEFFICIENT(56.0, 2.17, 155.42), new VSOP87COEFFICIENT(45.0, 0.4, 796.3), new VSOP87COEFFICIENT(36.0, 0.47, 775.52), new VSOP87COEFFICIENT(29.0, 2.65, 7.11), new VSOP87COEFFICIENT(21.0, 5.43, 0.98), 
            new VSOP87COEFFICIENT(19.0, 1.85, 5486.78), new VSOP87COEFFICIENT(19.0, 4.97, 213.3), new VSOP87COEFFICIENT(17.0, 2.99, 6275.96), new VSOP87COEFFICIENT(16.0, 0.03, 2544.31), new VSOP87COEFFICIENT(16.0, 1.43, 2146.17), new VSOP87COEFFICIENT(15.0, 1.21, 10977.08), new VSOP87COEFFICIENT(12.0, 2.83, 1748.02), new VSOP87COEFFICIENT(12.0, 3.26, 5088.63), new VSOP87COEFFICIENT(12.0, 5.27, 1194.45), new VSOP87COEFFICIENT(12.0, 2.08, 4694.0), new VSOP87COEFFICIENT(11.0, 0.77, 553.57), new VSOP87COEFFICIENT(10.0, 1.3, 6286.6), new VSOP87COEFFICIENT(10.0, 4.24, 1349.87), new VSOP87COEFFICIENT(9.0, 2.7, 242.73), new VSOP87COEFFICIENT(9.0, 5.64, 951.72), new VSOP87COEFFICIENT(8.0, 5.3, 2352.87), 
            new VSOP87COEFFICIENT(6.0, 2.65, 9437.76), new VSOP87COEFFICIENT(6.0, 4.67, 4690.48)
         };
        private static VSOP87COEFFICIENT[] Earth_SLG2 = new VSOP87COEFFICIENT[] { 
            new VSOP87COEFFICIENT(52919.0, 0.0, 0.0), new VSOP87COEFFICIENT(8720.0, 1.0721, 6283.0758), new VSOP87COEFFICIENT(309.0, 0.867, 12566.152), new VSOP87COEFFICIENT(27.0, 0.05, 3.52), new VSOP87COEFFICIENT(16.0, 5.19, 26.3), new VSOP87COEFFICIENT(16.0, 3.68, 155.42), new VSOP87COEFFICIENT(10.0, 0.76, 18849.23), new VSOP87COEFFICIENT(9.0, 2.06, 77713.77), new VSOP87COEFFICIENT(7.0, 0.83, 775.52), new VSOP87COEFFICIENT(5.0, 4.66, 1577.34), new VSOP87COEFFICIENT(4.0, 1.03, 7.11), new VSOP87COEFFICIENT(4.0, 3.44, 5573.14), new VSOP87COEFFICIENT(3.0, 5.14, 796.3), new VSOP87COEFFICIENT(3.0, 6.05, 5507.55), new VSOP87COEFFICIENT(3.0, 1.19, 242.73), new VSOP87COEFFICIENT(3.0, 6.12, 529.69), 
            new VSOP87COEFFICIENT(3.0, 0.31, 398.15), new VSOP87COEFFICIENT(3.0, 2.28, 553.57), new VSOP87COEFFICIENT(2.0, 4.38, 5223.69), new VSOP87COEFFICIENT(2.0, 3.75, 0.98)
         };
        private static VSOP87COEFFICIENT[] Earth_SLG3 = new VSOP87COEFFICIENT[] { new VSOP87COEFFICIENT(289.0, 5.844, 6283.076), new VSOP87COEFFICIENT(35.0, 0.0, 0.0), new VSOP87COEFFICIENT(17.0, 5.49, 12566.15), new VSOP87COEFFICIENT(3.0, 5.2, 155.42), new VSOP87COEFFICIENT(1.0, 4.72, 3.52), new VSOP87COEFFICIENT(1.0, 5.3, 18849.23), new VSOP87COEFFICIENT(1.0, 5.97, 242.73) };
        private static VSOP87COEFFICIENT[] Earth_SLG4 = new VSOP87COEFFICIENT[] { new VSOP87COEFFICIENT(114.0, 3.142, 0.0), new VSOP87COEFFICIENT(8.0, 4.13, 6283.08), new VSOP87COEFFICIENT(1.0, 3.84, 12566.15) };
        private static VSOP87COEFFICIENT[] Earth_SLG5 = new VSOP87COEFFICIENT[] { new VSOP87COEFFICIENT(1.0, 3.14, 0.0) };
        private static VSOP87COEFFICIENT[] Earth_SLT0 = new VSOP87COEFFICIENT[] { new VSOP87COEFFICIENT(280.0, 3.199, 84334.662), new VSOP87COEFFICIENT(102.0, 5.422, 5507.553), new VSOP87COEFFICIENT(80.0, 3.88, 5223.69), new VSOP87COEFFICIENT(44.0, 3.7, 2352.87), new VSOP87COEFFICIENT(32.0, 4.0, 1577.34) };
        private static VSOP87COEFFICIENT[] Earth_SLT1 = new VSOP87COEFFICIENT[] { new VSOP87COEFFICIENT(9.0, 3.9, 5507.55), new VSOP87COEFFICIENT(6.0, 1.73, 5223.69) };
        private static VSOP87COEFFICIENT[] Earth_SLT2 = new VSOP87COEFFICIENT[] { new VSOP87COEFFICIENT(22378.0, 3.38509, 10213.28555), new VSOP87COEFFICIENT(282.0, 0.0, 0.0), new VSOP87COEFFICIENT(173.0, 5.256, 20426.571), new VSOP87COEFFICIENT(27.0, 3.87, 30639.86) };
        private static VSOP87COEFFICIENT[] Earth_SLT3 = new VSOP87COEFFICIENT[] { new VSOP87COEFFICIENT(647.0, 4.992, 10213.286), new VSOP87COEFFICIENT(20.0, 3.14, 0.0), new VSOP87COEFFICIENT(6.0, 0.77, 20426.57), new VSOP87COEFFICIENT(3.0, 5.44, 30639.86) };
        private static VSOP87COEFFICIENT[] Earth_SLT4 = new VSOP87COEFFICIENT[] { new VSOP87COEFFICIENT(14.0, 0.32, 10213.29) };
        private static VSOP87COEFFICIENT[] Earth_SRV0 = new VSOP87COEFFICIENT[] { 
            new VSOP87COEFFICIENT(100013989.0, 0.0, 0.0), new VSOP87COEFFICIENT(1670700.0, 3.0984635, 6283.07585), new VSOP87COEFFICIENT(13956.0, 3.05525, 12566.1517), new VSOP87COEFFICIENT(3084.0, 5.1985, 77713.7715), new VSOP87COEFFICIENT(1628.0, 1.1739, 5753.3849), new VSOP87COEFFICIENT(1576.0, 2.8469, 7860.4194), new VSOP87COEFFICIENT(925.0, 5.453, 11506.77), new VSOP87COEFFICIENT(542.0, 4.564, 3930.21), new VSOP87COEFFICIENT(472.0, 3.661, 5884.927), new VSOP87COEFFICIENT(346.0, 0.964, 5507.553), new VSOP87COEFFICIENT(329.0, 5.9, 5223.694), new VSOP87COEFFICIENT(307.0, 0.299, 5573.143), new VSOP87COEFFICIENT(243.0, 4.273, 11790.629), new VSOP87COEFFICIENT(212.0, 5.847, 1577.344), new VSOP87COEFFICIENT(186.0, 5.022, 10977.079), new VSOP87COEFFICIENT(175.0, 3.012, 18849.228), 
            new VSOP87COEFFICIENT(110.0, 5.055, 5486.778), new VSOP87COEFFICIENT(98.0, 0.89, 6069.78), new VSOP87COEFFICIENT(86.0, 5.69, 15720.84), new VSOP87COEFFICIENT(86.0, 1.27, 161000.69), new VSOP87COEFFICIENT(65.0, 0.27, 17260.15), new VSOP87COEFFICIENT(63.0, 0.92, 529.69), new VSOP87COEFFICIENT(57.0, 2.01, 83996.85), new VSOP87COEFFICIENT(56.0, 5.24, 71430.7), new VSOP87COEFFICIENT(49.0, 3.25, 2544.31), new VSOP87COEFFICIENT(47.0, 2.58, 775.52), new VSOP87COEFFICIENT(45.0, 5.54, 9437.76), new VSOP87COEFFICIENT(43.0, 6.01, 6275.96), new VSOP87COEFFICIENT(39.0, 5.36, 4694.0), new VSOP87COEFFICIENT(38.0, 2.39, 8827.39), new VSOP87COEFFICIENT(37.0, 0.83, 19651.05), new VSOP87COEFFICIENT(37.0, 4.9, 12139.55), 
            new VSOP87COEFFICIENT(36.0, 1.67, 12036.46), new VSOP87COEFFICIENT(35.0, 1.84, 2942.46), new VSOP87COEFFICIENT(33.0, 0.24, 7084.9), new VSOP87COEFFICIENT(32.0, 0.18, 5088.63), new VSOP87COEFFICIENT(32.0, 1.78, 398.15), new VSOP87COEFFICIENT(28.0, 1.21, 6286.6), new VSOP87COEFFICIENT(28.0, 1.9, 6279.55), new VSOP87COEFFICIENT(26.0, 4.59, 10447.39)
         };
        private static VSOP87COEFFICIENT[] Earth_SRV1 = new VSOP87COEFFICIENT[] { new VSOP87COEFFICIENT(103019.0, 1.10749, 6283.07585), new VSOP87COEFFICIENT(1721.0, 1.0644, 12566.1517), new VSOP87COEFFICIENT(702.0, 3.142, 0.0), new VSOP87COEFFICIENT(32.0, 1.02, 18849.23), new VSOP87COEFFICIENT(31.0, 2.84, 5507.55), new VSOP87COEFFICIENT(25.0, 1.32, 5223.69), new VSOP87COEFFICIENT(18.0, 1.42, 1577.34), new VSOP87COEFFICIENT(10.0, 5.91, 10977.08), new VSOP87COEFFICIENT(9.0, 1.42, 6275.96), new VSOP87COEFFICIENT(9.0, 0.27, 5486.78) };
        private static VSOP87COEFFICIENT[] Earth_SRV2 = new VSOP87COEFFICIENT[] { new VSOP87COEFFICIENT(4359.0, 5.7846, 6283.0758), new VSOP87COEFFICIENT(124.0, 5.579, 12566.152), new VSOP87COEFFICIENT(12.0, 3.14, 0.0), new VSOP87COEFFICIENT(9.0, 3.63, 77713.77), new VSOP87COEFFICIENT(6.0, 1.87, 5573.14), new VSOP87COEFFICIENT(3.0, 5.47, 18849.23) };
        private static VSOP87COEFFICIENT[] Earth_SRV3 = new VSOP87COEFFICIENT[] { new VSOP87COEFFICIENT(145.0, 4.273, 6283.076), new VSOP87COEFFICIENT(7.0, 3.92, 12566.15) };
        private static VSOP87COEFFICIENT[] Earth_SRV4 = new VSOP87COEFFICIENT[] { new VSOP87COEFFICIENT(4.0, 2.56, 6283.08) };
        private static NUTATIONCOEFFICIENT[] Nutation_Gene = new NUTATIONCOEFFICIENT[] { 
            new NUTATIONCOEFFICIENT(0, 0, 0, 0, 1, -171996, -174.2, 0x16779, 8.9), new NUTATIONCOEFFICIENT(-2, 0, 0, 2, 2, -13187, -1.6, 0x1668, -3.1), new NUTATIONCOEFFICIENT(0, 0, 0, 2, 2, -2274, -0.2, 0x3d1, -0.5), new NUTATIONCOEFFICIENT(0, 0, 0, 0, 2, 0x80e, 0.2, -895, 0.5), new NUTATIONCOEFFICIENT(0, 1, 0, 0, 0, 0x592, -3.4, 0x36, -0.1), new NUTATIONCOEFFICIENT(0, 0, 1, 0, 0, 0x2c8, 0.1, -7, 0.0), new NUTATIONCOEFFICIENT(-2, 1, 0, 2, 2, -517, 1.2, 0xe0, -0.6), new NUTATIONCOEFFICIENT(0, 0, 0, 2, 1, -386, -0.4, 200, 0.0), new NUTATIONCOEFFICIENT(0, 0, 1, 2, 2, -301, 0.0, 0x81, -0.1), new NUTATIONCOEFFICIENT(-2, -1, 0, 2, 2, 0xd9, -0.5, -95, 0.3), new NUTATIONCOEFFICIENT(-2, 0, 1, 0, 0, -158, 0.0, 0, 0.0), new NUTATIONCOEFFICIENT(-2, 0, 0, 2, 1, 0x81, 0.1, -70, 0.0), new NUTATIONCOEFFICIENT(0, 0, -1, 2, 2, 0x7b, 0.0, -53, 0.0), new NUTATIONCOEFFICIENT(2, 0, 0, 0, 0, 0x3f, 0.0, 0, 0.0), new NUTATIONCOEFFICIENT(0, 0, 1, 0, 1, 0x3f, 0.1, -33, 0.0), new NUTATIONCOEFFICIENT(2, 0, -1, 2, 2, -59, 0.0, 0x1a, 0.0), 
            new NUTATIONCOEFFICIENT(0, 0, -1, 0, 1, -58, -0.1, 0x20, 0.0), new NUTATIONCOEFFICIENT(0, 0, 1, 2, 1, -51, 0.0, 0x1b, 0.0), new NUTATIONCOEFFICIENT(-2, 0, 2, 0, 0, 0x30, 0.0, 0, 0.0), new NUTATIONCOEFFICIENT(0, 0, -2, 2, 1, 0x2e, 0.0, -24, 0.0), new NUTATIONCOEFFICIENT(2, 0, 0, 2, 2, -38, 0.0, 0x10, 0.0), new NUTATIONCOEFFICIENT(0, 0, 2, 2, 2, -31, 0.0, 13, 0.0), new NUTATIONCOEFFICIENT(0, 0, 2, 0, 0, 0x1d, 0.0, 0, 0.0), new NUTATIONCOEFFICIENT(-2, 0, 1, 2, 2, 0x1d, 0.0, -12, 0.0), new NUTATIONCOEFFICIENT(0, 0, 0, 2, 0, 0x1a, 0.0, 0, 0.0), new NUTATIONCOEFFICIENT(-2, 0, 0, 2, 0, -22, 0.0, 0, 0.0), new NUTATIONCOEFFICIENT(0, 0, -1, 2, 1, 0x15, 0.0, -10, 0.0), new NUTATIONCOEFFICIENT(0, 2, 0, 0, 0, 0x11, -0.1, 0, 0.0), new NUTATIONCOEFFICIENT(2, 0, -1, 0, 1, 0x10, 0.0, -8, 0.0), new NUTATIONCOEFFICIENT(-2, 2, 0, 2, 2, -16, 0.1, 7, 0.0), new NUTATIONCOEFFICIENT(0, 1, 0, 0, 1, -15, 0.0, 9, 0.0), new NUTATIONCOEFFICIENT(-2, 0, 1, 0, 1, -13, 0.0, 7, 0.0), 
            new NUTATIONCOEFFICIENT(0, -1, 0, 0, 1, -12, 0.0, 6, 0.0), new NUTATIONCOEFFICIENT(0, 0, 2, -2, 0, 11, 0.0, 0, 0.0), new NUTATIONCOEFFICIENT(2, 0, -1, 2, 1, -10, 0.0, 5, 0.0), new NUTATIONCOEFFICIENT(2, 0, 1, 2, 2, -8, 0.0, 3, 0.0), new NUTATIONCOEFFICIENT(0, 1, 0, 2, 2, 7, 0.0, -3, 0.0), new NUTATIONCOEFFICIENT(-2, 1, 1, 0, 0, -7, 0.0, 0, 0.0), new NUTATIONCOEFFICIENT(0, -1, 0, 2, 2, -7, 0.0, 3, 0.0), new NUTATIONCOEFFICIENT(2, 0, 0, 2, 1, -7, 0.0, 3, 0.0), new NUTATIONCOEFFICIENT(2, 0, 1, 0, 0, 6, 0.0, 0, 0.0), new NUTATIONCOEFFICIENT(-2, 0, 2, 2, 2, 6, 0.0, -3, 0.0), new NUTATIONCOEFFICIENT(-2, 0, 1, 2, 1, 6, 0.0, -3, 0.0), new NUTATIONCOEFFICIENT(2, 0, -2, 0, 1, -6, 0.0, 3, 0.0), new NUTATIONCOEFFICIENT(2, 0, 0, 0, 1, -6, 0.0, 3, 0.0), new NUTATIONCOEFFICIENT(0, -1, 1, 0, 0, 5, 0.0, 0, 0.0), new NUTATIONCOEFFICIENT(-2, -1, 0, 2, 1, -5, 0.0, 3, 0.0), new NUTATIONCOEFFICIENT(-2, 0, 0, 0, 1, -5, 0.0, 3, 0.0), 
            new NUTATIONCOEFFICIENT(0, 0, 2, 2, 1, -5, 0.0, 3, 0.0), new NUTATIONCOEFFICIENT(-2, 0, 2, 0, 1, 4, 0.0, 0, 0.0), new NUTATIONCOEFFICIENT(-2, 1, 0, 2, 1, 4, 0.0, 0, 0.0), new NUTATIONCOEFFICIENT(0, 0, 1, -2, 0, 4, 0.0, 0, 0.0), new NUTATIONCOEFFICIENT(-1, 0, 1, 0, 0, -4, 0.0, 0, 0.0), new NUTATIONCOEFFICIENT(-2, 1, 0, 0, 0, -4, 0.0, 0, 0.0), new NUTATIONCOEFFICIENT(1, 0, 0, 0, 0, -4, 0.0, 0, 0.0), new NUTATIONCOEFFICIENT(0, 0, 1, 2, 0, 3, 0.0, 0, 0.0), new NUTATIONCOEFFICIENT(0, 0, -2, 2, 2, -3, 0.0, 0, 0.0), new NUTATIONCOEFFICIENT(-1, -1, 1, 0, 0, -3, 0.0, 0, 0.0), new NUTATIONCOEFFICIENT(0, 1, 1, 0, 0, -3, 0.0, 0, 0.0), new NUTATIONCOEFFICIENT(0, -1, 1, 2, 2, -3, 0.0, 0, 0.0), new NUTATIONCOEFFICIENT(2, -1, -1, 2, 2, -3, 0.0, 0, 0.0), new NUTATIONCOEFFICIENT(0, 0, 3, 2, 2, -3, 0.0, 0, 0.0), new NUTATIONCOEFFICIENT(2, -1, 0, 2, 2, -3, 0.0, 0, 0.0)
         };
        private const double PI = 3.1415926535897931;

        public static void CalcEclipticLongLat(double dbJD, out double dbLongitude, out double dbLatitude)
        {
            dbLongitude = GetSunLongitude(dbJD);
            dbLatitude = GetSunLatitude(dbJD);
            dbLongitude += CorrectionCalcSunLongitude(dbLongitude, dbLatitude, dbJD);
            dbLongitude += GetNutationJamScalar(dbJD) / 3600.0;
            dbLongitude -= (20.4898 / GetSunRadiusVector(dbJD)) / 3600.0;
            dbLatitude += CorrectionCalcSunLatitude(dbLongitude, dbJD);
        }

        public static void CalGD(int julian, out int y, out int m, out int d)
        {
            int num;
            if (julian >= 0x231519)
            {
                int num2 = (int) (((julian - 0x1c7dd0) - 0.25) / 36524.25);
                num = ((julian + 1) + num2) - ((int) (0.25 * num2));
            }
            else if (julian < 0)
            {
                num = julian + (0x8ead * (1 - (julian / 0x8ead)));
            }
            else
            {
                num = julian;
            }
            int num3 = num + 0x5f4;
            int num4 = (int) (6680.0 + (((num3 - 0x253abe) - 122.1) / 365.25));
            int num5 = (0x16d * num4) + ((int) (0.25 * num4));
            int num6 = (int) (((double) (num3 - num5)) / 30.6001);
            d = (num3 - num5) - ((int) (30.6001 * num6));
            m = num6 - 1;
            if (m > 12)
            {
                m -= 12;
            }
            y = num4 - 0x126b;
            if (m > 2)
            {
                y--;
            }
            if (y <= 0)
            {
                y--;
            }
            if (julian < 0)
            {
                y -= 100 * (1 - (julian / 0x8ead));
            }
        }

        public static void CalGD(double julday, out int y, out int m, out int d, out int h, out int min, out int s)
        {
            long num4;
            double v = julday + 0.5;
            long num2 = (long) Floor(v);
            double num3 = v - Floor(v);
            if (julday < 2299161.0)
            {
                num4 = num2;
            }
            else
            {
                int num9 = (int) ((num2 - 1867216.25) / 36524.25);
                num4 = ((num2 + 1L) + num9) - (num9 >> 2);
            }
            long num5 = num4 + 0x5f4L;
            long num6 = (int) ((num5 - 122.1) / 365.25);
            long num7 = (int) (365.25 * num6);
            int num8 = (int) (((double) (num5 - num7)) / 30.6001);
            d = ((int) (num5 - num7)) - ((int) (30.6001 * num8));
            h = (int) Floor(num3 * 24.0);
            min = (int) (((num3 * 24.0) - Floor(num3 * 24.0)) * 60.0);
            s = (int) ((((num3 * 24.0) * 60.0) - Floor((num3 * 24.0) * 60.0)) * 60.0);
            m = 0;
            y = 0;
            if (num8 < 14)
            {
                m = num8 - 1;
            }
            if ((num8 == 14) || (num8 == 15))
            {
                m = num8 - 13;
            }
            if (m > 2)
            {
                y = (int) (num6 - 0x126cL);
            }
            else if ((m == 1) || (m == 2))
            {
                y = (int) (num6 - 0x126bL);
            }
        }

        public static double CorrectionCalcSunLatitude(double dLongitude, double dJD)
        {
            double num = (dJD - 2451545.0) / 36525.0;
            double d = (dLongitude - (1.397 * num)) - ((0.00031 * num) * num);
            d *= 0.017453292519943295;
            return ((0.03916 * (Math.Cos(d) - Math.Sin(d))) / 3600.0);
        }

        public static double CorrectionCalcSunLongitude(double dbSrcLongitude, double dbSrcLatitude, double dbJD)
        {
            double num = (dbJD - 2451545.0) / 36525.0;
            double d = (dbSrcLongitude - (1.397 * num)) - ((0.00031 * num) * num);
            d *= 0.017453292519943295;
            return ((-0.09033 + ((0.03916 * (Math.Cos(d) + Math.Sin(d))) * Math.Tan(dbSrcLatitude * 0.017453292519943295))) / 3600.0);
        }

        private static double Floor(double v)
        {
            return Math.Floor(v);
        }

        public static double GetNutationJamScalar(double dbJD)
        {
            double num = (dbJD - 2451545.0) / 36525.0;
            double num2 = num * num;
            double num3 = num2 * num;
            double dbDegrees = ((297.85036 + (445267.11148 * num)) - (0.0019142 * num2)) + (num3 / 189474.0);
            dbDegrees = MapTo0To360Range(dbDegrees);
            double num5 = ((357.52772 + (35999.05034 * num)) - (0.0001603 * num2)) - (num3 / 300000.0);
            num5 = MapTo0To360Range(num5);
            double num6 = ((134.96298 + (477198.867398 * num)) + (0.0086972 * num2)) + (num3 / 56250.0);
            num6 = MapTo0To360Range(num6);
            double num7 = ((93.27191 + (483202.017538 * num)) - (0.0036825 * num2)) + (num3 / 327270.0);
            num7 = MapTo0To360Range(num7);
            double num8 = ((125.04452 - (1934.136261 * num)) + (0.0020708 * num2)) + (num3 / 450000.0);
            num8 = MapTo0To360Range(num8);
            double num9 = 0.0;
            for (int i = 0; i < Nutation_Gene.Length; i++)
            {
                double a = (((((Nutation_Gene[i].nD * dbDegrees) + (Nutation_Gene[i].nM * num5)) + (Nutation_Gene[i].nMprime * num6)) + (Nutation_Gene[i].nF * num7)) + (Nutation_Gene[i].nOmega * num8)) * 0.017453292519943295;
                num9 += ((Nutation_Gene[i].nSincoeff1 + (Nutation_Gene[i].dSincoeff2 * num)) * Math.Sin(a)) * 0.0001;
            }
            return num9;
        }

        public static double GetSunLatitude(double dbJD)
        {
            double num = (dbJD - 2451545.0) / 365250.0;
            double num2 = 0.0;
            double num3 = 0.0;
            double num4 = 0.0;
            double num5 = 0.0;
            double num6 = 0.0;
            double num7 = 0.0;
            for (int i = 0; i < Earth_SLT0.Length; i++)
            {
                num3 += Earth_SLT0[i].dA * Math.Cos(Earth_SLT0[i].dB + (Earth_SLT0[i].dC * num));
            }
            for (int j = 0; j < Earth_SLT1.Length; j++)
            {
                num4 += Earth_SLT1[j].dA * Math.Cos(Earth_SLT1[j].dB + (Earth_SLT1[j].dC * num));
            }
            for (int k = 0; k < Earth_SLT2.Length; k++)
            {
                num5 += Earth_SLT2[k].dA * Math.Cos(Earth_SLT2[k].dB + (Earth_SLT2[k].dC * num));
            }
            for (int m = 0; m < Earth_SLT3.Length; m++)
            {
                num6 += Earth_SLT3[m].dA * Math.Cos(Earth_SLT3[m].dB + (Earth_SLT3[m].dC * num));
            }
            for (int n = 0; n < Earth_SLT4.Length; n++)
            {
                num7 += Earth_SLT4[n].dA * Math.Cos(Earth_SLT4[n].dB + (Earth_SLT4[n].dC * num));
            }
            num2 = (((num3 + (num4 * num)) + (num5 * (num * num))) + ((num6 * ((num * num) * num)) * (num7 * (((num * num) * num) * num)))) / 100000000.0;
            return -(num2 / 0.017453292519943295);
        }

        public static double GetSunLongitude(double dbJD)
        {
            double num = (dbJD - 2451545.0) / 365250.0;
            double num2 = 0.0;
            double num3 = 0.0;
            double num4 = 0.0;
            double num5 = 0.0;
            double num6 = 0.0;
            double num7 = 0.0;
            double num8 = 0.0;
            for (int i = 0; i < Earth_SLG0.Length; i++)
            {
                num3 += Earth_SLG0[i].dA * Math.Cos(Earth_SLG0[i].dB + (Earth_SLG0[i].dC * num));
            }
            for (int j = 0; j < Earth_SLG1.Length; j++)
            {
                num4 += Earth_SLG1[j].dA * Math.Cos(Earth_SLG1[j].dB + (Earth_SLG1[j].dC * num));
            }
            for (int k = 0; k < Earth_SLG2.Length; k++)
            {
                num5 += Earth_SLG2[k].dA * Math.Cos(Earth_SLG2[k].dB + (Earth_SLG2[k].dC * num));
            }
            for (int m = 0; m < Earth_SLG3.Length; m++)
            {
                num6 += Earth_SLG3[m].dA * Math.Cos(Earth_SLG3[m].dB + (Earth_SLG3[m].dC * num));
            }
            for (int n = 0; n < Earth_SLG4.Length; n++)
            {
                num7 += Earth_SLG4[n].dA * Math.Cos(Earth_SLG4[n].dB + (Earth_SLG4[n].dC * num));
            }
            for (int num14 = 0; num14 < Earth_SLG5.Length; num14++)
            {
                num8 += Earth_SLG5[num14].dA * Math.Cos(Earth_SLG5[num14].dB + (Earth_SLG5[num14].dC * num));
            }
            num2 = ((((num3 + (num4 * num)) + (num5 * (num * num))) + ((num6 * ((num * num) * num)) * (num7 * (((num * num) * num) * num)))) + (num8 * ((((num * num) * num) * num) * num))) / 100000000.0;
            return MapTo0To360Range(MapTo0To360Range(num2 / 0.017453292519943295) + 180.0);
        }

        public static double GetSunRadiusVector(double dbJD)
        {
            double num = (dbJD - 2451545.0) / 365250.0;
            double num2 = 0.0;
            double num3 = 0.0;
            double num4 = 0.0;
            double num5 = 0.0;
            double num6 = 0.0;
            for (int i = 0; i < Earth_SRV0.Length; i++)
            {
                num2 += Earth_SRV0[i].dA * Math.Cos(Earth_SRV0[i].dB + (Earth_SRV0[i].dC * num));
            }
            for (int j = 0; j < Earth_SRV1.Length; j++)
            {
                num3 += Earth_SRV1[j].dA * Math.Cos(Earth_SRV1[j].dB + (Earth_SRV1[j].dC * num));
            }
            for (int k = 0; k < Earth_SRV2.Length; k++)
            {
                num4 += Earth_SRV2[k].dA * Math.Cos(Earth_SRV2[k].dB + (Earth_SRV2[k].dC * num));
            }
            for (int m = 0; m < Earth_SRV3.Length; m++)
            {
                num5 += Earth_SRV3[m].dA * Math.Cos(Earth_SRV3[m].dB + (Earth_SRV3[m].dC * num));
            }
            for (int n = 0; n < Earth_SRV4.Length; n++)
            {
                num6 += Earth_SRV4[n].dA * Math.Cos(Earth_SRV4[n].dB + (Earth_SRV4[n].dC * num));
            }
            return ((((num2 + (num3 * num)) + (num4 * (num * num))) + ((num5 * ((num * num) * num)) * (num6 * (((num * num) * num) * num)))) / 100000000.0);
        }

        public static int Julday(int y, int m, int d)
        {
            int num4;
            int num3 = y;
            if (num3 < 0)
            {
                num3++;
            }
            if (m > 2)
            {
                num4 = m + 1;
            }
            else
            {
                num3--;
                num4 = m + 13;
            }
            int num2 = (int) (((Floor(365.25 * num3) + Floor(30.6001 * num4)) + d) + 1720995.0);
            if ((d + (0x1f * (m + (12 * y)))) >= 0x8fc1d)
            {
                int num = (int) (0.01 * num3);
                num2 += (2 - num) + ((int) (0.25 * num));
            }
            return num2;
        }

        public static double Julday(int y, int m, int d, int h, int min, int s)
        {
            int num = y;
            int num2 = m;
            double num3 = ((d + (((double) h) / 24.0)) + ((((double) min) / 60.0) / 24.0)) + (((((double) s) / 60.0) / 60.0) / 24.0);
            if ((m == 1) || (m == 2))
            {
                num = y - 1;
                num2 = m + 12;
            }
            int num4 = num / 100;
            int num5 = (2 - num4) + (num4 >> 2);
            return ((((((int) (365.25 * (num + 0x126c))) + ((int) (30.6001 * (num2 + 1)))) + num3) + num5) - 1524.5);
        }

        public static double MapTo0To360Range(double dbDegrees)
        {
            double num = dbDegrees;
            while (num < 0.0)
            {
                num += 360.0;
            }
            while (num > 360.0)
            {
                num -= 360.0;
            }
            return num;
        }
    }
}

