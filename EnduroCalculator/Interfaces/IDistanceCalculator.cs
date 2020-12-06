using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Device.Location;
using System.Linq;
using EnduroLibrary;

namespace EnduroCalculator.Interfaces
{
    public interface IDistanceCalculator
    {
        double GetTotalDistance(ICollection<TrackPoint> trackPoints);
        double GetClimbingDistance(ICollection<TrackPoint> coordinates, double slopeDegree= 5);
        double GetDescentDistance(ICollection<TrackPoint> coordinates, double slopeDegree= 5);
        double GetFlatDistance(ICollection<TrackPoint> coordinates, double maxSlopeDegree= 5);
    }
}
