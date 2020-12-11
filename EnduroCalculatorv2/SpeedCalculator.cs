using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Text;
using EnduroCalculatorv2;
using EnduroLibrary;

namespace EnduroCalculator
{
    public class SpeedCalculator : Calculator
    {
        List<double> speeds = new List<double>();
        List<double> climbingSpeeds = new List<double>();
        List<double> descentSpeeds = new List<double>();
        List<double> flatSpeeds = new List<double>();
        public override void Calculate(TrackPoint nextPoint)
        {
            base.Calculate(nextPoint);
            var timeSpanSeconds = (nextPoint.DateTime - CurrentPoint.DateTime).TotalSeconds;
            if (timeSpanSeconds > TimeFilter || timeSpanSeconds == 0)
                return;
            var deltaS = CurrentPoint.GetGeoCoordinate().GetDistanceTo(nextPoint.GetGeoCoordinate());
            var deltaT = (nextPoint.DateTime - CurrentPoint.DateTime).TotalSeconds;
            var speed = deltaS / deltaT;
            speeds.Add(speed);
            switch (CurrentDirection)
            {
                case AltitudeDirection.Climbing:
                    climbingSpeeds.Add(speed);
                    break;
                case AltitudeDirection.Descent:
                    descentSpeeds.Add(speed);
                    break;
                case AltitudeDirection.Flat:
                    flatSpeeds.Add(speed);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            CurrentPoint = nextPoint;
        }

        public override void PrintResult()
        {
            var minSpeed = speeds.Min();
            var maxSpeed = speeds.Max();
            var averageSpeed = speeds.Average();
            var averageClimbing = climbingSpeeds.Average();
            var averageDescent = descentSpeeds.Average();
            var averageFlat = flatSpeeds.Average();
            Console.WriteLine("Speed");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Minimum Speed: " + minSpeed);
            Console.WriteLine("Maximum Speed: " + maxSpeed);
            Console.WriteLine("Average Speed: " + averageSpeed);
            Console.WriteLine("Average Climbing Speed: " + averageClimbing);
            Console.WriteLine("Average Descent Speed: " + averageDescent);
            Console.WriteLine("Average Flat Speed: " + averageFlat);
        }
    }
}
