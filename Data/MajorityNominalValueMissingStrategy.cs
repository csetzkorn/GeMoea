using System.Linq;

namespace Data
{
    public class MajorityNominalValueMissingStrategy : IMissingValueReplacementStrategy
    {
        public string[] GetColumn(string[] columnData, string missingValueIndicator)
        {
            var frequencyDictionary = columnData.GetFrequencyOfValues(missingValueIndicator);
            var returnArray = new string[columnData.Length];

            var majorityValue = frequencyDictionary.FirstOrDefault(x => x.Value.Equals(frequencyDictionary.Values.Max())).Key;

            for (var c = 0; c < columnData.Length; c++)
            {
                if (columnData[c].Equals(missingValueIndicator))
                {
                    returnArray[c] = majorityValue;
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
