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
            var trackPoints = trackReader.GetAllPoints().Where(t => t.DateTime.Day == 30).ToList();
        
            IDistanceCalculator distanceCalculator = new DistanceCalculator();
            ISpeedCalculator speedCalculator = new SpeedCalculator();
            IElevationCalculator elevationCalculator = new ElevationCalculator();
            ITimeCalculator timeCalculator = new TimeCalculator();

            Console.WriteLine("Distance");
            Console.WriteLine($"Total: {distanceCalculator.GetTotalDistance(trackPoints)}");
            Console.WriteLine($"Climbing: {distanceCalculator.GetClimbingDistance(trackPoints)}");
            Console.WriteLine($"Descent: {distanceCalculator.GetDescentDistance(trackPoints)}");
            Console.WriteLine($"Flat: {distanceCalculator.GetFlatDistance(trackPoints, 0.05)}");

            Console.WriteLine("Speed");
            Console.WriteLine($"Minimum: {speedCalculator.GetMinimumSpeed(trackPoints)}");
            Console.WriteLine($"Maximum: {speedCalculator.GetMaximumSpeed(trackPoints)}");
            Console.WriteLine($"Average: {speedCalculator.GetAverageSpeed(trackPoints)}");
            Console.WriteLine($"Average climbing: {speedCalculator.GetAverageClimbingSpeed(trackPoints)}");
            Console.WriteLine($"Average descent: {speedCalculator.GetAverageDescentSpeed(trackPoints)}");
            Console.WriteLine($"Average flat: {speedCalculator.GetAverageFlatSpeed(trackPoints, 0.05)}");

            Console.WriteLine("Elevation");
            Console.WriteLine($"Minimum: {elevationCalculator.GetMinimumElevation(trackPoints)}");
            Console.WriteLine($"Maximum: {elevationCalculator.GetMaximumElevation(trackPoints)}");
            Console.WriteLine($"Average: {elevationCalculator.GetAverageElevation(trackPoints)}");
            Console.WriteLine($"Total climbing: {elevationCalculator.GetTotalClimbing(trackPoints)}");
            Console.WriteLine($"Total descent: {elevationCalculator.GetTotalDescent(trackPoints)}");
            Console.WriteLine($"Final balance: {elevationCalculator.GetFinalBalance(trackPoints)}");

            Console.WriteLine("Time");
            Console.WriteLine($"Total: {timeCalculator.GetTotalTrackTime(trackPoints)}");
            Console.WriteLine($"Climbing: {timeCalculator.GetClimbingTime(trackPoints)}");
            Console.WriteLine($"Descent: {timeCalculator.GetDescentTime(trackPoints)}");
            Console.WriteLine($"Flat: {timeCalculator.GetFlatTime(trackPoints, 0.05)}");
        }
    }
}
