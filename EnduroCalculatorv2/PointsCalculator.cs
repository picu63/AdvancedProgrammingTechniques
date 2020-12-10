using System;
using System.Collections.Generic;
using System.Text;
using EnduroLibrary;

namespace EnduroCalculatorv2
{
    internal static class PointsCalculator
    {
        internal static AltitudeDirection CalculateDirection(TrackPoint previousPoint, TrackPoint currentPoint,
            double tolerance = double.Epsilon)
        {
            var previousAltitude = previousPoint.Altitude;
            var currentAltitude= currentPoint.Altitude;
            if (previousAltitude < currentAltitude - tolerance)
            {
                return AltitudeDirection.Climbing;
            }
            else if (previousAltitude > currentAltitude + tolerance)
            {
                return AltitudeDirection.Descent;
            }
            else
            {
                return AltitudeDirection.Flat;
            }
        }

        internal static AltitudeDirection CalculateDirectionWithSlope(TrackPoint previousPoint, TrackPoint currentPoint,
            double slope = double.Epsilon)
        {
            var slopeCalculated = CalculateSlope(previousPoint, currentPoint);
            if (slopeCalculated < -slope/100)
            {
                return AltitudeDirection.Climbing;
            }

            if (slopeCalculated > slope/100)
            {
                return AltitudeDirection.Descent;
            }
            return AltitudeDirection.Flat;
        }

        private static double CalculateSlope(TrackPoint previousPoint, TrackPoint currentPoint)
        {
            var deltaH = previousPoint.Altitude - currentPoint.Altitude;
            var deltaS = previousPoint.GetGeoCoordinate().GetDistanceTo(currentPoint.GetGeoCoordinate());
            return deltaH / deltaS;
        }
    }
}
