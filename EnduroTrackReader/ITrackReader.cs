using System;
using System.Collections.Generic;
using System.Text;
using EnduroLibrary;

namespace EnduroTrackReader
{
    public interface ITrackReader
    {
        IEnumerable<TrackPoint> GetAllPoints();

    }
}
