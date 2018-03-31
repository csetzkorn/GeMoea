using System.Collections.Generic;

namespace Data
{
    public class DataSet : IDataSet
    {
        public string[,] OriginalData { get; set; }
        public double[,] MappedData { get; set; }
        public IList<IColumn> Columns { get; set; }

        public DataSet(IList<IColumn> columns, string[,] originalData)
        {
            Columns = columns;
            OriginalData = originalData;
        }

        private void SeedMappedData()
        {
            
        }
    }
}
