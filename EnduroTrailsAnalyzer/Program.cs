using System;
using System.IO;
using System.Linq;
using System.Threading.Channels;
using EnduroTrackReader;
using EnduroCalculator;
using EnduroCalculator.Interfaces;

namespace EnduroTrailsAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = string.Empty;
            if (File.Exists(args.FirstOrDefault()))
            {
                filePath = args[0];
            }

            var trackReader = new TrackReader(filePath);
            var trackPoints = trackReader.GetAllPoints();
            IDistanceCalculator distanceCalculator;
            ISpeedCalculator speedCalculator;
            IElevationCalculator elevationCalculator;
            ITimeCalculator timeCalculator;
        }
    }
}
