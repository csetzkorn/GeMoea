using Data;
using Xunit;

namespace IntegrationTests.Data
{
    public class ContinousTargetTests
    {
        [Fact]
        public void Test()
        {
            const int expected = 1460;
            var continousTarget = new ContinousTarget(@"D:\Data\Kaggle\HousePrices\Target.txt");
            var actual = continousTarget.Values.Count;

            Assert.Equal(expected, actual);
        }
    }
}
