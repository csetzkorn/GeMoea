using System;
using System.Collections.Generic;

namespace GeneExpression
{
    public interface IGenoType : ICloneable
    {
        List<IGenoTypeNode> GenoTypeNodes { get; set; }
    }
}
