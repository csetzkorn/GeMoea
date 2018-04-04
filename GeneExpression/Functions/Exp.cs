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
                FunctionName = string.Copy(FunctionName),
                Middle = Middle
            };

            return returnObject;
        }
    }
}
