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
        double GetTotalDistance(ICollection<TrackPoint> coordinates);
        double GetClimbingDistance(ICollection<TrackPoint> coordinates);
        double GetDescentDistance(ICollection<TrackPoint> coordinates);
        double GetFlatDistance(ICollection<TrackPoint> coordinates, double range);
    }
}
