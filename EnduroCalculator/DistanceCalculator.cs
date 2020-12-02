using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using EnduroCalculator.Interfaces;
using EnduroLibrary;

namespace EnduroCalculator
{
    public class DistanceCalculator : IDistanceCalculator
    {
        public double GetTotalDistance(IEnumerable<TrackPoint> coordinates)
        {
            var geoCoordinates = coordinates.Select((t) => new GeoCoordinate(t.Latitude, t.Longitude));

            double initDistance = 0;
            while (true)
            {
                //Weź pierwsze 2 z listy,
                //Sprawdź czy drugi jest nullem jeśli jest to zwróć dystans
                //następnie zmierz dystans pomiędzy nimi
                //i dodaj go do 
                var first = geoCoordinates.FirstOrDefault();
                var second = geoCoordinates.Skip(1).FirstOrDefault();
                if (second is null)
                {
                    return initDistance;
                }

                var distance = initDistance + first.GetDistanceTo(second);
                geoCoordinates = geoCoordinates.Skip(1);
                initDistance = distance;
            }
        }

        public double GetClimbingDistance(IEnumerable<TrackPoint> coordinates)
        {
            throw new NotImplementedException();
        }

        public double GetDescentDistance(IEnumerable<TrackPoint> coordinates)
        {
            throw new NotImplementedException();
        }

        public double GetFlatDistance(IEnumerable<TrackPoint> coordinates, double range)
        {
            throw new NotImplementedException();
        }
    }
}
