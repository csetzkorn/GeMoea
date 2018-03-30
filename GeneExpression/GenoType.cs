using System.Collections.Generic;

namespace GeneExpression
{
    public class GenoType : IGenoType
    {
        public GenoType()
        {
            GenoTypeNodes = new List<IGenoTypeNode>();
        }

        public List<IGenoTypeNode> GenoTypeNodes { get; set; }
    }
}
