using System;
using EnduroLibrary;

namespace EnduroCalculator.Calculators
{
    public class DistanceCalculator : TrackCalculator
    {
        private double _totalDistance;
        private double _climbDistance;
        private double _descentDistance;
        private double _flatDistance;
        public override void Calculate(TrackPoint nextPoint)
        {
            base.Calculate(nextPoint);

            var timeSpanSeconds = (nextPoint.DateTime - CurrentPoint.DateTime).TotalSeconds;
            if (timeSpanSeconds > TimeFilter || timeSpanSeconds==0)
                return;
            var distance = CurrentPoint.GetGeoCoordinate().GetDistanceTo(nextPoint.GetGeoCoordinate());
            _totalDistance += distance;
            switch (CurrentDirection)
            {
                case AltitudeDirection.Climbing:
                    _climbDistance += distance;
                    break;
                case AltitudeDirection.Descent:
                    _descentDistance += distance;
                    break;
                case AltitudeDirection.Flat:
                    _flatDistance += distance;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            CurrentPoint = nextPoint;
        }

        public override void PrintResult()
        {
            Console.WriteLine("Distance");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Total Distance: " + _totalDistance.ToString("##.00 'm'"));
            Console.WriteLine("Climbing Distance: " + _climbDistance.ToString("##.00 'm'"));
            Console.WriteLine("Descent Distance: " + _descentDistance.ToString("##.00 'm'"));
            Console.WriteLine("Flat Distance: " + _flatDistance.ToString("##.00 'm'"));
        }
    }
}
