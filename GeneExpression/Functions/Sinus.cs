namespace GeneExpression.Functions
{
    public class Sinus : FunctionArity1
    {
        public Sinus() : base("Sin")
        {

        }

        public override object Clone()
        {
            var returnObject = new Sinus
            {
                FunctionName = FunctionName,
                Middle = Middle
            };

            return returnObject;
        }

    }
}
