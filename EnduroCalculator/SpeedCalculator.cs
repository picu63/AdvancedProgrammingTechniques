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
            var climbingTrackPoints = GetClimbingTrackPoints(trackPoints);
            List<double> averageClimbSpeedList = new List<double>();
            foreach (var climbingTrackPointList in climbingTrackPoints)
            {
                var averageClimbSpeed = GetAverageSpeed(climbingTrackPointList);
                averageClimbSpeedList.Add(averageClimbSpeed);
            }

            return averageClimbSpeedList.Average();
        }

        private IEnumerable<List<TrackPoint>> GetClimbingTrackPoints(IEnumerable<TrackPoint> trackPoints)
        {
            var trackPointsList = trackPoints.ToList();
            var climbTrackPoints = new List<TrackPoint>();
            Queue<TrackPoint> tracksQueue = new Queue<TrackPoint>(trackPoints);
            // 1, 3, 2, 4
            while (true)
            {
                var tp = tracksQueue.Dequeue();
                var tpNext = tracksQueue.Peek();
                if (tp.Altitude - tpNext.Altitude > 0)
                {
                    continue;
                }
            }

            for (int i = 0; i < trackPointsList.Count - 1; i++)
            {
                var tpCurrent = trackPointsList[i];
                var tpNext = trackPointsList[i + 1];
                if (tpCurrent.Altitude - tpNext.Altitude > 0)
                {
                    yield return climbTrackPoints;
                    climbTrackPoints = new List<TrackPoint>();
                }
                else
                {
                    climbTrackPoints.Add(tpNext);
                }
            }
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
