using System;

namespace EvolutionaryAlgorithm
{
    public interface IIndividual : ICloneable
    {
        Guid Guid { get; set; }
    }
}
