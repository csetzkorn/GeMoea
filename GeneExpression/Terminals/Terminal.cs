namespace GeneExpression.Terminals
{
    public abstract class Terminal : IGenoTypeNode
    {
        public bool Middle { get; set; }

        public int GetArity()
        {
            return 0;
        }

        public abstract object Clone();
    }
}
