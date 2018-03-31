using System.Collections;
using System.Collections.Generic;
using Data;
using Xunit;

namespace UnitTests.Data
{
    public class DataSetTests
    {
        [Fact]
        public void TestTodoRename()
        {
            const string missingValue = "NA";

            var testData = new string[4, 2] 
            {
                {"1","a"},
                {missingValue,"b"},
                {"1","a"},
                {"2",missingValue}
            };

            var expectedTransformedData = new double[4, 3]
            {
                {1,1,0},
                {1,0,1},
                {1,1,0},
                {2,1,0}
            };

            var continousStrategy = new MeanContinousValueMissingStrategy();
            var nominalStrategy = new MajorityNominalValueMissingStrategy();
            var columns = new List<IColumn>
            {
                new Column("Column1", null, DataType.Continous, "NA", continousStrategy, false)
                ,new Column("Column2", null, DataType.Nominal, "NA", nominalStrategy, false)
            };

            var dataSet = new DataSet(columns, testData);

        }

    }
}
