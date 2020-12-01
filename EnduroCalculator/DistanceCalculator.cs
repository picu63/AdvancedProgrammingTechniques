using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using EnduroCalculator.Interfaces;

namespace EnduroCalculator
{
    public class DistanceCalculator : IDistanceCalculator
    {
        public double GetTotalDistance(IEnumerable<GeoCoordinate> coordinates)
        {
            throw new NotImplementedException();
        }

        public double GetClimbingDistance(IOrderedEnumerable<GeoCoordinate> coordinates)
        {
            throw new NotImplementedException();
        }

        public double GetDescentDistance(IOrderedEnumerable<GeoCoordinate> coordinates)
        {
            throw new NotImplementedException();
        }

        public double GetFlatDistance(IOrderedEnumerable<GeoCoordinate> coordinates, double range)
        {
            throw new NotImplementedException();
        }
    }
}
