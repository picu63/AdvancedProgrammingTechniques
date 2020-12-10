using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using EnduroLibrary;

namespace EnduroCalculator
{
    public class ElevationCalculator : Calculator
    {
        public TrackPoint StartPoint { get; set; }

        public override void Calculate(TrackPoint nextPoint)
        {
            throw new NotImplementedException();
        }

        public override void PrintResult()
        {
            throw new NotImplementedException();
        }
    }
}
