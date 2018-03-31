using Data;
using Xunit;

namespace IntegrationTests.Data
{
    public class FlatFileTests
    {
        [Fact]
        public void CanReadFlatFileCommaSeparated()
        {
            var flatFileHelper = new CsvFlatFile(@"D:\Data\Kaggle\HousePrices\train_small.csv");

            var numberOfRows = flatFileHelper.Data.GetLength(0);
            var numberOfColumns = flatFileHelper.Data.GetLength(1);

            Assert.Equal(1460, numberOfRows);
            Assert.Equal(5, numberOfColumns);           
        }
    }
}
