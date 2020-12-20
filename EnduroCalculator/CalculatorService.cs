using System;
using System.Collections.Generic;
using EnduroLibrary;

namespace EnduroCalculator
{
    public class CalculatorService : ITrackCalculation, IPrintCalculation
    {
        private readonly Track _track;
        private readonly List<ITrackCalculator> _calculators = new List<ITrackCalculator>();
        public CalculatorService(Track track)
        {
            this._track = track;
        }

        public CalculatorService AddCalculator(ITrackCalculator calculator)
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

        public IPrintCalculation CalculateAll()
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

        public IPrintCalculation CalculateTrack(ITrackCalculator calculator)
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

        public IPrintCalculation PrintCalculationResult(ITrackCalculator calculator)
        {
            if (_calculators.Contains(calculator))
            {
                calculator.PrintResult();
            }

            return this;
        }

        /// <summary>
        /// Time filter for each subsequent point.
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public CalculatorService AddTimeFilter(TimeSpan timeSpan)
        {
            foreach (var calculator in _calculators)
            {
                calculator.TimeFilter = timeSpan.TotalSeconds;
            }

            return this;
        }
    }
}
