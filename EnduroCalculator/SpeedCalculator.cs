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
            var trackCalculator = new TrackCalculator();
            var velocities = trackCalculator.GetAllVelocities(trackPoints);
            return velocities.Min();
        }

        public double GetMaximumSpeed(IEnumerable<TrackPoint> trackPoints)
        {
            var trackCalculator = new TrackCalculator();
            var velocities = trackCalculator.GetAllVelocities(trackPoints);
            return velocities.Max();
        }

        public double GetAverageSpeed(IEnumerable<TrackPoint> trackPoints)
        {
            var trackCalculator = new TrackCalculator();
            var velocities = trackCalculator.GetAllVelocities(trackPoints);
            return velocities.Average();
        }

        public double GetAverageClimbingSpeed(IEnumerable<TrackPoint> trackPoints)
        {
            var trackCalculator = new TrackCalculator();
            var climbingTrackPoints = trackCalculator.GetClimbingTracks(trackPoints);
            var averageClimbSpeedList = new List<double>();
            foreach (var climbingTrackPointList in climbingTrackPoints)
            {
                var averageClimbSpeed = GetAverageSpeed(climbingTrackPointList);
                averageClimbSpeedList.Add(averageClimbSpeed);
            }

            return averageClimbSpeedList.Average();
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
