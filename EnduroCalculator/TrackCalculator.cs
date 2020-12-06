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
        public IEnumerable<List<TrackPoint>> GetClimbingSections(IEnumerable<TrackPoint> trackPoints)
        {
            var climbingSections = new List<List<TrackPoint>>();
            var tp = trackPoints.ToList();
            var currentSection = new List<TrackPoint>();
            for (var i = 1; i < tp.Count; i++)
            {
                var current = tp[i];
                var previous = tp[i - 1];
                if (current.Altitude > previous.Altitude)
                {
                    currentSection.Add(previous);
                    currentSection.Add(current);
                }
                else if(currentSection.Count >= 2)
                {
                    climbingSections.Add(currentSection);
                    currentSection = new List<TrackPoint>();
                }
            }
            if (currentSection.Count >= 2)
            {
                climbingSections.Add(currentSection);
            }
            var climbingSectionsDistincted = climbingSections
                .Select(track => track.DistinctBy(point => point.Altitude).ToList());

            return climbingSectionsDistincted;
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

        public IEnumerable<List<TrackPoint>> GetDescentSections(IEnumerable<TrackPoint> trackPoints)
        {
            var tp = trackPoints.ToList();
            var descentSections = new List<List<TrackPoint>>();
            var currentSections = new List<TrackPoint>();
            for (var i = 1; i < tp.Count; i++)
            {
                var current = tp[i];
                var previous = tp[i - 1];
                if (current.Altitude < previous.Altitude)
                {
                    currentSections.Add(previous);
                    currentSections.Add(current);
                }
                else if (currentSections.Count >= 2)
                {
                    descentSections.Add(currentSections);
                    currentSections = new List<TrackPoint>();
                }
            }
            if (currentSections.Count >= 2)
            {
                descentSections.Add(currentSections);
            }
            var descentSectionsDistincted = descentSections
                .Select(track => track.DistinctBy(point => point.Altitude).ToList());

            return descentSectionsDistincted;
        }

        public IEnumerable<List<TrackPoint>> GetFlatSections(IEnumerable<TrackPoint> trackPoints, double range)
        {
            var tp = trackPoints.ToList();
            var flatSections = new List<List<TrackPoint>>();
            var currentSection = new List<TrackPoint>();
            for (var i = 1; i < tp.Count; i++)
            {
                var current = tp[i];
                var previous = tp[i - 1];
                if (Math.Abs(current.Altitude - previous.Altitude) <= range)
                {
                    currentSection.Add(previous);
                    currentSection.Add(current);
                }
                else if (currentSection.Count >= 2)
                {
                    flatSections.Add(currentSection);
                    currentSection = new List<TrackPoint>();
                }
            }
            if (currentSection.Count >= 2)
            {
                flatSections.Add(currentSection);
            }
            var flatSectionsDistincted = flatSections
                .Select(track => track.DistinctBy(point => point.Altitude).ToList());

            return flatSectionsDistincted;
        }
        
        public IEnumerable<List<TrackPoint>> GetSections(IEnumerable<TrackPoint> trackPoints, Func<TrackPoint, TrackPoint, double, bool> predicate, double range = 0.05)
        {

            var tp = trackPoints.ToList();
            var sections = new List<List<TrackPoint>>();
            var currentSection = new List<TrackPoint>();
            for (var i = 1; i < tp.Count; i++)
            {
                var current = tp[i];
                var previous = tp[i - 1];
                if (predicate(current, previous, range))
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
    }

    public enum Direction
    {
        Climbing,
        Descending,
        Flat,
    }
}
