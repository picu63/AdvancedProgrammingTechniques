using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnduroCalculator.Interfaces;
using EnduroLibrary;
using MoreLinq;

namespace EnduroCalculator
{
    public class TrackCalculator : ITrackCalculator
    {
        public IEnumerable<List<TrackPoint>> GetClimbingTracks(IEnumerable<TrackPoint> trackPoints)
        {
            var climbingTracks = new List<List<TrackPoint>>();
            var tp = trackPoints.ToList();
            var currentTrackPoints = new List<TrackPoint>();
            for (var i = 1; i < tp.Count; i++)
            {
                var current = tp[i];
                var previous = tp[i - 1];
                if (current.Altitude > previous.Altitude)
                {
                    currentTrackPoints.Add(previous);
                    currentTrackPoints.Add(current);
                }
                else if(currentTrackPoints.Count >= 2)
                {
                    climbingTracks.Add(currentTrackPoints);
                    currentTrackPoints = new List<TrackPoint>();
                }
            }
            if (currentTrackPoints.Count >= 2)
            {
                climbingTracks.Add(currentTrackPoints);
            }
            var climbingTracksDistincted = climbingTracks
                .Select(track => track.DistinctBy(point => point.Altitude).ToList());

            return climbingTracksDistincted;
        }

        public IEnumerable<List<TrackPoint>> GetAlltitudeDirection(IEnumerable<TrackPoint> trackPoints,
            Direction direction)
        {

            switch (direction)
            {
                case Direction.Climbing:

                    break;
                case Direction.Descending:
                    break;
                case Direction.Flat:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            return null;
        }

        public IEnumerable<double> GetAllVelocities(IEnumerable<TrackPoint> trackPoints)
        {
            var trackPointsList = trackPoints.ToList();
            for (int i = 0; i < trackPointsList.Count - 1; i++)
            {
                var tpCurrent = trackPointsList[i];
                var tpNext = trackPointsList[i + 1];
                yield return GetVelocity(tpCurrent, tpNext);
            }
        }

        public double GetVelocity(TrackPoint startPoint, TrackPoint endPoint)
        {
            //TODO sprawdzić w jakiej jednostce mierzony jest dystans
            var distance = startPoint.GetGeoCoordinate().GetDistanceTo(endPoint.GetGeoCoordinate());
            var timeSpan = endPoint.DateTime - startPoint.DateTime;
            var timeInSeconds = timeSpan.TotalSeconds;
            return distance / timeInSeconds;
        }

        public IEnumerable<List<TrackPoint>> GetDescentTracks(IEnumerable<TrackPoint> trackPoints)
        {
            var descentTracks = new List<List<TrackPoint>>();
            var tp = trackPoints.ToList();
            var currentTrackPoints = new List<TrackPoint>();
            for (var i = 1; i < tp.Count; i++)
            {
                var current = tp[i];
                var previous = tp[i - 1];
                if (current.Altitude < previous.Altitude)
                {
                    currentTrackPoints.Add(previous);
                    currentTrackPoints.Add(current);
                }
                else if (currentTrackPoints.Count >= 2)
                {
                    descentTracks.Add(currentTrackPoints);
                    currentTrackPoints = new List<TrackPoint>();
                }
            }
            if (currentTrackPoints.Count >= 2)
            {
                descentTracks.Add(currentTrackPoints);
            }
            var descentTracksDistincted = descentTracks
                .Select(track => track.DistinctBy(point => point.Altitude).ToList());

            return descentTracksDistincted;
        }

        public IEnumerable<List<TrackPoint>> GetFlatTracks(IEnumerable<TrackPoint> trackPoints, double range)
        {
            var flatTracks = new List<List<TrackPoint>>();
            var tp = trackPoints.ToList();
            var currentTrackPoints = new List<TrackPoint>();
            for (var i = 1; i < tp.Count; i++)
            {
                var current = tp[i];
                var previous = tp[i - 1];
                if (Math.Abs(current.Altitude - previous.Altitude) <= range)
                {
                    currentTrackPoints.Add(previous);
                    currentTrackPoints.Add(current);
                }
                else if (currentTrackPoints.Count >= 2)
                {
                    flatTracks.Add(currentTrackPoints);
                    currentTrackPoints = new List<TrackPoint>();
                }
            }
            if (currentTrackPoints.Count >= 2)
            {
                flatTracks.Add(currentTrackPoints);
            }
            var flatTracksDistincted = flatTracks
                .Select(track => track.DistinctBy(point => point.Altitude).ToList());

            return flatTracksDistincted;
        }
    }

    public enum Direction
    {
        Climbing,
        Descending,
        Flat,
    }
}
