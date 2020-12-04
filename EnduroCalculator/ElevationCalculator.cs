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

        public double GetMinimumElevation(IEnumerable<TrackPoint> trackPoints)
        {
            var geoCoordinates = trackPoints.Select((t) => t.Altitude);

            var min = geoCoordinates.Min();
            return (min);
        }

        public double GetMaximumElevation(IEnumerable<TrackPoint> trackPoints)
        {
            var geoCoordinates = trackPoints.Select((t) => t.Altitude);

            var max = geoCoordinates.Max();
            return (max);
        }

        public double GetAverageElevation(IEnumerable<TrackPoint> trackPoints)
        {
            var geoCoordinates = trackPoints.Select((t) => t.Altitude);

            var avg = geoCoordinates.Average();
            return (avg);
        }

        public double GetTotalClimbing(IEnumerable<TrackPoint> trackPoints)
        {
            TrackCalculator trackCalculator = new TrackCalculator();
            var climbingTracks = trackCalculator.GetClimbingTracks(trackPoints);
            double total = 0;
            foreach (var climbingTrack in climbingTracks)
            {
                var geoCoordinates = climbingTrack.Select((t) => t.Altitude);

                var min = geoCoordinates.Min();
                var max = geoCoordinates.Max();
                var differece = max - min;
                total += differece;
            }
            return total;
        }

        public double GetTotalDescent(IEnumerable<TrackPoint> trackPoints)
        {
            TrackCalculator trackCalculator = new TrackCalculator();
            var descentTracks = trackCalculator.GetDescentTracks(trackPoints);
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

        public double GetFinalBalance(IEnumerable<TrackPoint> trackPoints)
        {
            var geoCoordinates = trackPoints.Select((t) => t.Altitude);

            var min = geoCoordinates.Min();
            var max = geoCoordinates.Max();
            var total = max - min;
            return (total);
        }
    }
}
