using System.Collections.Generic;
using GeneExpression.Terminals;

namespace GeneExpression
{
    public interface IEaGeneExpressionParameters
    {
        int MaximumNumberOfHeadNodes { get; set; }
        int MaximumNumberOTailNodes { get; set; }
        List<IGenoTypeNode> PossibleFunctions { get; set; }
        List<IGenoTypeNode> PossibleTerminals { get; set; }
        List<Constant> PossibleConstants { get; set; }

        int NumberOfPossibleFunctions { get; set; }
        int NumberOfPossibleTerminals { get; set; }

        double FunctionProbability { get; set; }
        double ConstantProbability { get; set; }
        double ParameterProbability { get; set; }

        double ParameterMinimum { get; set; }
        double ParameterMaximum { get; set; }
        bool ParameterTypeInteger { get; set; } // if true only integers allowed

        double CrossoverProbability { get; set; }
        double MutationProbability { get; set; }
    }
}
