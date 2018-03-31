namespace Data
{
    public interface IMissingValueReplacementStrategy
    {
        string[] GetColumn(string[] columnData, string missingValueIndicator);
    }
}
