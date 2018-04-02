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
            var continousMeanStrategy = new MeanContinousValueMissingStrategy();
            var nominalMajorityStrategy = new MajorityNominalValueMissingStrategy();

            var columns = new List<IColumn>
            {
                new Column("MSSubClass", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("MSZoning", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("LotFrontage", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("FireplaceQu", null, DataType.Nominal, "xxx", nominalMajorityStrategy)
            };

            var flatFileHelper = new CsvFlatFile(@"D:\Data\Kaggle\HousePrices\SmallCombined.csv");

            var dataSet = new DataSet(columns, flatFileHelper.Data);

            Assert.Equal(2919, dataSet.MappedData.GetLength(0));
            Assert.Equal(28, dataSet.MappedData.GetLength(1));
        }

        [Fact]
        public void LargeTest()
        {
            //=IF(EXACT(B1, "Nominal"),CONCAT(",new Column('", A1, "', null, DataType.Nominal, 'NA', nominalMajorityStrategy, false)"),CONCAT(",new Column('", A1, "', null, DataType.Continous, 'NA', continousMeanStrategy, false)"))

            var continousMeanStrategy = new MeanContinousValueMissingStrategy();
            var nominalMajorityStrategy = new MajorityNominalValueMissingStrategy();

            var columns = new List<IColumn>
            {
                new Column("Id", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("MSSubClass", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("MSZoning", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("LotFrontage", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("LotArea", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("Street", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("Alley", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("LotShape", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("LandContour", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("Utilities", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("LotConfig", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("LandSlope", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("Neighborhood", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("Condition1", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("Condition2", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("BldgType", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("HouseStyle", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("OverallQual", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("OverallCond", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("YearBuilt", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("YearRemodAdd", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("RoofStyle", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("RoofMatl", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("Exterior1st", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("Exterior2nd", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("MasVnrType", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("MasVnrArea", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("ExterQual", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("ExterCond", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("Foundation", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("BsmtQual", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("BsmtCond", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("BsmtExposure", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("BsmtFinType1", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("BsmtFinSF1", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("BsmtFinType2", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("BsmtFinSF2", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("BsmtUnfSF", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("TotalBsmtSF", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("Heating", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("HeatingQC", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("CentralAir", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("Electrical", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("1stFlrSF", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("2ndFlrSF", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("LowQualFinSF", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("GrLivArea", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("BsmtFullBath", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("BsmtHalfBath", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("FullBath", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("HalfBath", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("BedroomAbvGr", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("KitchenAbvGr", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("KitchenQual", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("TotRmsAbvGrd", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("Functional", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("Fireplaces", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("FireplaceQu", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("GarageType", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("GarageYrBlt", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("GarageFinish", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("GarageCars", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("GarageArea", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("GarageQual", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("GarageCond", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("PavedDrive", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("WoodDeckSF", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("OpenPorchSF", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("EnclosedPorch", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("3SsnPorch", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("ScreenPorch", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("PoolArea", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("PoolQC", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("Fence", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("MiscFeature", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("MiscVal", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("MoSold", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("YrSold", null, DataType.Continous, "NA", continousMeanStrategy)
                ,new Column("SaleType", null, DataType.Nominal, "NA", nominalMajorityStrategy)
                ,new Column("SaleCondition", null, DataType.Nominal, "NA", nominalMajorityStrategy)
            };

            var flatFileHelper = new CsvFlatFile(@"D:\Data\Kaggle\HousePrices\Combined.csv");

            var dataSet = new DataSet(columns, flatFileHelper.Data);

            Assert.Equal(2919, dataSet.MappedData.GetLength(0));
            Assert.Equal(304, dataSet.MappedData.GetLength(1));
        }
    }
}
