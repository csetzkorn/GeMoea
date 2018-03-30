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
                return Expression.Trim();
            }
            if (Left != null && Right == null)
            {
                return Expression.Trim() + "(" + Left.ToString().Trim() + ")";
            }
            
            if (Middle)
            {
                return "(" + Left.ToString().Trim() + Expression.Trim() + Right.ToString().Trim() + ")";
            }
            return Expression.Trim() + "(" + Left.ToString().Trim() + "," + Right.ToString().Trim() + ")";
        }
    }
}
