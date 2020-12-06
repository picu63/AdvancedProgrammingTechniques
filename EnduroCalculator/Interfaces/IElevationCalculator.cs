using System;
using System.Collections.Generic;
using System.Text;
using EnduroLibrary;

namespace EnduroCalculator.Interfaces
{
    public interface IElevationCalculator
    {
        double GetMinimumElevation(ICollection<TrackPoint> trackPoints);
        double GetMaximumElevation(ICollection<TrackPoint> trackPoints);
        double GetAverageElevation(ICollection<TrackPoint> trackPoints);
        double GetTotalClimbing(ICollection<TrackPoint> trackPoints, double slopeDegree= 5);
        double GetTotalDescent(ICollection<TrackPoint> trackPoints, double slopeDegree= 5);
        double GetFinalBalance(ICollection<TrackPoint> trackPoints);
    }
}