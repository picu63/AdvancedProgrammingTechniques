using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using EnduroCalculator.Interfaces;
using EnduroLibrary;

namespace EnduroCalculator
{
    public class DistanceCalculator : IDistanceCalculator
    {
        public double GetTotalDistance(ICollection<TrackPoint> trackPoints)
        {
            var geoCoordinates = trackPoints.Select((t) => t.GetGeoCoordinate());
            double distance = 0;
            while (true)
            {
                var first = geoCoordinates.FirstOrDefault();
                var second = geoCoordinates.Skip(1).FirstOrDefault();
                if (first is null || second is null)
                {
                    break;
                }

                distance += first.GetDistanceTo(second);
                geoCoordinates = geoCoordinates.Skip(1);
            }
            return distance;
        }

        public double GetClimbingDistance(ICollection<TrackPoint> coordinates, double slopeDegree)
        {
            TrackCalculator trackCalculator = new TrackCalculator();
            var climbingSections = trackCalculator.GetClimbingSections(coordinates, slopeDegree);
            int count = climbingSections.Sum((list => list.Count));
            double climbingDistance = GetDistance(climbingSections);
            return climbingDistance;
        }

        public double GetDescentDistance(ICollection<TrackPoint> coordinates, double slopeDegree)
        {
            TrackCalculator trackCalculator = new TrackCalculator();
            var descentTracks = trackCalculator.GetDescentSections(coordinates, slopeDegree);
            int count = descentTracks.Sum((list => list.Count));
            var descentDistance = GetDistance(descentTracks);
            return descentDistance;
        }

        public double GetFlatDistance(ICollection<TrackPoint> coordinates, double maxSlopeDegree)
        {
            TrackCalculator trackCalculator = new TrackCalculator();
            var flatTracks = trackCalculator.GetFlatSections(coordinates, maxSlopeDegree);
            double flatDistance = GetDistance(flatTracks);
            return flatDistance;
        }

        private static double GetDistance(ICollection<List<TrackPoint>> sections)
        {
            double distance = 0;

            foreach (var section in sections)
            {
                var geoCoordinates = section.Select((t) => new GeoCoordinate(t.Latitude, t.Longitude));
                while (true)
                {
                    var first = geoCoordinates.FirstOrDefault();
                    var second = geoCoordinates.Skip(1).FirstOrDefault();
                    if (second is null)
                    {
                        break;
                    }

                    distance += first.GetDistanceTo(second);
                    geoCoordinates = geoCoordinates.Skip(1);
                }
            }
            return distance;
        }
    }
}
