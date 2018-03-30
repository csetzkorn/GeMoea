using System.Collections.Generic;
using EvolutionaryAlgorithm.Helpers;
using GeneExpression;
using GeneExpression.Functions;
using GeneExpression.Terminals;
using Xunit;

namespace UnitTests.GenoType
{
    public class GenoTypeFactoryTests
    {

        [Fact]
        public void CanGenerateGenoType()
        {
            var randomGenerator = new UniformRandomGenerator();

            var possibleFunctions = new List<IGenoTypeNode>
            {
                new SquareRoot(),
                new Multiplication(),
                new Plus(),
                new Minus()
            };

            var possibleTerminals = new List<IGenoTypeNode>
            {
                new FeatureTerminal("a"),
                new FeatureTerminal("b"),
                new FeatureTerminal("c"),
                new FeatureTerminal("d")
            };

            var eaGeneExpressionParameters = new EaGeneExpressionParameters(4, possibleFunctions, possibleTerminals);
            var parameterTerminalFactory = new ParameterTerminalFactory(eaGeneExpressionParameters, randomGenerator);
            var genoTypeFactory = new GenoTypeFactory(eaGeneExpressionParameters, randomGenerator, parameterTerminalFactory);

            var genoType = genoTypeFactory.GetGenoType();

            Assert.NotNull(genoType);
        }
    }
}
