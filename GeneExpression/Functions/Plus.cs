
namespace GeneExpression.Functions
{
    public class Plus : FunctionArity2
    {
        public Plus() : base("+", true)
        {
            
        }

        public override object Clone()
        {
            var returnObject = new Plus
            {
                FunctionName = FunctionName,
                Middle = Middle
            };

            return returnObject;
        }
    }
}
