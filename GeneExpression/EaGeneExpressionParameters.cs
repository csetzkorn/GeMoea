using System;
using System.Collections.Generic;
using System.Linq;
using GeneExpression.Terminals;

namespace GeneExpression
{
    public class EaGeneExpressionParameters : IEaGeneExpressionParameters
    {
        public int MaximumNumberOfHeadNodes { get; set; }
        public int MaximumNumberOTailNodes { get; set; }
        public List<IGenoTypeNode> PossibleFunctions { get; set; }
        public List<IGenoTypeNode> PossibleTerminals { get; set; }
        public List<Constant> PossibleConstants { get; set; }
        public int NumberOfPossibleFunctions { get; set; }
        public int NumberOfPossibleTerminals { get; set; }
        public double FunctionProbability { get; set; }
        public double ConstantProbability { get; set; }
        public double ParameterProbability { get; set; }
        public double ParameterMinimum { get; set; }
        public double ParameterMaximum { get; set; }
        public bool ParameterTypeInteger { get; set; }
        public double CrossoverProbability { get; set; }
        public double MutationProbability { get; set; }
        public int PopulationSize { get; set; }
        public int NumberOfGeneration { get; set; }
        public int TournamentSize { get; set; }

        public EaGeneExpressionParameters(
            int maximumNumberOfHeadNodes
            ,List<IGenoTypeNode> possibleFunctions
            ,List<IGenoTypeNode> possibleTerminals

            ,double functionProbability = 0.8
            ,double constantProbability = 0.5
            ,double parametertProbability = 0.5

            ,double parameterMinimum = -10
            ,double parameterMaximum = 10
            ,bool parameterType = false

            ,double crossoverProbability = 0.6
            ,double mutationProbability = 0.01

            ,int populationSize = 100
            ,int numberOfGeneration = 100
            ,int tournamentSize = 2
        )
        {
            MaximumNumberOfHeadNodes = maximumNumberOfHeadNodes;
            var maximumArity = possibleFunctions.Select(function => function.GetArity()).Concat(new[] {0}).Max();
            MaximumNumberOTailNodes = MaximumNumberOfHeadNodes * (maximumArity - 1) + 1;

            PossibleFunctions = possibleFunctions;
            PossibleTerminals = possibleTerminals;

            NumberOfPossibleFunctions = possibleFunctions.Count;
            NumberOfPossibleTerminals = possibleTerminals.Count;

            FunctionProbability = functionProbability;
            ConstantProbability = constantProbability;
            ParameterProbability = parametertProbability;

            ParameterMinimum = parameterMinimum;
            ParameterMaximum = parameterMaximum;
            ParameterTypeInteger = parameterType;

            CrossoverProbability = crossoverProbability;
            MutationProbability = mutationProbability;

            // http://www.ebyte.it/library/educards/constants/MathConstants.html
            PossibleConstants = new List<Constant>
            {
                new Constant() {Name = "Pi", Value = Math.PI},
                new Constant() {Name = "E", Value = Math.E},
                new Constant() {Name = "EulerMascheroniConstant", Value = 0.577215664901532860606512},
                new Constant() {Name = "PythagorasConstant", Value = 1.414213562373095048801688},
                new Constant() {Name = "GoldenRatio", Value = 1.618033988749894848204586},
                new Constant() {Name = "InverseGoldenRatio", Value = 0.618033988749894848204586},
                new Constant() {Name = "SilverRatio", Value = 2.414213562373095048801688},
                new Constant() {Name = "PlasticNumber", Value = 1.324717957244746025960908},                
            };

            PopulationSize = populationSize;
            NumberOfGeneration = numberOfGeneration;
            TournamentSize = tournamentSize;
        }
    }
}
