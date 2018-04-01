using System.Collections.Generic;
using Data;
using EvolutionaryAlgorithm.Helpers;
using GeneExpression;
using GeneExpression.Functions;
using GeneExpression.Terminals;
using NCalc;
using Xunit;

namespace IntegrationTests.Data
{
    public class GenoTypeEvaluation
    {
        [Fact]
        public void Test()
        {
            var dataSet = GetDataSet();
            todo(dataSet);
        }

        public DataSet GetDataSet()
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

            return new DataSet(columns, flatFileHelper.Data);
        }

        public void todo(IDataSet dataSet)
        {
            var randomGenerator = new UniformRandomGenerator();

            var possibleFunctions = new List<IGenoTypeNode>
            {
//                new SquareRoot(),
                new Multiplication(),
                new Plus(),
                new Minus()
            };

            var possibleTerminals = new List<IGenoTypeNode>();
            foreach (var mappedColumn in dataSet.MappedColumns)
            {
                possibleTerminals.Add(new FeatureTerminal(mappedColumn));
            }

            var eaGeneExpressionParameters = new EaGeneExpressionParameters(10, possibleFunctions, possibleTerminals);
            var parameterTerminalFactory = new ParameterTerminalFactory(eaGeneExpressionParameters, randomGenerator);
            var genoTypeFactory = new GenoTypeFactory(eaGeneExpressionParameters, randomGenerator, parameterTerminalFactory);

            var genoType = genoTypeFactory.GetGenoType();

            var phenoTypeTree = new PhenoTypeTree(genoType.GenoTypeNodes);

            var stringExpresssion = phenoTypeTree.ToString();

            var expression = new Expression(stringExpresssion);

            var numberOfRows = dataSet.MappedData.GetLength(0);
            var numberOfColumns = dataSet.MappedData.GetLength(1);

            var sum = 0.0;
            for (var row = 0; row < numberOfRows; row++)
            {
                for (var column = 0; column < numberOfColumns; column++)
                {
                    expression.Parameters[dataSet.MappedColumns[column].Replace("[","").Replace("]","")] = dataSet.MappedData[row, column];
                }

                if (!expression.HasErrors())
                {
                    var test = (double) expression.Evaluate();
                    sum = sum + test;
                }
            }
        }


    }
}
