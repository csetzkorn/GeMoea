using EvolutionaryAlgorithm.Helpers;
using GeneExpression.Terminals;

namespace GeneExpression
{
    public class GenoTypeFactory : IGenoTypeFactory
    {
        private IEaGeneExpressionParameters EaGeneExpressionParameters { get; }
        private IUniformRandomGenerator UniformRandomGenerator { get; }
        private IParameterTerminalFactory ParameterTerminalFactory { get; }
        private int NumberOfPossibleFunctions { get; }
        private int NumberOfPossibleTerminals { get; }

        public GenoTypeFactory(IEaGeneExpressionParameters eaGeneExpressionParameters, IUniformRandomGenerator uniformRandomGenerator, IParameterTerminalFactory parameterTerminalFactory)
        {
            EaGeneExpressionParameters = eaGeneExpressionParameters;
            UniformRandomGenerator = uniformRandomGenerator;
            ParameterTerminalFactory = parameterTerminalFactory;
            NumberOfPossibleFunctions = EaGeneExpressionParameters.NumberOfPossibleFunctions - 1;
            NumberOfPossibleTerminals = EaGeneExpressionParameters.NumberOfPossibleTerminals - 1;
        }

        public IGenoType GetGenoType()
        {
            var genoType = new GenoType();

            for (var c = 0; c < EaGeneExpressionParameters.MaximumNumberOfHeadNodes; c++)
            {
                genoType.GenoTypeNodes.Add(GetFunctionOrTerminalNode());
            }

            for (var c = 0; c < EaGeneExpressionParameters.MaximumNumberOTailNodes; c++)
            {
                genoType.GenoTypeNodes.Add(GetTerminalNode());
            }

            return genoType;
        }

        public IGenoTypeNode GetFunctionOrTerminalNode()
        {
            return UniformRandomGenerator.GetContinousRandomNumber(0, 1.0) <
                   EaGeneExpressionParameters.FunctionProbability ? GetFunctionNode() : GetTerminalNode();
        }

        public IGenoTypeNode GetTerminalNode()
        {
            if (UniformRandomGenerator.GetContinousRandomNumber(0, 1.0) <
                EaGeneExpressionParameters.ParameterProbability)
            {
                return ParameterTerminalFactory.GetParameterTerminal();
            }

            return EaGeneExpressionParameters.PossibleTerminals[
                UniformRandomGenerator.GetIntegerRandomNumber(0, NumberOfPossibleTerminals)];
        }

        public IGenoTypeNode GetFunctionNode()
        {
            return EaGeneExpressionParameters.PossibleFunctions[
                UniformRandomGenerator.GetIntegerRandomNumber(0, NumberOfPossibleFunctions)];
        }
    }
}
