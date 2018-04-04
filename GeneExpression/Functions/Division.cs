namespace GeneExpression.Functions
{
    public class Division : FunctionArity2
    {
        public Division() : base("/", true)
        {

        }

        public override object Clone()
        {
            var returnObject = new Division
            {
                FunctionName = string.Copy(FunctionName),
                Middle = Middle
            };

            return returnObject;
        }
    }
}
