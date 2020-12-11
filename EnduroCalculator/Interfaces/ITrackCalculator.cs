using EnduroLibrary;

namespace EnduroCalculator
{
    public interface ITrackCalculator
    {
        void Calculate(TrackPoint nextPoint);
        double Slope { get; set; }
        double TimeFilter { get; set; }
        void PrintResult();
    }
}