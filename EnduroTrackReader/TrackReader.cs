using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            //TODO Odczytać z XLinq
            var points = from trkseg in XDocument.Load(File.ReadAllText(_path)).XPathSelectElements("trkpt")
                select new TrackPoint()
                {
                    
                };
            throw new NotImplementedException();
        }
    }
}
