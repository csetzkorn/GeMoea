using GeneExpression.Functions;
using Xunit;

namespace UnitTests.Cloning
{
    public class CloningTests1
    {
        [Fact]
        public void CanClone()
        {
            
            var squareRoot1 = new SquareRoot();
            var squareRoot2 = squareRoot1.Clone();

            
        }
    }
}
