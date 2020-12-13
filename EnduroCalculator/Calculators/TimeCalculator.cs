using System;
using System.Collections.Generic;
using System.Linq;
using EnduroLibrary;

namespace EnduroCalculator.Calculators
{
    public class TimeCalculator : TrackCalculator
    {
        private readonly List<DateTime> _times = new List<DateTime>();
        private double _climbingTime;
        private double _descentTime;
        private double _flatTime;
        public override void Calculate(TrackPoint nextPoint)
        {
            base.Calculate(nextPoint);
            var nextTime = nextPoint.DateTime;
            var timeSpanSeconds = (nextTime - CurrentPoint.DateTime).TotalSeconds;
            if (timeSpanSeconds > TimeFilter || timeSpanSeconds == 0)
                return;
            _times.Add(nextTime);
            switch (CurrentDirection)
            {
                case AltitudeDirection.Climbing:
                    _climbingTime += timeSpanSeconds;
                    break;
                case AltitudeDirection.Descent:
                    _descentTime += timeSpanSeconds;
                    break;
                case AltitudeDirection.Flat:
                    _flatTime += timeSpanSeconds;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            CurrentPoint = nextPoint;
        }

        public override void PrintResult()
        {
            var total = _times.Max() - _times.Min();
            Console.WriteLine("Time");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Total Track Time: " + total);
            Console.WriteLine("Climbing Time: " + TimeSpan.FromSeconds(_climbingTime));
            Console.WriteLine("Descent Time: " + TimeSpan.FromSeconds(_descentTime));
            Console.WriteLine("Flat Time: " + TimeSpan.FromSeconds(_flatTime));
        }
    }
}
