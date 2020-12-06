using System;
using System.Collections.Concurrent;
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
        public List<Track> GetDrives(ICollection<TrackPoint> trackPoints)
        {
            IDictionary<DateTime, List<TrackPoint>> drives = new ConcurrentDictionary<DateTime, List<TrackPoint>>();
            foreach (var date in trackPoints.Select(t => t.DateTime.Date))
            {
                drives.Keys.Add(date);
            }

            foreach (var drivesKey in drives.Keys)
            {
                drives.Add(drivesKey, trackPoints.Where(t => t.DateTime.Date == drivesKey).ToList());
            }

            return null;
        }

        public ICollection<List<TrackPoint>> GetClimbingSections(ICollection<TrackPoint> trackPoints, double range = 0.1)
        {
            var climbingSections = GetSections(trackPoints,
                (previous, current) => current.Altitude > previous.Altitude + range);
            var climbingSectionsDistincted = climbingSections
                .Select(track => track.DistinctBy(point => point.Altitude).ToList()).ToList();

            return climbingSectionsDistincted;
        }

        public ICollection<List<TrackPoint>> GetDescentSections(ICollection<TrackPoint> trackPoints, double range = 0.1)
        {
            var descentSections = GetSections(trackPoints,
                (previous, current) => current.Altitude < previous.Altitude - range);
            var descentSectionsDisctincted = descentSections
                .Select((track => track.DistinctBy(point => point.Altitude).ToList())).ToList();
            return descentSectionsDisctincted;
        }

        public ICollection<List<TrackPoint>> GetFlatSections(ICollection<TrackPoint> trackPoints, double range = 0.1)
        {
            var flatSections = GetSections(trackPoints,
                (previous, current) => Math.Abs(current.Altitude - previous.Altitude) <= range);
            var flatSectionsDistincted = flatSections
                .Select((track => track.DistinctBy(point => point.DateTime).ToList())).ToList();

            return flatSectionsDistincted;
        }
        
        public ICollection<List<TrackPoint>> GetSections(ICollection<TrackPoint> trackPoints, Func<TrackPoint, TrackPoint, bool> adjacentPointsPredicate)
        {

            var tp = trackPoints.ToList();
            var sections = new List<List<TrackPoint>>();
            var currentSection = new List<TrackPoint>();
            for (var i = 1; i < tp.Count; i++)
            {
                var current = tp[i];
                var previous = tp[i - 1];
                if (adjacentPointsPredicate(previous, current))
                {
                    currentSection.Add(previous);
                    currentSection.Add(current);
                }
                else if (currentSection.Count >= 2)
                {
                    sections.Add(currentSection);
                    currentSection = new List<TrackPoint>();
                }
            }
            if (currentSection.Count >= 2)
            {
                sections.Add(currentSection);
            }

            return sections;
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
            var distance = startPoint.GetGeoCoordinate().GetDistanceTo(endPoint.GetGeoCoordinate());
            var timeSpan = endPoint.DateTime - startPoint.DateTime;
            var timeInSeconds = timeSpan.TotalSeconds;
            return distance / timeInSeconds;
        }
    }

    public enum Direction
    {
        Climbing,
        Descending,
        Flat,
    }
}
