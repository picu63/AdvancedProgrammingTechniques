using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Text;
using EnduroCalculator.Interfaces;
using EnduroLibrary;

namespace EnduroCalculator
{
    public class SpeedCalculator : ISpeedCalculator
    {
        public double GetMinimumSpeed(IEnumerable<TrackPoint> trackPoints)
        {
            var velocities = GetAllVelocities(trackPoints);
            return velocities.Min();
        }

        private IEnumerable<double> GetAllVelocities(IEnumerable<TrackPoint> trackPoints)
        {
            var trackPointsList = trackPoints.ToList();
            for (int i = 0; i < trackPointsList.Count - 1; i++)
            {
                var tpCurrent = trackPointsList[i];
                var tpNext = trackPointsList[i + 1];
                yield return GetVelocity(tpCurrent, tpNext);
            }
        }

        private double GetVelocity(TrackPoint startPoint, TrackPoint endPoint)
        { 
            //TODO sprawdzić w jakiej jednostce mierzony jest dystans
            var distance = startPoint.GetGeoCoordinate().GetDistanceTo(endPoint.GetGeoCoordinate());
            var timeSpan = endPoint.DateTime - startPoint.DateTime;
            var timeInSeconds = timeSpan.TotalSeconds;
            return distance / timeInSeconds;
        }

        public double GetMaximumSpeed(IEnumerable<TrackPoint> trackPoints)
        {
            var velocities = GetAllVelocities(trackPoints);
            return velocities.Max();
        }

        public double GetAverageSpeed(IEnumerable<TrackPoint> trackPoints)
        {
            var velocities = GetAllVelocities(trackPoints);
            return velocities.Average();
        }

        public double GetAverageClimbingSpeed(IEnumerable<TrackPoint> trackPoints)
        {
            var climbingTrackPoints = GetClimbingTracks(trackPoints);
            var averageClimbSpeedList = new List<double>();
            foreach (var climbingTrackPointList in climbingTrackPoints)
            {
                var averageClimbSpeed = GetAverageSpeed(climbingTrackPointList);
                averageClimbSpeedList.Add(averageClimbSpeed);
            }

            return averageClimbSpeedList.Average();
        }

        public IEnumerable<List<TrackPoint>> GetClimbingTracks(IEnumerable<TrackPoint> trackPoints)
        {
            var climbingTracks = new List<List<TrackPoint>>();
            var tp = trackPoints.ToList();
            TrackPoint current;
            TrackPoint next;
            var currentTrackPoints = new List<TrackPoint>();
            //// 1, 3, 5, 4, 5                1, 3, 3, 5
            for (int i = 0; i < tp.Count - 1; i++)
            {
                current = tp[i];
                next = tp[i + 1];
                var nextIndex = i + 1;
                if (nextIndex == tp.Count - 1)
                {
                    
                }
                if (current.Altitude < next.Altitude)
                {
                    currentTrackPoints.Add(current);
                    currentTrackPoints.Add(next);
                }
                else
                {
                    climbingTracks.Add(currentTrackPoints);
                    currentTrackPoints = new List<TrackPoint>();
                }
            }
            climbingTracks.Select((track => track.Distinct()));
            //var climbingTrack = new List<TrackPoint>();
            //var tracksQueue = new Queue<TrackPoint>(trackPoints);

            //while (true)
            //{
            //    var tp = tracksQueue.Dequeue();
            //    var tpNext = tracksQueue.Peek();
            //    if (tpNext is null)
            //    {
            //        return climbingTracks;
            //    }
            //    if (tp.Altitude < tpNext.Altitude)
            //    {
            //        climbingTrack.Add(tp);
            //    }
            //    else
            //    {
            //        if (climbingTrack.Count >= 2)
            //        {
            //            climbingTracks.Add(climbingTrack);
            //        }
            //        climbingTrack = new List<TrackPoint>();
            //    }
            //}
        }

        public double GetAverageDescentSpeed(IEnumerable<TrackPoint> trackPoints)
        {
            throw new NotImplementedException();
        }

        public double GetAverageFlatSpeed(IEnumerable<TrackPoint> trackPoints)
        {
            throw new NotImplementedException();
        }
    }
}
