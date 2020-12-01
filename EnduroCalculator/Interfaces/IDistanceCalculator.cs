using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Device.Location;
using System.Linq;

namespace EnduroCalculator.Interfaces
{
    interface IDistanceCalculator
    {
        double GetTotalDistance(IEnumerable<GeoCoordinate> coordinates);
        double GetClimbingDistance(IOrderedEnumerable<GeoCoordinate> coordinates);
        double GetDescentDistance(IOrderedEnumerable<GeoCoordinate> coordinates);
        double GetFlatDistance(IOrderedEnumerable<GeoCoordinate> coordinates, double range);
    }
}
