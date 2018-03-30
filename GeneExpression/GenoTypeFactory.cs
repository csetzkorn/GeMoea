using EvolutionaryAlgorithm.Helpers;
using GeneExpression.Terminals;

namespace GeneExpression
{
    public class GenoTypeFactory
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
                if (UniformRandomGenerator.GetContinousRandomNumber(0, 1.0) < EaGeneExpressionParameters.FunctionProbability)
                {
                    var node = EaGeneExpressionParameters.PossibleFunctions[
                        UniformRandomGenerator.GetIntegerRandomNumber(0, NumberOfPossibleFunctions)];

                    genoType.GenoTypeNodes.Add(node);
                }
                else
                {
                    genoType.GenoTypeNodes.Add(GetTerminalNode());
                }
            }

            for (var c = 0; c < EaGeneExpressionParameters.MaximumNumberOTailNodes; c++)
            {
                genoType.GenoTypeNodes.Add(GetTerminalNode());
            }

            return genoType;
        }

        private IGenoTypeNode GetTerminalNode()
        {
            // TODO generation of parameternode via factory
            //ParameterTerminalFactory

            if (UniformRandomGenerator.GetContinousRandomNumber(0, 1.0) <
                EaGeneExpressionParameters.ParameterProbability)
            {
                return ParameterTerminalFactory.GetParameterTerminal();
            }

            return EaGeneExpressionParameters.PossibleTerminals[
                UniformRandomGenerator.GetIntegerRandomNumber(0, NumberOfPossibleTerminals)];
        }
    }
}
