using System.Collections.Generic;

namespace Data
{
    public class DataSet : IDataSet
    {
        public string[,] OriginalData { get; set; }
        public double[,] MappedData { get; set; }
        public IList<IColumn> OriginalColumns { get; set; }
        public Dictionary<string, int> MappedColumns { get; set; }

        public DataSet(IList<IColumn> originalColumns, string[,] originalData)
        {
            OriginalColumns = originalColumns;
            OriginalData = originalData;

            SeedMappedData();
        }

        private string GetNewMappedName(string name)
        {
            return name.Replace("[", "").Replace("]", "").Replace("(", "").Replace(")", "");
        }

        private void SeedMappedData()
        {
            var originalDataColumnPosition = 0;
            var newDataColumnPosition = 0;
            MappedColumns = new Dictionary<string,int>();
            var listOfNewColumnData = new List<string[]>();
            var listOfUniqueData = new List<List<string>>();
            var numberOfRows = OriginalData.GetLength(0);

            foreach (var originalColumn in OriginalColumns)
            {
                var originalColumnData = OriginalData.GetColumn(originalDataColumnPosition);

                listOfNewColumnData.Add(originalColumn.MissingValueReplacementStrategy.GetColumn(originalColumnData, originalColumn.MissingValueIndicatorString));

                if (originalColumn.DataType == DataType.Nominal)
                {
                    var uniqueValues = originalColumnData.GetUniqueValues(originalColumn.MissingValueIndicatorString);
                    listOfUniqueData.Add(uniqueValues);

                    foreach (var uniqueValue in uniqueValues)
                    {
                        MappedColumns.Add($"[ {GetNewMappedName(originalColumn.NewColumnName)}_{uniqueValue.Replace("]", "").Replace("(", "").Replace(")", "").Replace(" ", "-")}] ", newDataColumnPosition);
                        
                        newDataColumnPosition++;
                    }
                }
                else
                {
                    MappedColumns.Add($"[{GetNewMappedName(originalColumn.NewColumnName)}]", newDataColumnPosition);
                    
                    newDataColumnPosition++;

                    listOfUniqueData.Add(null);
                }

                originalDataColumnPosition++;
            }

            MappedData = new double[numberOfRows, newDataColumnPosition];

            newDataColumnPosition = 0;
            originalDataColumnPosition = 0;
            foreach (var originalColumn in OriginalColumns)
            {
                if (originalColumn.DataType == DataType.Nominal)
                {
                    var uniqueValues = listOfUniqueData[originalDataColumnPosition];

                    for (var row = 0; row < numberOfRows; row++)
                    {
                        for (var c = 0; c < uniqueValues.Count; c++)
                        {
                            if (listOfNewColumnData[originalDataColumnPosition][row] == uniqueValues[c])
                            {
                                MappedData[row, newDataColumnPosition + c] = 1.0;
                            }
                            else
                            {
                                MappedData[row, newDataColumnPosition + c] = 0.0;
                            }
                        }
                    }

                    newDataColumnPosition = newDataColumnPosition + uniqueValues.Count;
                }
                else
                {
                    for (var row = 0; row < numberOfRows; row++)
                    {
                        MappedData[row, newDataColumnPosition] = double.Parse(listOfNewColumnData[originalDataColumnPosition][row]);
                    }

                    newDataColumnPosition++;
                }

                originalDataColumnPosition++;
            }
        }
    }
}
