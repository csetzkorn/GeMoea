using System.Collections.Generic;

namespace Data
{
    public interface IDataSet
    {
        //string[,] array = new string[,]
        string[,] OriginalData { get; set; }
        double[,] MappedData { get; set; }
        IList<IColumn> Columns { get; set; }

        //int GetNumberOfRows
    }
}
