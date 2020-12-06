using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using EnduroCalculator.Interfaces;
using EnduroLibrary;

namespace EnduroCalculator
{
    public class ElevationCalculator : IElevationCalculator
    {

        public double GetMinimumElevation(ICollection<TrackPoint> trackPoints)
        {
            var geoCoordinates = trackPoints.Select((t) => t.Altitude);

            var min = geoCoordinates.Min();
            return (min);
        }

        public double GetMaximumElevation(ICollection<TrackPoint> trackPoints)
        {
            var geoCoordinates = trackPoints.Select((t) => t.Altitude);

            var max = geoCoordinates.Max();
            return (max);
        }

        public double GetAverageElevation(ICollection<TrackPoint> trackPoints)
        {
            var geoCoordinates = trackPoints.Select((t) => t.Altitude);

            var avg = geoCoordinates.Average();
            return (avg);
        }

        public double GetTotalClimbing(ICollection<TrackPoint> trackPoints, double slopeDegree)
        {
            TrackCalculator trackCalculator = new TrackCalculator();
            var climbingSections = trackCalculator.GetClimbingSections(trackPoints);
            double total = 0;
            foreach (var section in climbingSections)
            {
                var geoCoordinates = section.Select((t) => t.Altitude);

                var min = geoCoordinates.Min();
                var max = geoCoordinates.Max();
                var differece = max - min;
                total += differece;
            }
            return total;
        }

        public double GetTotalDescent(ICollection<TrackPoint> trackPoints, double slopeDegree)
        {
            TrackCalculator trackCalculator = new TrackCalculator();
            var descentTracks = trackCalculator.GetDescentSections(trackPoints);
            double total = 0;
            foreach (var descentTrack in descentTracks)
            {
                var geoCoordinates = descentTrack.Select((t) => t.Altitude);

                var min = geoCoordinates.Min();
                var max = geoCoordinates.Max();
                var differece = max - min;
                total += differece;
            }
            return total;

        }

        public double GetFinalBalance(ICollection<TrackPoint> trackPoints)
        {
            var geoCoordinates = trackPoints.Select((t) => t.Altitude);

            var min = geoCoordinates.Min();
            var max = geoCoordinates.Max();
            var total = max - min;
            return (total);
        }
    }
}
