namespace Data
{
    public interface IContinousValueMissingStrategy
    {
        double[] GetColumn(string[] columnData, string missingValueIndicator);
    }
}
