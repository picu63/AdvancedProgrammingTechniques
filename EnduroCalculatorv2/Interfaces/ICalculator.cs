using EnduroLibrary;

namespace EnduroCalculatorv2
{
    public interface ICalculator
    {
        public void Calculate(TrackPoint nextPoint);
        public double Slope { get; set; }
        public double TimeFilter { get; set; }
        public void PrintResult();
    }
}