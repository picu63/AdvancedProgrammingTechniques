namespace EnduroCalculator
{
    public interface ICalculations
    {
        IPrintCalculations CalculateTrack(ICalculator calculator);
        IPrintCalculations CalculateAll();
    }
}