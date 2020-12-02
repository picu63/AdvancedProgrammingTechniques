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
        double GetTotalDistance(IEnumerable<TrackPoint> coordinates);
        double GetClimbingDistance(IEnumerable<TrackPoint> coordinates);
        double GetDescentDistance(IEnumerable<TrackPoint> coordinates);
        double GetFlatDistance(IEnumerable<TrackPoint> coordinates, double range);
    }
}
