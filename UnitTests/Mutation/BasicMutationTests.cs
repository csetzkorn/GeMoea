using System.Collections.Generic;
using EvolutionaryAlgorithm.Helpers;
using GeneExpression;
using GeneExpression.Functions;
using GeneExpression.Mutation;
using GeneExpression.Terminals;
using Xunit;

namespace UnitTests.Mutation
{
    public class BasicMutationTests
    {
        [Fact]
        public void CanMutate()
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

            var eaGeneExpressionParameters =
                new EaGeneExpressionParameters(4, possibleFunctions, possibleTerminals) {MutationProbability = 1.0};
            var parameterTerminalFactory = new ParameterTerminalFactory(eaGeneExpressionParameters, randomGenerator);
            var genoTypeFactory = new GenoTypeFactory(eaGeneExpressionParameters, randomGenerator, parameterTerminalFactory);

            var mutator = new GenoTypeMutatorBasic1(randomGenerator, eaGeneExpressionParameters, genoTypeFactory);

            var genoType = genoTypeFactory.GetGenoType();
            //mutator.PerformMutation();

            var phenoTypeTreeBefore = new GeneExpression.PhenoTypeTree(genoType.GenoTypeNodes);
            mutator.PerformMutation(ref genoType);
            var phenoTypeTreeAfter = new GeneExpression.PhenoTypeTree(genoType.GenoTypeNodes);

            Assert.NotEqual(phenoTypeTreeBefore, phenoTypeTreeAfter);
        }
    }
}
