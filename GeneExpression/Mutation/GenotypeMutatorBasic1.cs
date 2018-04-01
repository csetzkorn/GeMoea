using EvolutionaryAlgorithm.Helpers;

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

            var index = UniformRandomGenerator.GetIntegerRandomNumber(0, genoType.GenoTypeNodes.Count - 1);

            if (index < EaGeneExpressionParameters.MaximumNumberOfHeadNodes)
            {
                // create new function
                genoType.GenoTypeNodes[index] = GenoTypeFactory.GetFunctionOrTerminalNode();
            }
            else
            {
                genoType.GenoTypeNodes[index] = GenoTypeFactory.GetTerminalNode();
            }
        }
    }
}
