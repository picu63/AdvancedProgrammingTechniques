using System;
using System.Collections.Generic;
using System.Text;
using EnduroLibrary;

namespace EnduroCalculator.Interfaces
{
    public interface IElevationCalculator
    {
        double GetMinimumElevation(IEnumerable<TrackPoint> trackPoints);
        double GetMaximumElevation(IEnumerable<TrackPoint> trackPoints);
        double GetAverageElevation(IEnumerable<TrackPoint> trackPoints);
        double GetTotalClimbing(IEnumerable<TrackPoint> trackPoints);
        double GetTotalDescent(IEnumerable<TrackPoint> trackPoints);
        double GetFinalBalance(IEnumerable<TrackPoint> trackPoints);
    }
}