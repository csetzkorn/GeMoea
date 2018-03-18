using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneExpression
{
    public abstract class FunctionArity1 : IFunction
    {
        public string FunctionName { get; set; }

        protected FunctionArity1(string functionName)
        {
            FunctionName = functionName;
        }

        public int GetArity()
        {
            return 2;
        }

        public override string ToString()
        {
            return FunctionName;
        }
    }
}
