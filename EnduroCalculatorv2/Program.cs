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

            var calculatorService = new CalculatorService(track)
                .AddCalculator(new DistanceCalculator())
                .AddCalculator(new ElevationCalculator())
                .AddCalculator(new SpeedCalculator())
                .AddCalculator(new TimeCalculator())
                .SetSlope(2)
                .AddTimeFilter(7200)
                .CalculateAll();
            calculatorService.PrintAllCalculations();
        }
    }
}
