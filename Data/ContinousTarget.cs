using System.Collections.Generic;
using System.IO;

namespace Data
{
    public class ContinousTarget : Target<double>
    {
        public sealed override List<double> Values { get; set; }

        public ContinousTarget(string folderAndFileName)
        {
            Values = new List<double>();
            using (var file = new StreamReader(folderAndFileName))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    Values.Add(double.Parse(line.Trim()));
                }
            }
        }
    }
}
