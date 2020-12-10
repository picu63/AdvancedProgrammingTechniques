using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EnduroLibrary
{
    public class Track
    {
        public Track(ICollection<TrackPoint> trackPoints)
        {
            TrackPoints = trackPoints;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TrackPoint> TrackPoints { get; }
    }
}
