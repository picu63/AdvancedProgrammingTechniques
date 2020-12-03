using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using EnduroCalculator.Interfaces;
using EnduroLibrary;

namespace EnduroCalculator
{
    public class TimeCalculator : ITimeCalculator
    {
        public double GetAverageElevation(IEnumerable<TrackPoint> coordinates)
        {
            throw new NotImplementedException();
        }

        public double GetClimbingTime(IEnumerable<TrackPoint> coordinates)
        {
            throw new NotImplementedException();
        }

        public double GetDescentTime(IEnumerable<TrackPoint> coordinates)
        {
            throw new NotImplementedException();
        }

        public double GetFinalBalance(IEnumerable<TrackPoint> coordinates)
        {
            throw new NotImplementedException();
        }

        public double GetFlatTime(IEnumerable<TrackPoint> coordinates)
        {
            throw new NotImplementedException();
        }

        public double GetMaximumElevation(IEnumerable<TrackPoint> coordinates)
        {
            throw new NotImplementedException();
        }

        public double GetMinimumElevation(IEnumerable<TrackPoint> coordinates)
        {
            throw new NotImplementedException();
        }

        public double GetTotalClimbing(IEnumerable<TrackPoint> coordinates)
        {
            throw new NotImplementedException();
        }

        public double GetTotalDescent(IEnumerable<TrackPoint> coordinates)
        {
            throw new NotImplementedException();
        }

        public double GetTotalTrackTime(IEnumerable<TrackPoint> coordinates)
        {
            throw new NotImplementedException();
        }
    }
}
