using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvolutionaryAlgorithm
{
    public class ObjectiveValues : IObjectiveValues
    {
        public double[] Maxs { get; set; }
        public int DominatedByCount { get; set; }
        public decimal[] Values { get; set; }
        public string[] Names { get; set; }
        public double[] Mins { get; set; }
        public Guid IndividualGuid { get; set; }
        public int Rank { get; set; }
        public decimal CrowdingDistance { get; set; }
        public IList<IObjectiveValues> DominatingObjectiveValues { get; set; }

        public ObjectiveValues(decimal[] values, string[] names, Guid individualGuid)
        {
            Values = values;
            Names = names;
            IndividualGuid = individualGuid;
            DominatingObjectiveValues = new List<IObjectiveValues>();
        }

        public List<KeyValuePair<string, decimal>> GetObjectiveValues()
        {
            return Values.Select((t, c) => new KeyValuePair<string, decimal>(Names[c], t)).ToList();
        }

        public override string ToString()
        {
            var kvps = GetObjectiveValues();

            var sb = new StringBuilder();
            foreach (var kvp in kvps)
            {
                sb.Append($"[{kvp.Key} : {kvp.Value}]");
            }
            sb.AppendLine();
            sb.AppendLine(" DominatedByCount: " + DominatedByCount);
            sb.AppendLine(" DominationCount: " + DominatingObjectiveValues.Count);
            sb.AppendLine(" Individual Guid: " + IndividualGuid);
            sb.AppendLine(" Rank: " + Rank);
            sb.AppendLine(" Distance: " + CrowdingDistance);

            return sb.ToString();
        }

        public void Reset()
        {
            DominatedByCount = 0;
            Rank = 0;
            CrowdingDistance = 0;
            DominatingObjectiveValues = new List<IObjectiveValues>();
        }

        public int CompareTo(object obj)
        {
            if (!(obj is IObjectiveValues)) throw new ArgumentException("object must implement IObjectiveValues !!!");
            var otherObjectiveValues = (IObjectiveValues)obj;

            var lessEqualCounter = 0;
            var lessCounter = 0;

            for (var c = 0; c < Values.Length; c++)
            {
                if (Values[c] <= otherObjectiveValues.Values[c])
                {
                    lessEqualCounter++;

                    if (Values[c] < otherObjectiveValues.Values[c])
                    {
                        lessCounter++;
                    }
                }
            }

            if (lessEqualCounter == Values.Length && lessCounter > 0)
            {
                return 1;
            }
            return 0;

            //            if (!(obj is IObjectiveValues)) throw new ArgumentException("object must implement IObjectiveValues !!!");
            //            var otherObjectiveValues = (IObjectiveValues)obj;
            //            var betterCounter = 0;
            //            var worseCounter = 0;
            //            for (var c = 0; c < Values.Length; c++)
            //            {
            //                if (Values[c] < otherObjectiveValues.Values[c])
            //                {
            //                    betterCounter++;
            //                }
            //                else
            //                {
            //                    if (Values[c] > otherObjectiveValues.Values[c])
            //                    {
            //                        worseCounter++;
            //                    }
            //                }
            //            }
            //
            //            if ((worseCounter == 0) && (betterCounter == 0))
            //            {
            //                return 0; // equal
            //            }
            //            if ((worseCounter == 0) && (betterCounter > 0))
            //            {
            //                return Values.Length == betterCounter ? 3 : 2; // 3 - strongly dominating, 2 - dominating
            //            }
            //            if (Values.Length == worseCounter)
            //            {
            //                return -2; // strongly dominated
            //            }
            //            if (betterCounter > 0)
            //            {
            //                return 1; // incomparable
            //            }
            //            return -1; // dominated
        }
    }
}
