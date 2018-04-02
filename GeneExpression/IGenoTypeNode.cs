using System;

namespace GeneExpression
{
    public interface IGenoTypeNode : ICloneable
    {
        int GetArity();
        bool Middle { get; set; }
    }
}
