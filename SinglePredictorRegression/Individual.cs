using System;
using EvolutionaryAlgorithm;
using GeneExpression;

namespace SinglePredictorRegression
{
    public class Individual : IIndividual
    {
        public Guid Guid { get; set; }
        public IGenoType GenoType;

        public Individual(IGenoType genoType)
        {
            Guid = Guid.NewGuid();
            GenoType = genoType;
        }

        public object Clone()
        {
            return new Individual((IGenoType) GenoType.Clone());
        }
    }
}
