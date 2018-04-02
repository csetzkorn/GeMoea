namespace GeneExpression.Functions
{
    public abstract class FunctionArity1 : IFunction
    {
        public string FunctionName { get; set; }
        public bool Middle { get; set; }

        protected FunctionArity1(string functionName)
        {
            FunctionName = functionName;
        }

        public int GetArity()
        {
            return 1;
        }

        public override string ToString()
        {
            return FunctionName;
        }

        public abstract object Clone();
    }
}
