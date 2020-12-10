using System;
using System.Linq;
using EnduroCalculator;
using EnduroLibrary;
using EnduroTrackReader;

namespace EnduroCalculatorv2
{
    class Program
    {
        static void Main(string[] args)
        {
            TrackReader trackReader = new TrackReader(args[0]);
            var points = trackReader.GetAllPoints();
            Track track = new Track(points.ToList());

            var calculatorProcessor = new CalculatorProcessor(track)
                .AddCalculator(new DistanceCalculator())
                .AddCalculator(new ElevationCalculator())
                .AddCalculator(new SpeedCalculator())
                .AddCalculator(new TimeCalculator())
                .CalculateTrack();
            calculatorProcessor.PrintAllCalculations();
        }
    }
}
