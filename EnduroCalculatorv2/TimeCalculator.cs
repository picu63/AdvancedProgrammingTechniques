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
        List<DateTime> times = new List<DateTime>();
        private double climbingTime;
        private double descentTime;
        private double flatTime;
        public override void Calculate(TrackPoint nextPoint)
        {
            base.Calculate(nextPoint);
            var nextTime = nextPoint.DateTime;
            var timeSpanSeconds = (nextTime - CurrentPoint.DateTime).TotalSeconds;
            if (timeSpanSeconds > TimeFilter || timeSpanSeconds == 0)
                return;
            times.Add(nextTime);
            switch (CurrentDirection)
            {
                case AltitudeDirection.Climbing:
                    climbingTime += timeSpanSeconds;
                    break;
                case AltitudeDirection.Descent:
                    descentTime += timeSpanSeconds;
                    break;
                case AltitudeDirection.Flat:
                    flatTime += timeSpanSeconds;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            CurrentPoint = nextPoint;
        }

        public override void PrintResult()
        {
            var total = times.Max() - times.Min();
            Console.WriteLine("Time");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Total Track Time: " + total);
            Console.WriteLine("Climbing Time: " + climbingTime);
            Console.WriteLine("Descent Time: " + descentTime);
            Console.WriteLine("Flat Time: " + flatTime);
        }
    }
}
