using System.Collections.Generic;
using EvolutionaryAlgorithm.Helpers;
using GeneExpression;
using GeneExpression.Crossover;
using GeneExpression.Functions;
using GeneExpression.Terminals;
using Xunit;

namespace UnitTests.Crossover
{
    public class CrossoverTests
    {
        [Fact]
        public void Test()
        {
            var listLeft = new List<IGenoTypeNode>
            {
                new SquareRoot(),
                new Multiplication(),
                new Plus(),
                new Minus(),
                new FeatureTerminal("a"),
                new FeatureTerminal("b"),
                new FeatureTerminal("c"),
                new FeatureTerminal("d")
            };

            var listRight = new List<IGenoTypeNode>
            {
                new Sinus(),
                new Multiplication(),
                new Division(),
                new Minus(),
                new FeatureTerminal("a1"),
                new FeatureTerminal("b1"),
                new FeatureTerminal("c1"),
                new FeatureTerminal("d1")
            };

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
            IGenoType left = new GeneExpression.GenoType {GenoTypeNodes = listLeft};
            IGenoType right = new GeneExpression.GenoType {GenoTypeNodes = listRight};
            var randomGenerator = new UniformRandomGenerator();
            var genotypeCrossoverator1 = new GenoTypeCrossoveratorBasic1(randomGenerator, eaGeneExpressionParameters);

            var phenoTypeTreeLeft1 = new GeneExpression.PhenoTypeTree(left.GenoTypeNodes);
            var phenoTypeTreeRight1 = new GeneExpression.PhenoTypeTree(right.GenoTypeNodes);

            genotypeCrossoverator1.PerformCrossover(ref left, ref right);

            var phenoTypeTreeLeft2 = new GeneExpression.PhenoTypeTree(left.GenoTypeNodes);
            var phenoTypeTreeRight2 = new GeneExpression.PhenoTypeTree(right.GenoTypeNodes);

            Assert.NotEqual(phenoTypeTreeLeft1, phenoTypeTreeLeft2);
            Assert.NotEqual(phenoTypeTreeRight1, phenoTypeTreeRight2);
        }

    }
}
