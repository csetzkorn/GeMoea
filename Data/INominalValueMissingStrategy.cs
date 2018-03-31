namespace Data
{
    public interface INominalValueMissingStrategy
    {
        string[] GetColumn(string[] columnData, string missingValueIndicator);
    }
}
