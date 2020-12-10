using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using EnduroCalculatorv2;
using EnduroLibrary;

namespace EnduroCalculator
{
    public class DistanceCalculator : Calculator
    {
        private double totalDistance;
        private double climbDistance;
        private double descentDistance;
        private double flatDistance;
        public override void Calculate(TrackPoint nextPoint)
        {
            base.Calculate(nextPoint);
            if ((nextPoint.DateTime - CurrentPoint.DateTime).TotalSeconds > TimeFilter)
                return;
            var distance = CurrentPoint.GetGeoCoordinate().GetDistanceTo(nextPoint.GetGeoCoordinate());
            totalDistance += distance;
            switch (CurrentDirection)
            {
                case AltitudeDirection.Climbing:
                    climbDistance += distance;
                    break;
                case AltitudeDirection.Descent:
                    descentDistance += distance;
                    break;
                case AltitudeDirection.Flat:
                    flatDistance += distance;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            CurrentPoint = nextPoint;
        }

        public override double Slope { get; set; }

        public override void PrintResult()
        {
            throw new NotImplementedException();
        }
    }
}
