using System.Collections.Generic;
using Data;
using Xunit;

namespace UnitTests.Data
{
    public class DataSetTests
    {
        [Fact]
        public void DataSetCanProduceMappedData1()
        {
            const string missingValue = "NA";

            var testData = new string[4, 3] 
            {
                {"1","a","1"},
                {missingValue,"b","1"},
                {"1","a",missingValue},
                {"2",missingValue,"0"}
            };

            var expected = new double[4, 4]
            {
                {1,1,0,1},
                {1.33333333333333,0,1,1},
                {1,1,0,1},
                {2,1,0,0}
            };

            var continousStrategy = new MeanContinousValueMissingStrategy();
            var nominalStrategy = new MajorityNominalValueMissingStrategy();
            var columns = new List<IColumn>
            {
                new Column("Column1", null, DataType.Continous, "NA", continousStrategy, false)
                ,new Column("Column2", null, DataType.Nominal, "NA", nominalStrategy, false)
                ,new Column("Column2", null, DataType.Boolean, "NA", nominalStrategy, false)
            };

            var dataSet = new DataSet(columns, testData);

            var actual = dataSet.MappedData;

            Assert.Equal(expected, actual);
        }

    }
}
