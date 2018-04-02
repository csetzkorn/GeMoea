namespace GeneExpression.Functions
{
    public abstract class FunctionArity2 : IFunction
    {
        public string FunctionName { get; set; }
        public bool Middle { get; set; }

        protected FunctionArity2(string functionName, bool middle)
        {
            FunctionName = functionName;
            Middle = middle;
        }

        public int GetArity()
        {
            return 2;
        }

        public override string ToString()
        {
            return FunctionName;
        }

        public abstract object Clone();
    }
}
