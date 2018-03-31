using Data;
using Xunit;

namespace UnitTests.Data
{
    public class MissingValueStrategyTests
    {
        [Fact]
        public void MeanContinousValueMissingStrategyWorks()
        {
            const string missingValueIndicator = "NA";
            var testData = new string[4];
            testData[0] = "3.5";
            testData[1] = "4.0";
            testData[2] = missingValueIndicator;
            testData[3] = "13.123";
            var expected = (double.Parse(testData[0]) + double.Parse(testData[1]) + double.Parse(testData[3])) / 3.0;
            var meanContinousValueMissingStrategy = new MeanContinousValueMissingStrategy();

            var actual = meanContinousValueMissingStrategy.GetColumn(testData, "NA")[2];

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MajorityNominalValueMissingStrategyWorks()
        {
            const string missingValueIndicator = "NA";
        }
    }
}
