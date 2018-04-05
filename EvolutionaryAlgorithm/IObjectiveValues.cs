using System;
using System.Collections.Generic;

namespace EvolutionaryAlgorithm
{
    public interface IObjectiveValues : IComparable
    {
        decimal[] Values { get; set; }
        string[] Names { get; set; }
        int DominatedByCount { get; set; }
        int Rank { get; set; }
        decimal CrowdingDistance { get; set; }
        IList<IObjectiveValues> DominatingObjectiveValues { get; set; }
        Guid IndividualGuid { get; set; }
        bool Invalid { get; set; }

        string ExpressionForDebugging { get; set; }

        List<KeyValuePair<string, decimal>> GetObjectiveValues();
        void Reset();
    }
}
