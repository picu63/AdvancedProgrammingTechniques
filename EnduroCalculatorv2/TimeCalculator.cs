using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using EnduroCalculatorv2;
using EnduroLibrary;

namespace EnduroCalculator
{
    public class TimeCalculator : Calculator
    {
        public override double TimeFilter { get; set; }

        public override void Calculate(TrackPoint nextPoint)
        {
            base.Calculate(nextPoint);
        }

        public override void PrintResult()
        {
            throw new NotImplementedException();
        }
    }
}
