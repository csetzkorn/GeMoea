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
                FunctionName = string.Copy(FunctionName),
                Middle = Middle
            };

            return returnObject;
        }
    }
}
