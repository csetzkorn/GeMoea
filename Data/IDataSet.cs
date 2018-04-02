using System.Collections.Generic;

namespace Data
{
    public interface IDataSet
    {
        //string[,] array = new string[,]
        string[,] OriginalData { get; set; }
        double[,] MappedData { get; set; }
        IList<IColumn> OriginalColumns { get; set; }
        Dictionary<string,int> MappedColumns { get; set; }

        //int GetNumberOfRows
    }
}
