namespace Data
{
    public interface IColumn
    {
        string OrginalColumnName { get; set; }
        string NewColumnName { get; set; }
        DataType DataType { get; set; }
        string MissingValueIndicatorString { get; set; }
    }
}
