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

        public EaGeneExpressionParameters(
            int maximumNumberOfHeadNodes
            ,List<IGenoTypeNode> possibleFunctions
            ,List<IGenoTypeNode> possibleTerminals

            ,double functionProbability = 0.8
            ,double constantProbability = 0.5
            ,double parametertProbability = 0.5

            , double parameterMinimum = -100
            ,double parameterMaximum = 100
            ,bool parameterType = false
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
        }
    }
}
