using System;
using System.Collections.Generic;
using System.Text;
using EnduroLibrary;

namespace EnduroCalculator.Interfaces
{
    public interface IElevationCalculator
    {
        double GetMinimumElevation(IEnumerable<TrackPoint> coordinates);
        double GetMaximumElevation(IEnumerable<TrackPoint> coordinates);
        double GetAverageElevation(IEnumerable<TrackPoint> coordinates);
        double GetTotalClimbing(IEnumerable<TrackPoint> coordinates);
        double GetTotalDescent(IEnumerable<TrackPoint> coordinates);
        double GetFinalBalance(IEnumerable<TrackPoint> coordinates);
    }
}