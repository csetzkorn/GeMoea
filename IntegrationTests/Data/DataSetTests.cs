using System.Collections.Generic;
using Data;
using Xunit;

namespace IntegrationTests.Data
{
    public class DataSetTests
    {
        [Fact]
        public void CanCreateDataSet1()
        {
            var continousStrategy = new MeanContinousValueMissingStrategy();
            var nominalStrategy = new MajorityNominalValueMissingStrategy();

            var columns = new List<IColumn>
            {
                new Column("MSSubClass", null, DataType.Continous, "NA", continousStrategy, false)
                ,new Column("MSZoning", null, DataType.Nominal, "NA", nominalStrategy, false)
                ,new Column("LotFrontage", null, DataType.Continous, "NA", continousStrategy, false)
                ,new Column("FireplaceQu", null, DataType.Nominal, "NA", nominalStrategy, false)
                ,new Column("SalePrice", null, DataType.Continous, "NA", continousStrategy, true)
            };

            var flatFileHelper = new CsvFlatFile(@"D:\Data\Kaggle\HousePrices\train_small.csv");

            var dataSet = new DataSet(columns, flatFileHelper.Data);
        }
    }
}
