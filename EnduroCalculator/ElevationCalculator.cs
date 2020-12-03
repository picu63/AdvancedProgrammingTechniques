using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using EnduroCalculator.Interfaces;
using EnduroLibrary;

namespace EnduroCalculator
{
    public class ElevationCalculator : IElevationCalculator
    {

        public double GetMinimumElevation(IEnumerable<TrackPoint> coordinates)
        {
            // Należy pobrać wszystkie wysokości i pobrać najniższą z nich
            var geoCoordinates = coordinates.Select((t) => t.Altitude);

            var min = geoCoordinates.Min();
            return (min);
        }

        public double GetMaximumElevation(IEnumerable<TrackPoint> coordinates)
        {
            // Należy pobrać wszystkie wysokości i pobrać najwyższą z nich
            var geoCoordinates = coordinates.Select((t) => t.Altitude);

            var max = geoCoordinates.Max();
            return (max);
        }

        public double GetAverageElevation(IEnumerable<TrackPoint> coordinates)
        {
            // Należy pobrać wszystkie wysokości i pobrać z nich średnią
            var geoCoordinates = coordinates.Select((t) => t.Altitude);

            var avg = geoCoordinates.Average();
            return (avg);
        }

        public double GetTotalClimbing(IEnumerable<TrackPoint> coordinates)
        {
            throw new NotImplementedException();
        }

        public double GetTotalDescent(IEnumerable<TrackPoint> coordinates)
        {
  
            var geoCoordinates = coordinates.Select((t) => t.Altitude);

            var min = geoCoordinates.Min();
            var max = geoCoordinates.Max();
            var total = max - min;
            return (total);
        }

        public double GetFinalBalance(IEnumerable<TrackPoint> coordinates)
        {
            throw new NotImplementedException();
        }
    }
}
