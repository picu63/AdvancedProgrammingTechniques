using System;
using System.Collections.Generic;
using System.Linq;
using EnduroLibrary;

namespace EnduroCalculator.Calculators
{
    public class SpeedCalculator : TrackCalculator
    {
        private readonly List<double> _speeds = new List<double>();
        private readonly List<double> _climbingSpeeds = new List<double>();
        private readonly List<double> _descentSpeeds = new List<double>();
        private readonly List<double> _flatSpeeds = new List<double>();
        public override void Calculate(TrackPoint nextPoint)
        {
            base.Calculate(nextPoint);
            var timeSpanSeconds = (nextPoint.DateTime - CurrentPoint.DateTime).TotalSeconds;
            if (timeSpanSeconds > TimeFilter || timeSpanSeconds == 0)
                return;
            var deltaS = CurrentPoint.GetGeoCoordinate().GetDistanceTo(nextPoint.GetGeoCoordinate());
            var deltaT = (nextPoint.DateTime - CurrentPoint.DateTime).TotalSeconds;
            var speed = deltaS / deltaT;
            _speeds.Add(speed);
            switch (CurrentDirection)
            {
                case AltitudeDirection.Climbing:
                    _climbingSpeeds.Add(speed);
                    break;
                case AltitudeDirection.Descent:
                    _descentSpeeds.Add(speed);
                    break;
                case AltitudeDirection.Flat:
                    _flatSpeeds.Add(speed);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            CurrentPoint = nextPoint;
        }

        public override void PrintResult()
        {
            var minSpeed = _speeds.Min();
            var maxSpeed = _speeds.Max();
            var averageSpeed = _speeds.Average();
            var averageClimbing = _climbingSpeeds.Average();
            var averageDescent = _descentSpeeds.Average();
            var averageFlat = _flatSpeeds.Average();
            Console.WriteLine("Speed");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Minimum Speed: " + minSpeed.ToKilometersPerHour().ToString("##.00 'km/h'"));
            Console.WriteLine("Maximum Speed: " + maxSpeed.ToKilometersPerHour().ToString("##.00 'km/h'"));
            Console.WriteLine("Average Speed: " + averageSpeed.ToKilometersPerHour().ToString("##.00 'km/h'"));
            Console.WriteLine("Average Climbing Speed: " + averageClimbing.ToKilometersPerHour().ToString("##.00 'km/h'"));
            Console.WriteLine("Average Descent Speed: " + averageDescent.ToKilometersPerHour().ToString("##.00 'km/h'"));
            Console.WriteLine("Average Flat Speed: " + averageFlat.ToKilometersPerHour().ToString("##.00 'km/h'"));
        }
    }
}
