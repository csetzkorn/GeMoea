namespace GeneExpression.Functions
{
    public class SquareRoot : FunctionArity1
    {
        public SquareRoot() : base("Sqrt")
        {

        }

        public override object Clone()
        {
            var returnObject = new SquareRoot
            {
                FunctionName = FunctionName,
                Middle = Middle
            };

            return returnObject;
        }
    }
}
