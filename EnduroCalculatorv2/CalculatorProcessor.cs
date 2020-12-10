using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnduroLibrary;

namespace EnduroCalculatorv2
{
    public class CalculatorProcessor : IPrintCalculations
    {
        private readonly Track _track;
        private readonly List<ICalculator> _calculators = new List<ICalculator>();
        public CalculatorProcessor(Track track)
        {
            this._track = track;
        }

        public CalculatorProcessor AddCalculator(ICalculator calculator)
        {
            calculator.SetupStart(_track.TrackPoints.First());
            this._calculators.Add(calculator);
            return this;
        }

        public CalculatorProcessor SetTolerance(double toleranceInMeters)
        {
            foreach (var calculator in _calculators)
            {
                calculator.AddTolerance(toleranceInMeters);
            }

            return this;
        }

        public CalculatorProcessor SetupStartPoint(TrackPoint startPoint)
        {
            foreach (var calculator in _calculators)
            {
                calculator.SetupStart(startPoint);
            }

            return this;
        }

        public IPrintCalculations CalculateTrack()
        {
            foreach (var calculator in _calculators)
            {
                foreach (var trackPoint in _track.TrackPoints.Skip(1))
                {
                    calculator.Calculate(trackPoint);
                }
            }

            return this;
        }

        public void PrintAllCalculations()
        {
            foreach (var calculator in _calculators)
            {
                calculator.PrintResult();
            }
        }

        public IPrintCalculations PrintCalculationResult(ICalculator calculator)
        {
            if (_calculators.Contains(calculator))
            {
                calculator.PrintResult();
            }

            return this;
        }
    }

    public interface IPrintCalculations
    {
        void PrintAllCalculations();
        IPrintCalculations PrintCalculationResult(ICalculator calculator);
    }
}
