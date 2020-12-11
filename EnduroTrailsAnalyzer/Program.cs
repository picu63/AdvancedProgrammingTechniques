using System;
using System.IO;
using System.Linq;
using System.Threading.Channels;
using EnduroTrackReader;
using EnduroCalculator;
using EnduroCalculator.Calculators;
using EnduroLibrary;

namespace EnduroTrailsAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = args.FirstOrDefault();
            if (!File.Exists(filePath)) return;

            var trackReader = new TrackReader(filePath);
            var points = trackReader.GetAllPoints();
            var track = new Track(points.ToList());

            new CalculatorService(track)
                .AddCalculator(new DistanceCalculator())
                .AddCalculator(new ElevationCalculator())
                .AddCalculator(new SpeedCalculator())
                .AddCalculator(new TimeCalculator())
                .SetSlope(2)
                .AddTimeFilter(7200)
                .CalculateAll()
                .PrintAllCalculations();
        }
    }
}
