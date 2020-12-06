using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using EnduroCalculator.Interfaces;
using EnduroLibrary;

namespace EnduroCalculator
{
    public class TimeCalculator : ITimeCalculator
    {
        public double GetClimbingTime(IEnumerable<TrackPoint> trackPoints)
        {
            var trackCalculator = new TrackCalculator();
            var climbigTracks = trackCalculator.GetClimbingSections(trackPoints);
            TimeSpan total;

            foreach (var climbing in climbigTracks)
            {
                var times = climbing.Select((ct) => ct.DateTime);
                var minTime = times.Min();
                var maxTime = times.Max();
                var currentTotal = maxTime - minTime;
                total += currentTotal;
            }

            return total.TotalSeconds;
        }

        public double GetDescentTime(IEnumerable<TrackPoint> trackPoints)
        {
            var trackCalculator = new TrackCalculator();
            var descentTracks = trackCalculator.GetDescentSections(trackPoints);
            TimeSpan total ;

            foreach (var descent in descentTracks)
            {
                var times = descent.Select((dt) => dt.DateTime);
                var minTime = times.Min();
                var maxTime = times.Max();
                var currentTotal = maxTime - minTime;
                total += currentTotal;
            }

            return total.TotalSeconds;
        }

        public double GetFlatTime(IEnumerable<TrackPoint> trackPoints, double range)
        {
            var trackCalculator = new TrackCalculator();
            var flatTracks = trackCalculator.GetFlatSections(trackPoints,range);
            TimeSpan total;

            foreach (var flat in flatTracks)
            {
                var times = flat.Select((ft) => ft.DateTime);
                var minTime = times.Min();
                var maxTime = times.Max();
                var currentTotal = maxTime - minTime;
                total += currentTotal;
            }
            return total.TotalSeconds;

        }

        public double GetTotalTrackTime(IEnumerable<TrackPoint> trackPoints)
        {
            var times = trackPoints.Select((tt) => tt.DateTime);
            var minTime = times.Min();
            var maxTime = times.Max();
            var total = maxTime - minTime;
            return total.TotalSeconds;
        }
    }
}
