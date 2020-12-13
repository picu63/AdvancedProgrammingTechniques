using System;
using System.Collections.Generic;
using System.Linq;
using EnduroLibrary;

namespace EnduroCalculator.Calculators
{
    public class ElevationCalculator : TrackCalculator
    {
        private double _totalClimbing;
        private double _totalDescent;
        List<double> _altitudes = new List<double>();
        public override void Calculate(TrackPoint nextPoint)
        {
            _altitudes.Add(nextPoint.Altitude);
            base.Calculate(nextPoint);
            if ((nextPoint.DateTime - CurrentPoint.DateTime).TotalSeconds > TimeFilter)
                return;
            switch (CurrentDirection)
            {
                case AltitudeDirection.Climbing:
                    _totalClimbing += nextPoint.Altitude - CurrentPoint.Altitude;
                    break;
                case AltitudeDirection.Descent:
                    _totalDescent += CurrentPoint.Altitude - nextPoint.Altitude;
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
            var minElevation = _altitudes.Min();
            var maxElevation = _altitudes.Max();
            var averageElevation = _altitudes.Average();
            var finalBalance = maxElevation - minElevation;
            Console.WriteLine("Elevation");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Minimum Elevation: " + minElevation.ToString("##.00 'm above level see'"));
            Console.WriteLine("Maximum Elevation: " + maxElevation.ToString("##.00 'm above level see'"));
            Console.WriteLine("Average Elevation: " + averageElevation.ToString("##.00 'meters'"));
            Console.WriteLine("Total Climbing: " + _totalClimbing.ToString("##.00 'm'"));
            Console.WriteLine("Total Descent: " + _totalDescent.ToString("##.00 'm'"));
            Console.WriteLine("Final Balance: " + finalBalance.ToString("##.00 'm'"));

        }
    }
}
