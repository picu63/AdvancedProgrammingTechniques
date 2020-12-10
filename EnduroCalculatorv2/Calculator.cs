using EnduroCalculatorv2;
using EnduroLibrary;

namespace EnduroCalculator
{
    public abstract class Calculator : ICalculator
    {
        protected TrackPoint CurrentPoint;
        public virtual void Calculate(TrackPoint nextPoint)
        {
            if (CurrentPoint is null)
            {
                CurrentPoint = nextPoint;
                return;
            }

            CurrentDirection = PointsCalculator.CalculateDirectionWithSlope(CurrentPoint, nextPoint, Slope);
        }
        public AltitudeDirection CurrentDirection { get; set; }
        public virtual double Slope { get; set; }
        public virtual double TimeFilter { get; set; }
        public abstract void PrintResult();
    }
}