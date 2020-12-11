namespace EnduroCalculator
{
    public interface IPrintCalculations
    {
        void PrintAllCalculations();
        IPrintCalculations PrintCalculationResult(ICalculator calculator);
    }
}