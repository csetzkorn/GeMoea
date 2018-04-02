namespace GeneExpression.Functions
{
    public class Not : FunctionArity1
    {
        public Not() : base("Not")
        {

        }

        public override object Clone()
        {
            var returnObject = new Not
            {
                FunctionName = FunctionName,
                Middle = Middle
            };

            return returnObject;
        }
    }
}
