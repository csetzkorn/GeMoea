namespace GeneExpression.Functions
{
    public class Exp : FunctionArity1
    {
        public Exp() : base("Exp")
        {

        }

        public override object Clone()
        {
            var returnObject = new Exp
            {
                FunctionName = FunctionName,
                Middle = Middle
            };

            return returnObject;
        }
    }
}
