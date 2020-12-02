using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Text;

namespace EnduroLibrary
{
    public class TrackPoint
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime DateTime { get; set; }
        public double Altitude { get; set; }

        public GeoCoordinate GetGeoCoordinate() => new GeoCoordinate(this.Latitude, Longitude, Altitude);
    }
}
