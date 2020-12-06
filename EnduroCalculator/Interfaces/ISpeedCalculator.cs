using System;
using System.Collections.Generic;
using System.Text;
using EnduroLibrary;

namespace EnduroCalculator.Interfaces
{
    public interface ISpeedCalculator
    {
        double GetMinimumSpeed(ICollection<TrackPoint> trackPoints);
        double GetMaximumSpeed(ICollection<TrackPoint> trackPoints);
        double GetAverageSpeed(ICollection<TrackPoint> trackPoints);
        double GetAverageClimbingSpeed(ICollection<TrackPoint> trackPoints, double slopeDegree= 5);
        double GetAverageDescentSpeed(ICollection<TrackPoint> trackPoints, double slopeDegree= 5);
        double GetAverageFlatSpeed(ICollection<TrackPoint> trackPoints, double maxSlopeDegree= 5);
    }
}
