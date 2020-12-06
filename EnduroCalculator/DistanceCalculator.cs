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
        public double GetTotalDistance(ICollection<TrackPoint> coordinates)
        {
            var geoCoordinates = coordinates.Select((t) => new GeoCoordinate(t.Latitude, t.Longitude));

            double totalDistance = 0;
            while (true)
            {
                var first = geoCoordinates.FirstOrDefault();
                var second = geoCoordinates.Skip(1).FirstOrDefault();
                if (second is null)
                {
                    break;
                }

                var distance = totalDistance + first.GetDistanceTo(second);
                geoCoordinates = geoCoordinates.Skip(1);
                totalDistance = distance;
            }

            return totalDistance;
        }

        public double GetClimbingDistance(ICollection<TrackPoint> coordinates)
        {
            TrackCalculator trackCalculator = new TrackCalculator();
            var climbingSections = trackCalculator.GetClimbingSections(coordinates);
            double climbingDistance = 0;
            foreach(var section in climbingSections)
            {
                var geoCoordinates = section.Select((t) => t.GetGeoCoordinate());
                while (true)
                {
                    var first = geoCoordinates.FirstOrDefault();
                    var second = geoCoordinates.Skip(1).FirstOrDefault(); 
                    if (second is null) 
                    { 
                        break;
                    }
                    climbingDistance += first.GetDistanceTo(second);
                    geoCoordinates = geoCoordinates.Skip(1);
                }
            }
            return climbingDistance;
        }

        public double GetDescentDistance(ICollection<TrackPoint> coordinates)
        {
            TrackCalculator trackCalculator = new TrackCalculator();
            var descentTracks = trackCalculator.GetDescentSections(coordinates);
            double descentDistance = 0;
            foreach (var section in descentTracks)
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
                    descentDistance += first.GetDistanceTo(second);
                    geoCoordinates = geoCoordinates.Skip(1);
                }
            }
            return descentDistance;
        }

        public double GetFlatDistance(ICollection<TrackPoint> coordinates, double range = 0.05)
        {
            TrackCalculator trackCalculator = new TrackCalculator();
            var flatTracks = trackCalculator.GetFlatSections(coordinates, range);
            double flatDistance = 0;
            foreach (var section in flatTracks)
            {
                var geoCoordinates = section.Select((t) => new GeoCoordinate(t.Latitude, t.Longitude));
                while (true)
                {
                    var first = geoCoordinates.FirstOrDefault();
                    var second = geoCoordinates.Skip(1).FirstOrDefault();
                    if (second is null)
                    {
                        return flatDistance;
                    }
                    flatDistance = first.GetDistanceTo(second);
                    geoCoordinates = geoCoordinates.Skip(1);
                }
            }
            return flatDistance;
        }
    }
}
