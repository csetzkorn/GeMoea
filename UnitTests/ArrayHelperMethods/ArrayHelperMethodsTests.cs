using System.Collections.Generic;
using Data;
using Xunit;

namespace UnitTests.ArrayHelperMethods
{
    public class ArrayExtensionMethodTests
    {
        [Fact]
        public void CanGetRow()
        {
            var testData = new double[4, 3]
            {
                {1,1,0},
                {1,0,1},
                {1,1,0},
                {2,1,0}
            };
            var expected = new double[] { 2, 1, 0 };

            var actual = testData.GetRow(3);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CanGetColumn()
        {
            var testData = new double[4, 3]
            {
                {1,1,0},
                {1,0,1},
                {1,1,0},
                {2,1,0}
            };
            var expected = new double[] { 1, 0, 1, 1 };

            var actual = testData.GetColumn(1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFrequencyOfValues()
        {
            var testData = new string[] { "a", "a", "NA", "b" };
            var expected = new Dictionary<string, int>
            {
                {"a", 2},
                {"b", 1}
            };

            var actual = testData.GetFrequencyOfValues("NA");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetUniqueValues()
        {
            var testData = new string[] { "a", "a", "NA", "b" };
            var expected = new List<string> {"a", "b"};

            var actual = testData.GetUniqueValues("NA");

            Assert.Equal(expected, actual);
        }
    }
}
