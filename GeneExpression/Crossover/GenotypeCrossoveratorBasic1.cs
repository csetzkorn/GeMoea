using System.Collections.Generic;
using EvolutionaryAlgorithm.Helpers;

namespace GeneExpression.Crossover
{
    public class GenoTypeCrossoveratorBasic1 : IGenoTypeCrossoverator
    {
        private IUniformRandomGenerator UniformRandomGenerator { get; }
        private IEaGeneExpressionParameters EaGeneExpressionParameters { get; }

        public GenoTypeCrossoveratorBasic1(IUniformRandomGenerator uniformRandomGenerator, IEaGeneExpressionParameters eaGeneExpressionParameters)
        {
            UniformRandomGenerator = uniformRandomGenerator;
            EaGeneExpressionParameters = eaGeneExpressionParameters;
        }

        public void PerformCrossover(ref IGenoType genoTypeLeft, ref IGenoType genoTypeRight)
        {
            if (!(UniformRandomGenerator.GetContinousRandomNumber(0, 1.0) <=
                  EaGeneExpressionParameters.CrossoverProbability)) return;
            var crossOverPosition =
                UniformRandomGenerator.GetIntegerRandomNumber(1, genoTypeLeft.GenoTypeNodes.Count - 2);

            for (var index = crossOverPosition; index < genoTypeLeft.GenoTypeNodes.Count; index++)
            {
                Swap(genoTypeLeft.GenoTypeNodes, genoTypeRight.GenoTypeNodes, index);
            }
        }

        private static void Swap(IList<IGenoTypeNode> listLeft,  IList<IGenoTypeNode> listRight, int index)
        {
            var tmp = listLeft[index];
            listLeft[index] = listRight[index];
            listRight[index] = tmp;
        }
    }
}
