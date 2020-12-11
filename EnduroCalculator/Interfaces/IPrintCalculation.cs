namespace EnduroCalculator
{
    public interface IPrintCalculation
    {
        void PrintAllCalculations();
        IPrintCalculation PrintCalculationResult(ITrackCalculator calculator);
    }
}