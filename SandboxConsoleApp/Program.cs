using System;
using NCalc;

namespace SandboxConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] data = new double[100, 10];
            Random random = new Random();

            var numberOfRows = data.GetLength(0);
            var numberOfColumns = data.GetLength(1);

            for (int row = 0; row < numberOfRows; row++)
            {
                for (int col = 0; col < numberOfColumns; col++)
                {
                    data[row, col] = random.Next();
                }
            }

            // in the case of 10 columns the expression looks like: [x0] + [x1] + [x2] + [x3] + [x4] + [x5] + [x6] + [x7] + [x8] + [x9]
            var stringExpression = "";
            for (int col = 0; col < numberOfColumns - 1; col++)
            {
                stringExpression += string.Format("[x{0}] + ", col);
            }
            stringExpression += string.Format("[x{0}]", (numberOfColumns - 1));

            var exp = new Expression(stringExpression);
            var total = 0.0;

            for (int row = 0; row < numberOfRows; row++)
            {
                for (int col = 0; col < numberOfColumns; col++)
                {
                    exp.Parameters[string.Format("x{0}", col)] = data[row, col];
                }

                if (row % 100000 == 0)
                {
                    Console.WriteLine(row);
                }

                if (!exp.HasErrors())
                {
                    total += (double)exp.Evaluate();
                }
            }
        } 
    }
}
