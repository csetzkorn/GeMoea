using System.Collections.Generic;

namespace Data
{
    public class CsvFlatFile : IFlatFile
    {
        public string[,] Data { get; set; }

        public CsvFlatFile(string folderFilename)
        {
            Data = Import(folderFilename, ',', true, false);
        }

        // TODO factor out into helper class
        private static string[,] Import(string file, char delimiter, bool ignoreHeadline, bool removeQuoteSign)
        {
            var list = ReadCsvFile(file, delimiter, ignoreHeadline, removeQuoteSign);

            var arrayToReturn = new string[list.Count, list[0].Length];
            for(var row = 0; row < list.Count; row++)
            {
                for (var column = 0; column < list[row].Length; column++)
                {
                    arrayToReturn[row, column] = list[row][column].Trim();
                }
            }

            return arrayToReturn;
        }

        // TODO factor out into helper class
        private static List<string[]> ReadCsvFile(string filename, char csvDelimiter, bool ignoreHeadline, bool removeQuoteSign)
        {
            var lst = new List<string[]>();

            var currentLineNumner = 0;

            // Read the file and display it line by line.  
            using (var file = new System.IO.StreamReader(filename))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    currentLineNumner++;
                    var strAr = line.Split(csvDelimiter);                  

                    if (removeQuoteSign) strAr = RemoveQouteSign(strAr);

                    if (ignoreHeadline)
                    {
                        if (currentLineNumner != 1) lst.Add(strAr);
                    }
                    else
                    {
                        lst.Add(strAr);
                    }
                }
            }

            return lst;
        }
        private static string[] RemoveQouteSign(string[] ar)
        {
            for (var i = 0; i < ar.Length; i++)
            {
                if (ar[i].StartsWith("\"") || ar[i].StartsWith("'")) ar[i] = ar[i].Substring(1);
                if (ar[i].EndsWith("\"") || ar[i].EndsWith("'")) ar[i] = ar[i].Substring(0, ar[i].Length - 1);

            }
            return ar;
        }
    }
}
