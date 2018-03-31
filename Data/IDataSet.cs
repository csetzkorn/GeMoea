namespace Data
{
    public interface IDataSet
    {
        //string[,] array = new string[,]
        string[,] OriginalData { get; set; }
        double[,] MappedData { get; set; }

        int GetNumberOfRows
    }
}
