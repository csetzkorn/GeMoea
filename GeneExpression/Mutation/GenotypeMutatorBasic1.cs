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

            var before = genoType.ToString();

            var index = UniformRandomGenerator.GetIntegerRandomNumber(0, genoType.GenoTypeNodes.Count - 1);

            if (genoType.GenoTypeNodes[index].GetType() == typeof(ParameterTerminal))
            {
                // TODO improve
                ((ParameterTerminal) genoType.GenoTypeNodes[index]).Value = ((ParameterTerminal)genoType.GenoTypeNodes[index]).Value +
                    UniformRandomGenerator.GetContinousRandomNumber(-10, 10);
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
