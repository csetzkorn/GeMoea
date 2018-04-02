namespace GeneExpression.Functions
{
    public class Multiplication : FunctionArity2
    {
        public Multiplication() : base("*", true)
        {

        }

        public override object Clone()
        {
            var returnObject = new Multiplication
            {
                FunctionName = FunctionName,
                Middle = Middle
            };

            return returnObject;
        }
    }
}
