namespace GeneExpression.Functions
{
    public class Cosinus : FunctionArity1
    {
        public Cosinus() : base("Cos")
        {
            
        }

        public override object Clone()
        {
            var returnObject = new Cosinus
            {
                FunctionName = string.Copy(FunctionName),
                Middle = Middle
            };

            return returnObject;
        }
    }
}
