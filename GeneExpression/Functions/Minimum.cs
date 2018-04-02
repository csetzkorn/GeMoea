namespace GeneExpression.Functions
{
    public class Minimum : FunctionArity2
    {
        public Minimum() : base("Min", false)
        {

        }

        public override object Clone()
        {
            var returnObject = new Minimum
            {
                FunctionName = FunctionName,
                Middle = Middle
            };

            return returnObject;
        }
    }
}
