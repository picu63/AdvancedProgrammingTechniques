using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnduroLibrary;

namespace EnduroCalculatorv2
{
    public class CalculatorService : ICalculations, IPrintCalculations
    {
        private readonly Track _track;
        private readonly List<ICalculator> _calculators = new List<ICalculator>();
        public CalculatorService(Track track)
        {
            this._track = track;
        }

        public CalculatorService AddCalculator(ICalculator calculator)
        {
            this._calculators.Add(calculator);
            return this;
        }

        public CalculatorService SetSlope(double slopePercentage)
        {
            foreach (var calculator in _calculators)
            {
                calculator.Slope = slopePercentage;
            }

            return this;
        }

        public IPrintCalculations CalculateAll()
        {
            foreach (var calculator in _calculators)
            {
                foreach (var trackPoint in _track.TrackPoints)
                {
                    calculator.Calculate(trackPoint);
                }
            }

            return this;
        }

        public IPrintCalculations CalculateTrack(ICalculator calculator)
        {
            _calculators.Add(calculator);
            foreach (var trackPoint in _track.TrackPoints)
            {
                calculator.Calculate(trackPoint);
            }
            return this;
        }

        public void PrintAllCalculations()
        {
            foreach (var calculator in _calculators)
            {
                calculator.PrintResult();
                Console.WriteLine();
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

        public CalculatorService AddTimeFilter(double seconds)
        {
            foreach (var calculator in _calculators)
            {
                calculator.TimeFilter = seconds;
            }

            return this;
        }
    }
}
