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

            Console.WriteLine("\nDistance");
            Console.WriteLine($"Total: {distanceCalculator.GetTotalDistance(trackPoints):0.##} m");
            Console.WriteLine($"Climbing: {distanceCalculator.GetClimbingDistance(trackPoints):0.##} m");
            Console.WriteLine($"Descent: {distanceCalculator.GetDescentDistance(trackPoints):0.##} m");
            Console.WriteLine($"Flat: {distanceCalculator.GetFlatDistance(trackPoints):0.##} m");

            Console.WriteLine("\nSpeed");
            Console.WriteLine($"Minimum: {speedCalculator.GetMinimumSpeed(trackPoints):0.####} m/s");
            Console.WriteLine($"Maximum: {speedCalculator.GetMaximumSpeed(trackPoints):0.##} m/s");
            Console.WriteLine($"Average: {speedCalculator.GetAverageSpeed(trackPoints):0.##} m/s");
            Console.WriteLine($"Average climbing: {speedCalculator.GetAverageClimbingSpeed(trackPoints):0.##} m/s");
            Console.WriteLine($"Average descent: {speedCalculator.GetAverageDescentSpeed(trackPoints):0.##} m/s");
            Console.WriteLine($"Average flat: {speedCalculator.GetAverageFlatSpeed(trackPoints):0.##} m/s");

            Console.WriteLine("\nElevation");
            Console.WriteLine($"Minimum: {elevationCalculator.GetMinimumElevation(trackPoints):0.##} m");
            Console.WriteLine($"Maximum: {elevationCalculator.GetMaximumElevation(trackPoints):0.##} m");
            Console.WriteLine($"Average: {elevationCalculator.GetAverageElevation(trackPoints):0.##} m");
            Console.WriteLine($"Total climbing: {elevationCalculator.GetTotalClimbing(trackPoints):0.##} m");
            Console.WriteLine($"Total descent: {elevationCalculator.GetTotalDescent(trackPoints):0.##} m");
            Console.WriteLine($"Final balance: {elevationCalculator.GetFinalBalance(trackPoints):0.##} m");

            Console.WriteLine("\nTime");
            Console.WriteLine($"Total: {timeCalculator.GetTotalTrackTime(trackPoints):0.##} s");
            Console.WriteLine($"Climbing: {timeCalculator.GetClimbingTime(trackPoints):0.##} s");
            Console.WriteLine($"Descent: {timeCalculator.GetDescentTime(trackPoints):0.##} s");
            Console.WriteLine($"Flat: {timeCalculator.GetFlatTime(trackPoints):0.##} s");
        }
    }
}
