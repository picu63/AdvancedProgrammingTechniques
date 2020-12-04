using System;
using System.Collections.Generic;
using System.Text;

namespace EnduroLibrary
{
    public class Track
    {

        public string Name { get; set; }
        public IEnumerable<TrackPoint> TrackPoints { get; set; }
    }
}
