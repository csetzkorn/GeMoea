using EvolutionaryAlgorithm.Helpers;
using GeneExpression.Terminals;

namespace GeneExpression.Mutation
{
    public class GenoTypeMutatorBasic1 : IGenoTypeMutator
    {
        private IUniformRandomGenerator UniformRandomGenerator { get; }
        private IEaGeneExpressionParameters EaGeneExpressionParameters { get; }
        private IGenoTypeFactory GenoTypeFactory { get; }

        public GenoTypeMutatorBasic1(IUniformRandomGenerator uniformRandomGenerator, IEaGeneExpressionParameters eaGeneExpressionParameters, IGenoTypeFactory genoTypeFactory)
        {
            UniformRandomGenerator = uniformRandomGenerator;
            EaGeneExpressionParameters = eaGeneExpressionParameters;
            GenoTypeFactory = genoTypeFactory;
        }

        public void PerformMutation(ref IGenoType genoType)
        {
            if (!(UniformRandomGenerator.GetContinousRandomNumber(0, 1.0) <= EaGeneExpressionParameters.MutationProbability)) return;

            var randomNumber = UniformRandomGenerator.GetContinousRandomNumber(0, 1.0);

            if (randomNumber <= 0.3)
            {
                genoType = GenoTypeFactory.GetGenoType();
            }
            else
            {
                //var index = UniformRandomGenerator.GetIntegerRandomNumber(0, genoType.GenoTypeNodes.Count - 1);
                var numberOfNodes = PhenoTypeTree.GetNumberOfNodes(genoType.GenoTypeNodes);
                var index = UniformRandomGenerator.GetIntegerRandomNumber(0, numberOfNodes - 1);

                if (randomNumber <= 0.6)
                {
                    var node = genoType.GenoTypeNodes[index];
                    genoType.GenoTypeNodes.RemoveAt(index);
                    index = UniformRandomGenerator.GetIntegerRandomNumber(0, genoType.GenoTypeNodes.Count - 1);
                    genoType.GenoTypeNodes.Insert(index, node);
                    
                }
                else
                {
                    if (genoType.GenoTypeNodes[index].GetType() == typeof(ParameterTerminal))
                    {
                        ((ParameterTerminal)genoType.GenoTypeNodes[index]).Value =
                            ((ParameterTerminal)genoType.GenoTypeNodes[index]).Value +
                            UniformRandomGenerator.GetContinousRandomNumber(-3.0, 3.0);
                    }
                    else
                    {
                        if (index < EaGeneExpressionParameters.MaximumNumberOfHeadNodes)
                        {
                            genoType.GenoTypeNodes[index] = GenoTypeFactory.GetFunctionOrTerminalNode();
                        }
                        else
                        {
                            genoType.GenoTypeNodes[index] = GenoTypeFactory.GetTerminalNode();
                        }
                    }
                }
            }
        }
    }
}
