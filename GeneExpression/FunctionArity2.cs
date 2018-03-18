namespace GeneExpression
{
    public abstract class FunctionArity2 : IFunction
    {
        public string FunctionName { get; set; }

        protected FunctionArity2(string functionName)
        {
            FunctionName = functionName;
        }

        public int GetArity()
        {
            return 2;
        }

        public override string ToString()
        {
            return FunctionName;
        }
    }
}
