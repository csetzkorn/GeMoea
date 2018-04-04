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
                FunctionName = string.Copy(FunctionName),
                Middle = Middle
            };

            return returnObject;
        }
    }
}
