namespace Data
{
    public class MeanContinousValueMissingStrategy : IMissingValueReplacementStrategy
    {
        public string[] GetColumn(string[] columnData, string missingValueIndicator)
        {
            var sum = 0.0;
            var counter = 0;
            var returnArray = new string[columnData.Length];

            foreach (var row in columnData)
            {
                if (!double.TryParse(row, out double num)) continue;
                sum += num;
                counter++;
            }

            var mean = sum / counter;

            for (var c = 0; c < columnData.Length; c++)
            {
                if (columnData[c].Equals(missingValueIndicator))
                {
                    // ReSharper disable once SpecifyACultureInStringConversionExplicitly
                    returnArray[c] = mean.ToString();
                }
                else
                {
                    returnArray[c] = columnData[c];
                }
            }

            return returnArray;
        }
    }
}
