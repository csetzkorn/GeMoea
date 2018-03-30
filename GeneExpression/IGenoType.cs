using System.Collections.Generic;

namespace GeneExpression
{
    public interface IGenoType
    {
        List<IGenoTypeNode> GenoTypeNodes { get; set; }
    }
}
