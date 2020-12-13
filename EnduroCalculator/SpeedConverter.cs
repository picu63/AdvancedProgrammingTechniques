using System;
using System.Collections.Generic;
using System.Text;

namespace EnduroCalculator
{
    internal static class SpeedConverter
    {
        internal static double ToKilometersPerHour(this double metersPerSecond)
        {
            return metersPerSecond * 3600 / 1000;
        }
    }
}
