namespace GeneExpression.Functions
{
    public class Multiplication : FunctionArity2
    {
        public Multiplication() : base("*", true)
        {

        }

        public override object Clone()
        {
            var returnObject = new Multiplication()
            {
                FunctionName = string.Copy(FunctionName),
                Middle = Middle
            };

            return returnObject;
        }
    }
}
