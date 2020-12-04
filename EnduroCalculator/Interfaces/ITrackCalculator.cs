using EnduroLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnduroCalculator.Interfaces
{
    public interface ITrackCalculator
    {
        IEnumerable<List<TrackPoint>> GetClimbingTracks(IEnumerable<TrackPoint> trackPoints);
        IEnumerable<List<TrackPoint>> GetDescentTracks(IEnumerable<TrackPoint> trackPoints);
        IEnumerable<List<TrackPoint>> GetFlatTracks(IEnumerable<TrackPoint> trackPoints, double range);
        IEnumerable<double> GetAllVelocities(IEnumerable<TrackPoint> trackPoints);
        double GetVelocity(TrackPoint startPoint, TrackPoint endPoint);
    }
}
