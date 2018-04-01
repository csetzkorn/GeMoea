using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Data
{
    public static class ArrayHelperMethods
    {
        public static T[] GetRow<T>(this T[,] array, int row)
        {
            if (!typeof(T).IsPrimitive)
                throw new InvalidOperationException("Not supported for managed types.");

            if (array == null)
                throw new ArgumentNullException(nameof(array));

            var cols = array.GetUpperBound(1) + 1;
            var result = new T[cols];
            var size = Marshal.SizeOf<T>();

            Buffer.BlockCopy(array, row * cols * size, result, 0, cols * size);

            return result;
        }

        public static T[] GetColumn<T>(this T[,] array, int column)
        {
            var numberofRows = array.GetLength(0);
            var returnArray = new T[numberofRows];
            for (var c = 0; c < numberofRows; c++)
            {
                returnArray[c] = array[c, column];
            }

            return returnArray;
        }

        public static Dictionary<T, int> GetFrequencyOfValues<T>(this T[] stringArray, T missingValueIndicator)
        {
            var returnDictionary = new Dictionary<T, int>();

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

        public static List<T> GetUniqueValues<T>(this T[] stringArray, T missingValueIndicator)
        {
            var returnList = new List<T>();

            foreach (var item in stringArray)
            {
                if (item.Equals(missingValueIndicator)) continue;
                if (returnList.Contains(item) == false)
                {
                    returnList.Add(item);
                }
            }
            return returnList;
        }
    }
}
