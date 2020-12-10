using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using EnduroCalculatorv2;
using EnduroLibrary;

namespace EnduroCalculator
{
    public class DistanceCalculator : ICalculator
    {
        private double totalDistance;
        private double climbDistance;
        private double descentDistance;
        private TrackPoint previousTrackPoint;
        public void SetupStart(TrackPoint startPoint)
        {
            previousTrackPoint = startPoint;
        }

        public void Calculate(TrackPoint nextPoint)
        {
            if (previousTrackPoint is null)
            {
                throw new ArgumentNullException(nameof(previousTrackPoint), "There is no start point initialized.");
            }
            var distance = previousTrackPoint.GetGeoCoordinate().GetDistanceTo(nextPoint.GetGeoCoordinate());
            totalDistance += distance;
            if (previousTrackPoint.Altitude < nextPoint.Altitude)
            {
                climbDistance += distance;
            }

            if (previousTrackPoint.Altitude > nextPoint.Altitude)
            {
                descentDistance += distance;
            }
        }

        public void AddTolerance(double toleranceInMeters)
        {
            throw new NotImplementedException();
        }

        public void PrintResult()
        {
            throw new NotImplementedException();
        }
    }
}
