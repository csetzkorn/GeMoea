using System.Collections.Generic;
using EvolutionaryAlgorithm.Helpers;
using GeneExpression;
using GeneExpression.Functions;
using GeneExpression.Terminals;
using Xunit;

namespace UnitTests.PhenoTypeTree
{
    public class GeneToPhenoTypeTests
    {
        [Fact]
        public void CanTranslateTest1()
        {
            var list = new List<IGenoTypeNode>
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

            var phenoTypeTree = new GeneExpression.PhenoTypeTree(list);

            var expresssion = phenoTypeTree.ToString();

            Assert.Equal("SQRT(((a+b)*(c-d)))", expresssion);
        }

        [Fact]
        public void CanTranslateTest2()
        {
            var list = new List<IGenoTypeNode>
            {
                new Multiplication(),
                new FeatureTerminal("b"),
                new Plus(),
                new FeatureTerminal("a"),
                new Minus(),
                new FeatureTerminal("a"),
                new SquareRoot(),
                new FeatureTerminal("a")
            };


            var phenoTypeTree = new GeneExpression.PhenoTypeTree(list);

            var expresssion = phenoTypeTree.ToString();

            Assert.Equal("(b*(a+(a-SQRT(a))))", expresssion);
        }

        [Fact]
        public void CanTranslateTest3()
        {
            var randomGenerator = new UniformRandomGenerator();

            var possibleFunctions = new List<IGenoTypeNode>
            {
                new SquareRoot(),
                new Multiplication(),
                new Division(),
                new Plus(),
                new Minus(),
                new Minimum(),
                new Maximum(),
                new Not(),
                new Exp(),
                new Sinus(),
                new Cosinus()
            };

            var possibleTerminals = new List<IGenoTypeNode>
            {
                new FeatureTerminal("a"),
                new FeatureTerminal("b"),
                new FeatureTerminal("c"),
                new FeatureTerminal("d")
            };

            var eaGeneExpressionParameters = new EaGeneExpressionParameters(20, possibleFunctions, possibleTerminals);
            var parameterTerminalFactory = new ParameterTerminalFactory(eaGeneExpressionParameters, randomGenerator);
            var genoTypeFactory = new GenoTypeFactory(eaGeneExpressionParameters, randomGenerator, parameterTerminalFactory);

            eaGeneExpressionParameters.ParameterTypeInteger = true;
            eaGeneExpressionParameters.ConstantProbability = 0;

            var genoType = genoTypeFactory.GetGenoType();
            var phenoTypeTree = new GeneExpression.PhenoTypeTree(genoType.GenoTypeNodes);

            var expresssion = phenoTypeTree.ToString();
        }
    }
}
