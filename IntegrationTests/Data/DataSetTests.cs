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
                new Column("MSSubClass", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("MSZoning", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("LotFrontage", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("FireplaceQu", null, DataType.Nominal, "xxx", nominalMajorityStrategy, false)
            };

            var flatFileHelper = new CsvFlatFile(@"D:\Data\Kaggle\HousePrices\SmallCombined.csv");

            var dataSet = new DataSet(columns, flatFileHelper.Data);

            //MSSubClass Nominal
            //MSZoning Nominal
            //LotFrontage Cont
            //FireplaceQu Nominal
            //SalePrice Continous

        }

        [Fact]
        public void LargeTest()
        {
            //=IF(EXACT(B1, "Nominal"),CONCAT(",new Column('", A1, "', null, DataType.Nominal, 'NA', nominalMajorityStrategy, false)"),CONCAT(",new Column('", A1, "', null, DataType.Continous, 'NA', continousMeanStrategy, false)"))

            var continousMeanStrategy = new MeanContinousValueMissingStrategy();
            var nominalMajorityStrategy = new MajorityNominalValueMissingStrategy();

            var columns = new List<IColumn>
            {
                new Column("Id", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("MSSubClass", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("MSZoning", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("LotFrontage", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("LotArea", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("Street", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("Alley", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("LotShape", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("LandContour", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("Utilities", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("LotConfig", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("LandSlope", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("Neighborhood", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("Condition1", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("Condition2", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("BldgType", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("HouseStyle", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("OverallQual", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("OverallCond", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("YearBuilt", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("YearRemodAdd", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("RoofStyle", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("RoofMatl", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("Exterior1st", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("Exterior2nd", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("MasVnrType", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("MasVnrArea", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("ExterQual", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("ExterCond", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("Foundation", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("BsmtQual", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("BsmtCond", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("BsmtExposure", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("BsmtFinType1", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("BsmtFinSF1", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("BsmtFinType2", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("BsmtFinSF2", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("BsmtUnfSF", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("TotalBsmtSF", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("Heating", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("HeatingQC", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("CentralAir", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("Electrical", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("1stFlrSF", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("2ndFlrSF", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("LowQualFinSF", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("GrLivArea", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("BsmtFullBath", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("BsmtHalfBath", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("FullBath", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("HalfBath", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("BedroomAbvGr", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("KitchenAbvGr", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("KitchenQual", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("TotRmsAbvGrd", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("Functional", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("Fireplaces", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("FireplaceQu", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("GarageType", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("GarageYrBlt", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("GarageFinish", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("GarageCars", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("GarageArea", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("GarageQual", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("GarageCond", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("PavedDrive", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("WoodDeckSF", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("OpenPorchSF", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("EnclosedPorch", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("3SsnPorch", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("ScreenPorch", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("PoolArea", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("PoolQC", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("Fence", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("MiscFeature", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("MiscVal", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("MoSold", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("YrSold", null, DataType.Continous, "NA", continousMeanStrategy, false)
                ,new Column("SaleType", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
                ,new Column("SaleCondition", null, DataType.Nominal, "NA", nominalMajorityStrategy, false)
            };

            var flatFileHelper = new CsvFlatFile(@"D:\Data\Kaggle\HousePrices\Combined.csv");

            var dataSet = new DataSet(columns, flatFileHelper.Data);

        }
    }
}
