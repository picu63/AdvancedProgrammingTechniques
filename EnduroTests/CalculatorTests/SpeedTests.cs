using System;
using System.Collections.Generic;
using System.Text;
using EnduroCalculator;
using EnduroCalculator.Interfaces;
using EnduroLibrary;
using NUnit.Framework;

namespace EnduroTests.CalculatorTests
{
    public class SpeedTests
    {
        List<TrackPoint> trackPoints = new List<TrackPoint>()
        {
            new TrackPoint()
            {
                Altitude = 2
            },
            new TrackPoint()
            {
                Altitude = 3
            },
            new TrackPoint()
            {
                Altitude = 5
            },
            new TrackPoint()
            {
                Altitude = 2
            },
            new TrackPoint()
            {
                Altitude = 1
            },
            new TrackPoint()
            {
                Altitude = 4
            },
            new TrackPoint(){ Altitude = 6}
        };
        [Test]
        public void GetClimbingAverageSpeed()
        {
            SpeedCalculator speedCalculator = new SpeedCalculator();
            var tracks = speedCalculator.GetClimbingTracks(this.trackPoints);
            foreach (var track in tracks)
            {
                foreach (var trackPoint in track)
                {
                    Console.Write(trackPoint.Altitude + ',');
                }

                Console.WriteLine();
            }
        }
    }
}
