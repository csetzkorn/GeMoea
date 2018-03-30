using EvolutionaryAlgorithm.Helpers;

namespace GeneExpression.Terminals
{
    public class ParameterTerminalFactory : IParameterTerminalFactory
    {
        private IUniformRandomGenerator UniformRandomGenerator { get; }
        private IEaGeneExpressionParameters EaGeneExpressionParameters { get; }
        private int PossibleConstantsCountMinusOne { get; }

        public ParameterTerminalFactory(IEaGeneExpressionParameters eAGeneExpressionParameters, IUniformRandomGenerator uniformRandomGenerator)
        {
            UniformRandomGenerator = uniformRandomGenerator;
            EaGeneExpressionParameters = eAGeneExpressionParameters;
            PossibleConstantsCountMinusOne = EaGeneExpressionParameters.PossibleConstants.Count - 1;
        }

        public IGenoTypeNode GetParameterTerminal()
        {
            if (UniformRandomGenerator.GetContinousRandomNumber(0, 1.0) <=
                EaGeneExpressionParameters.ConstantProbability)
            {
                return new ParameterTerminal(EaGeneExpressionParameters.PossibleConstants[UniformRandomGenerator.GetIntegerRandomNumber(0, PossibleConstantsCountMinusOne)].Value);
            }

            return EaGeneExpressionParameters.ParameterTypeInteger 
                ? new ParameterTerminal(UniformRandomGenerator.GetIntegerRandomNumber((int) EaGeneExpressionParameters.ParameterMinimum, (int) EaGeneExpressionParameters.ParameterMaximum)) 
                : new ParameterTerminal(UniformRandomGenerator.GetContinousRandomNumber(EaGeneExpressionParameters.ParameterMinimum, EaGeneExpressionParameters.ParameterMaximum));
        }
    }
}
