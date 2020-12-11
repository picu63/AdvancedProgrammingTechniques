using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using EnduroCalculatorv2;
using EnduroLibrary;

namespace EnduroCalculator
{
    public class ElevationCalculator : Calculator
    {
        private double totalClimbing;
        private double totalDescent;
        List<double> altitudes = new List<double>();
        public override void Calculate(TrackPoint nextPoint)
        {
            altitudes.Add(nextPoint.Altitude);
            base.Calculate(nextPoint);
            if ((nextPoint.DateTime - CurrentPoint.DateTime).TotalSeconds > TimeFilter)
                return;
            switch (CurrentDirection)
            {
                case AltitudeDirection.Climbing:
                    totalClimbing += nextPoint.Altitude - CurrentPoint.Altitude;
                    break;
                case AltitudeDirection.Descent:
                    totalDescent += CurrentPoint.Altitude - nextPoint.Altitude;
                    break;
                case AltitudeDirection.Flat:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            CurrentPoint = nextPoint;
        }

        public override void PrintResult()
        {
            var minElevation = altitudes.Min();
            var maxElevation = altitudes.Max();
            var averageElevation = altitudes.Average();
            var finalBalance = maxElevation - minElevation;
            Console.WriteLine("Elevation");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Minimum Elevation: " + minElevation);
            Console.WriteLine("Maximum Elevation: " + maxElevation);
            Console.WriteLine("Average Elevation: " + averageElevation);
            Console.WriteLine("Total Climbing: " + totalClimbing);
            Console.WriteLine("Total Descent: " + totalDescent);
            Console.WriteLine("Final Balance: " + finalBalance);

        }
    }
}
