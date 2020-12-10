namespace EnduroCalculatorv2
{
    public interface ICalculations
    {
        IPrintCalculations CalculateTrack(ICalculator calculator);
        IPrintCalculations CalculateAll();
    }
}