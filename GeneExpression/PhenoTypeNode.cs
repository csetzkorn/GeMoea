namespace GeneExpression
{
    public class PhenoTypeNode : IPhenoTypeNode
    {
        public string Expression { get; set; }
        public IPhenoTypeNode Left { get; set; }
        public IPhenoTypeNode Right { get; set; }
        public bool Middle { get; set; }

        public override string ToString()
        {
            if (Left == null && Right == null)
            {
                return Expression;
            }
            if (Left != null && Right == null)
            {
                return Expression + "(" + Left.ToString() + ")";
            }
            
            if (Middle)
            {
                return "(" + Left.ToString() + Expression + Right.ToString() + ")";
            }
            return Expression + "(" + Left.ToString() + "," + Right.ToString() + ")";
        }
    }
}
