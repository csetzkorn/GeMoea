using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public class MajorityNominalValueMissingStrategy : IMissingValueReplacementStrategy
    {
        public string[] GetColumn(string[] columnData, string missingValueIndicator)
        {
            var frequencyDictionary = GetFrequencyOfvalues(columnData, missingValueIndicator);
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

        public Dictionary<string, int> GetFrequencyOfvalues(string[] stringArray, string missingValueIndicator)
        {
            var returnDictionary = new Dictionary<string,int>();

            foreach (var item in stringArray)
            {
                if (item.Equals(missingValueIndicator)) continue;
                if (!returnDictionary.ContainsKey(item))
                {
                    returnDictionary.Add(item, 1);
                }
                else
                {
                    returnDictionary[item]++;
                }
            }
            return returnDictionary;
        }
    }
}
