using System;
using System.Collections.Generic;

namespace EvolutionaryAlgorithm
{
    public interface IObjectiveValues : IComparable
    {
        double[] Values { get; set; }
        string[] Names { get; set; }
        int DominatedByCount { get; set; }
        int Rank { get; set; }
        double CrowdingDistance { get; set; }
        IList<IObjectiveValues> DominatingObjectiveValues { get; set; }
        Guid IndividualGuid { get; set; }

        List<KeyValuePair<string, double>> GetObjectiveValues();
        void Reset();
    }
}
