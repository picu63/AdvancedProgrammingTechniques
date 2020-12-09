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
        public double GetMinimumSpeed(ICollection<TrackPoint> trackPoints)
        {
            var trackCalculator = new TrackCalculator();
            var velocities = trackCalculator.GetAllVelocities(trackPoints);
            var min = velocities.Min();
            return min;
        }

        public double GetMaximumSpeed(ICollection<TrackPoint> trackPoints)
        {
            var trackCalculator = new TrackCalculator();
            var velocities = trackCalculator.GetAllVelocities(trackPoints);
            var max = velocities.Max();
            return max;
        }

        public double GetAverageSpeed(ICollection<TrackPoint> trackPoints)
        {
            var trackCalculator = new TrackCalculator();
            var velocities = trackCalculator.GetAllVelocities(trackPoints).ToList();
            var average = velocities.Average();
            return average;
        }

        public double GetAverageClimbingSpeed(ICollection<TrackPoint> trackPoints, double slopeDegree)
        {
            var trackCalculator = new TrackCalculator();
            var climbingTracks = trackCalculator.GetClimbingSections(trackPoints, slopeDegree);
            var averageClimbSpeedList = new List<double>();
            foreach (var climbingTrack in climbingTracks)
            {
                var averageClimbSpeed = GetAverageSpeed(climbingTrack);
                averageClimbSpeedList.Add(averageClimbSpeed*climbingTrack.Count);
            }

            return averageClimbSpeedList.Average()/climbingTracks.Count;
        }

        public double GetAverageDescentSpeed(ICollection<TrackPoint> trackPoints, double slopeDegree)
        {
            var trackCalculator = new TrackCalculator();
            var descentSections = trackCalculator.GetDescentSections(trackPoints, slopeDegree);
            var averageDescentSpeedList = new List<double>();
            foreach (var descentSection in descentSections)
            {
                var averageDescentSpeed = GetAverageSpeed(descentSection);
                averageDescentSpeedList.Add(averageDescentSpeed*descentSection.Count);
            }

            return averageDescentSpeedList.Average()/descentSections.Count;
        }

        public double GetAverageFlatSpeed(ICollection<TrackPoint> trackPoints, double maxSlopeDegree)
        {
            var trackCalculator = new TrackCalculator();
            var flatTracks = trackCalculator.GetFlatSections(trackPoints, maxSlopeDegree);
            var averageFlatSpeedList = new List<double>();
            foreach (var flatTrack in flatTracks)
            {
                var averageFlatSpeed = GetAverageSpeed(flatTrack);
                averageFlatSpeedList.Add(averageFlatSpeed*flatTrack.Count);
            }

            return averageFlatSpeedList.Average()/flatTracks.Count;
        }
    }
}
