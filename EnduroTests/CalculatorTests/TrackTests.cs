using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnduroCalculator;
using EnduroCalculator.Interfaces;
using EnduroLibrary;
using NUnit.Framework;

namespace EnduroTests.CalculatorTests
{
    public class TrackTests
    {
        List<double> altitudes = new List<double>(){2,3,5,2,1,4,6,7,6,5,3,6,9,3,1,1,1,1};
        private List<TrackPoint> trackPoints
        {
            get { return altitudes.Select(a => new TrackPoint() {Altitude = a}).ToList(); }
        }
        private readonly TrackCalculator _trackCalculator = new TrackCalculator();
        [Test]
        public void GetClimbingSections()
        {
            var tracks = _trackCalculator.GetClimbingSections(this.trackPoints).ToList();
            PrintTracks(tracks);
            Assert.That(tracks.Count() == 3);
        }

        [Test]
        public void GetDescentSections()
        {
            var tracks = _trackCalculator.GetDescentSections(this.trackPoints).ToList();
            PrintTracks(tracks);
            Assert.That(tracks.Count() == 3);
        }

        [Test]
        public void GetFlatSections()
        {
            double range = 0.5;
            var tracks = _trackCalculator.GetFlatSections(this.trackPoints, range).ToList();
            PrintTracks(tracks);
            //Assert.That(tracks.Count() == 3);
        }

        private static void PrintTracks(List<List<TrackPoint>> tracks)
        {
            for (int i = 0; i < tracks.Count(); i++)
            {
                var track = tracks[i];
                Console.Write("Track " + i + ":  ");
                foreach (var trackPoint in track)
                {
                    Console.Write(trackPoint.Altitude + "  ");
                }

                Console.WriteLine();
            }
        }
    }
}
