using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using EnduroLibrary;

namespace EnduroTrackReader
{
    public class TrackReader : ITrackReader
    {
        public TrackReader(string path)
        {
            _path = path;
        }

        private readonly string _path;


        public IEnumerable<TrackPoint> GetAllPoints()
        {
            XNamespace gpx = XNamespace.Get("http://www.topografix.com/GPX/1/1");
            XDocument gpxDoc = XDocument.Load(_path);

            var asd = gpxDoc.Descendants(gpx + "trkpt");
            
            var points = from point in gpxDoc.Descendants(gpx + "trkpt")
                         select new TrackPoint()
                         {
                             Latitude = double.Parse(point.Attribute("lon").Value, System.Globalization.CultureInfo.InvariantCulture),
                             Longitude = double.Parse(point.Attribute("lat").Value, System.Globalization.CultureInfo.InvariantCulture),
                             Altitude = double.Parse(point.Descendants(gpx + "ele").FirstOrDefault().Value, System.Globalization.CultureInfo.InvariantCulture),
                             DateTime = DateTime.Parse(point.Descendants(gpx + "time").FirstOrDefault().Value, System.Globalization.CultureInfo.InvariantCulture)
                         };
            return points;
        }
    }
}
