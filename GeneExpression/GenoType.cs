using System.Collections.Generic;

namespace GeneExpression
{
    public class GenoType : IGenoType
    {
        public List<IGenoTypeNode> GenoTypeNodes { get; set; }

        public GenoType()
        {
            GenoTypeNodes = new List<IGenoTypeNode>();
        }

        public object Clone()
        {
            var list = new List<IGenoTypeNode>();
            foreach (var genoTypeNode in GenoTypeNodes)
            {
                list.Add((IGenoTypeNode) genoTypeNode.Clone());
            }

            return new GenoType { GenoTypeNodes = list };
        }
    }
}
