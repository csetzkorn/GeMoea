namespace Data
{
    public class MeanContinousValueMissingStrategy : IContinousValueMissingStrategy
    {
        public double[] GetColumn(string[] columnData, string missingValueIndicator)
        {
            var sum = 0.0;
            var counter = 0;
            var returnArray = new double[columnData.Length];

            foreach (var row in columnData)
            {
                if (!double.TryParse(row, out double num)) continue;
                sum += num;
                counter++;
            }

            var mean = sum / counter;
            counter = 0;
            foreach (var row in columnData)
            {
                if (row.Equals(missingValueIndicator))
                {
                    returnArray[counter] = mean;
                }
                else
                {
                    returnArray[counter] = double.Parse(row);
                }
                counter++;
            }

            return returnArray;
        }
    }
}
