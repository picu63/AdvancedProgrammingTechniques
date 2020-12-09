using System;
using System.Collections.Generic;
using System.Text;
using EnduroLibrary;

namespace EnduroCalculator.Interfaces
{
    public interface ITimeCalculator
    {
        double GetTotalTrackTime(ICollection<TrackPoint> coordinates);
        double GetClimbingTime(ICollection<TrackPoint> coordinates, double slopeDegree= 5);
        double GetDescentTime(ICollection<TrackPoint> coordinates, double slopeDegree= 5);
        double GetFlatTime(ICollection<TrackPoint> coordinates, double maxSlopeDegree= 5);
    }
}
