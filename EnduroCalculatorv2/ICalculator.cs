using EnduroLibrary;

namespace EnduroCalculatorv2
{
    public interface ICalculator
    {
        public void SetupStart(TrackPoint startPoint);
        public void Calculate(TrackPoint nextPoint);
        public void AddTolerance(double toleranceInMeters);
        public void PrintResult();
    }
}