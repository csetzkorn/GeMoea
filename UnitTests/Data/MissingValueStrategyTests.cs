using Data;
using Xunit;

namespace UnitTests.Data
{
    public class MissingValueStrategyTests
    {
        [Fact]
        public void MeanContinousValueMissingStrategyWorks()
        {
            const string missingValue = "NA";
            var testData = new string[4];
            testData[0] = "3.5";
            testData[1] = "4.0";
            testData[2] = missingValue;
            testData[3] = "13.123";
            // ReSharper disable once SpecifyACultureInStringConversionExplicitly
            var expected = ((double.Parse(testData[0]) + double.Parse(testData[1]) + double.Parse(testData[3])) / 3.0).ToString();
            var meanContinousValueMissingStrategy = new MeanContinousValueMissingStrategy();

            var actual = meanContinousValueMissingStrategy.GetColumn(testData, "NA")[2];

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MajorityNominalValueMissingStrategyWorks()
        {
            const string missingValue = "NA";
            var testData = new string[7];
            testData[0] = "a";
            testData[1] = "a";
            testData[2] = missingValue;
            testData[3] = "b";
            testData[4] = "b";
            testData[5] = "c";
            testData[6] = "a";

            const string expected = "a";

            var majorityNominalValueMissingStrategy = new MajorityNominalValueMissingStrategy();

            var actual = majorityNominalValueMissingStrategy.GetColumn(testData, "NA")[2];

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MajorityNominalValueMissingStrategyWithTieWorks()
        {
            const string missingValueIndicator = "NA";
            var testData = new string[8];
            testData[0] = "a";
            testData[1] = "a";
            testData[2] = missingValueIndicator;
            testData[3] = "b";
            testData[4] = "b";
            testData[5] = "c";
            testData[6] = "a";
            testData[7] = "b";

            const string expected = "a";

            var majorityNominalValueMissingStrategy = new MajorityNominalValueMissingStrategy();

            var actual = majorityNominalValueMissingStrategy.GetColumn(testData, "NA")[2];

            Assert.Equal(expected, actual);
        }
    }
}
