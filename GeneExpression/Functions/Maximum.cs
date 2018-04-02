namespace GeneExpression.Functions
{
    public class Maximum : FunctionArity2
    {
        public Maximum() : base("Max", false)
        {

        }

        public override object Clone()
        {
            var returnObject = new Maximum
            {
                FunctionName = FunctionName,
                Middle = Middle
            };

            return returnObject;
        }
    }
}
