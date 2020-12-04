﻿using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using EnduroCalculator.Interfaces;
using EnduroLibrary;

namespace EnduroCalculator
{
    public class DistanceCalculator : IDistanceCalculator
    {
        public double GetTotalDistance(IEnumerable<TrackPoint> coordinates)
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

        public double GetClimbingDistance(IEnumerable<TrackPoint> coordinates)
        {
            TrackCalculator trackCalculator = new TrackCalculator();
            var climbingTracks = trackCalculator.GetClimbingTracks(coordinates);
            double climbingDistance = 0;
            foreach(var track in climbingTracks)
            {
                var geoCoordinates = coordinates.Select((t) => new GeoCoordinate(t.Latitude, t.Longitude));
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

        public double GetDescentDistance(IEnumerable<TrackPoint> coordinates)
        {
            TrackCalculator trackCalculator = new TrackCalculator();
            var descentTracks = trackCalculator.GetClimbingTracks(coordinates);
            double descentDistance = 0;
            foreach (var track in descentTracks)
            {
                var geoCoordinates = coordinates.Select((t) => new GeoCoordinate(t.Latitude, t.Longitude));
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

        public double GetFlatDistance(IEnumerable<TrackPoint> coordinates, double range)
        {
            TrackCalculator trackCalculator = new TrackCalculator();
            var flatTracks = trackCalculator.GetClimbingTracks(coordinates);
            double flatDistance = 0;
            foreach (var track in flatTracks)
            {
                var geoCoordinates = coordinates.Select((t) => new GeoCoordinate(t.Latitude, t.Longitude));
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
