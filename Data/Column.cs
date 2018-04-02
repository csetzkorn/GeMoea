namespace Data
{
    public class Column : IColumn
    {
        public string OrginalColumnName { get; set; }
        public string NewColumnName { get; set; }
        public DataType DataType { get; set; }
        public string MissingValueIndicatorString { get; set; }
        public IMissingValueReplacementStrategy MissingValueReplacementStrategy { get; set; }

        public Column(string orginalColumnName, string newColumnName, DataType dataType, string missingValueIndicatorString, IMissingValueReplacementStrategy missingValueReplacementStrategy)
        {
            OrginalColumnName = orginalColumnName;
            if (string.IsNullOrEmpty(newColumnName))
            {
                NewColumnName = orginalColumnName;
            }
            DataType = dataType;
            MissingValueIndicatorString = missingValueIndicatorString;
            MissingValueReplacementStrategy = missingValueReplacementStrategy;
        }
    }
}
