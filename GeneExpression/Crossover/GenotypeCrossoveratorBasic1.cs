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
            if((new PhenoTypeTree(genoTypeLeft.GenoTypeNodes)).ToString().Equals((new PhenoTypeTree(genoTypeRight.GenoTypeNodes)).ToString()))
            {
                return;
            }

//            var left1 = new PhenoTypeTree(genoTypeLeft.GenoTypeNodes).ToString();
//            var right1 = new PhenoTypeTree(genoTypeRight.GenoTypeNodes).ToString();

            if (!(UniformRandomGenerator.GetContinousRandomNumber(0, 1.0) <= EaGeneExpressionParameters.CrossoverProbability)) return;

            if (UniformRandomGenerator.GetContinousRandomNumber(0, 1.0) < 0.5)
            {
                var numberOfNodes = PhenoTypeTree.GetNumberOfNodes(genoTypeLeft.GenoTypeNodes);
                var crossOverPosition1 = UniformRandomGenerator.GetIntegerRandomNumber(0, numberOfNodes - 1);

                if (UniformRandomGenerator.GetContinousRandomNumber(0, 1.0) < 0.1)
                {
                    for (var index = crossOverPosition1; index < genoTypeLeft.GenoTypeNodes.Count; index++)
                    {
                        Swap(genoTypeLeft.GenoTypeNodes, genoTypeRight.GenoTypeNodes, index);
                    }
                }
                else
                {
                    for (var index = 0; index <= crossOverPosition1; index++)
                    {
                        Swap(genoTypeLeft.GenoTypeNodes, genoTypeRight.GenoTypeNodes, index);
                    }
                }
            }
            else
            {
                var crossOverPosition1 = UniformRandomGenerator.GetIntegerRandomNumber(1, genoTypeLeft.GenoTypeNodes.Count - 2);
                var crossOverPosition2 = UniformRandomGenerator.GetIntegerRandomNumber(crossOverPosition1, genoTypeLeft.GenoTypeNodes.Count - 2);

                for (var index = crossOverPosition1; index < crossOverPosition2; index++)
                {
                    Swap(genoTypeLeft.GenoTypeNodes, genoTypeRight.GenoTypeNodes, index);
                }
            }

//            var left2 = new PhenoTypeTree(genoTypeLeft.GenoTypeNodes).ToString();
//            var right2 = new PhenoTypeTree(genoTypeRight.GenoTypeNodes).ToString();
        }

        private static void Swap(IList<IGenoTypeNode> listLeft,  IList<IGenoTypeNode> listRight, int index)
        {
            var tmp = listLeft[index];
            listLeft[index] = listRight[index];
            listRight[index] = tmp;
        }
    }
}
