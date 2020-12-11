using EnduroCalculator;
using EnduroLibrary;

namespace EnduroCalculator
{
    public abstract class TrackCalculator : ITrackCalculator
    {
        public virtual void Calculate(TrackPoint nextPoint)
        {
            if (CurrentPoint is null)
            {
                CurrentPoint = nextPoint;
                return;
            }

            CurrentDirection = DirectionCalculation.CalculateDirectionWithSlope(CurrentPoint, nextPoint, Slope);
        }
        protected TrackPoint CurrentPoint { get; set; }
        public virtual AltitudeDirection CurrentDirection { get; set; }
        public virtual double Slope { get; set; }
        public virtual double TimeFilter { get; set; }
        public abstract void PrintResult();
    }
}