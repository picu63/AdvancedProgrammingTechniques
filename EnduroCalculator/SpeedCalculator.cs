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
            var climbingTracks = trackCalculator.GetClimbingSections(trackPoints);
            var averageClimbSpeedList = new List<double>();
            foreach (var climbingTrack in climbingTracks)
            {
                var averageClimbSpeed = GetAverageSpeed(climbingTrack);
                averageClimbSpeedList.Add(averageClimbSpeed*climbingTrack.Count);
            }

            return averageClimbSpeedList.Average();
        }

        public double GetAverageDescentSpeed(IEnumerable<TrackPoint> trackPoints)
        {
            var trackCalculator = new TrackCalculator();
            var descentTracks = trackCalculator.GetClimbingSections(trackPoints);
            var averageDescentSpeedList = new List<double>();
            foreach (var descentTrack in descentTracks)
            {
                var averageDescentSpeed = GetAverageSpeed(descentTrack);
                averageDescentSpeedList.Add(averageDescentSpeed * descentTrack.Count);
            }

            return averageDescentSpeedList.Average();
        }

        public double GetAverageFlatSpeed(IEnumerable<TrackPoint> trackPoints, double range)
        {
            var trackCalculator = new TrackCalculator();
            var flatTracks = trackCalculator.GetFlatSections(trackPoints, range);
            var averageFlatSpeedList = new List<double>();
            foreach (var flatTrack in flatTracks)
            {
                var averageFlatSpeed = GetAverageSpeed(flatTrack);
                averageFlatSpeedList.Add(averageFlatSpeed*flatTrack.Count);
            }

            return averageFlatSpeedList.Average();
        }
    }
}
