using System;
using System.Collections.Generic;
using System.Text;
using EnduroLibrary;

namespace EnduroCalculator.Interfaces
{
    public interface ITimeCalculator
    {
        double GetTotalTrackTime(IEnumerable<TrackPoint> coordinates);
        double GetClimbingTime(IEnumerable<TrackPoint> coordinates);
        double GetDescentTime(IEnumerable<TrackPoint> coordinates);
        double GetFlatTime(IEnumerable<TrackPoint> coordinates);
    }
}
