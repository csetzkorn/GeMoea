using Data;
using Xunit;

namespace IntegrationTests.Data
{
    public class FlatFileHelperTests
    {
        [Fact]
        public void CanReadFlatFileCommaSeparated()
        {
            var flatFileHelper = new FlatFileHelper();

            var stringArray = flatFileHelper.Import(@"D:\Data\Kaggle\HousePrices\train_small.csv", ',', true, true);

            var numberOfRows = stringArray.GetLength(0);
            var numberOfColumns = stringArray.GetLength(1);

            Assert.Equal(1460, numberOfRows);
            Assert.Equal(5, numberOfColumns);           
        }
    }
}
