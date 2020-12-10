namespace EnduroCalculatorv2
{
    public interface IPrintCalculations
    {
        void PrintAllCalculations();
        IPrintCalculations PrintCalculationResult(ICalculator calculator);
    }
}