using System;
using System.Collections.Generic;
using System.Text;
using EnduroLibrary;

namespace EnduroCalculator.Interfaces
{
    public interface ISpeedCalculator
    {
        double GetMinimumSpeed(IEnumerable<TrackPoint> trackPoints);
        double GetMaximumSpeed(IEnumerable<TrackPoint> trackPoints);
        double GetAverageSpeed(IEnumerable<TrackPoint> trackPoints);
        double GetAverageClimbingSpeed(IEnumerable<TrackPoint> trackPoints);
        double GetAverageDescentSpeed(IEnumerable<TrackPoint> trackPoints);
        double GetAverageFlatSpeed(IEnumerable<TrackPoint> trackPoints);
    }
}
