﻿namespace GeneExpression.Functions
{
    public class Minus : FunctionArity2
    {
        public Minus() : base("-", true)
        {

        }

        public override object Clone()
        {
            var returnObject = new Minus
            {
                FunctionName = string.Copy(FunctionName),
                Middle = Middle
            };

            return returnObject;
        }
    }
}
