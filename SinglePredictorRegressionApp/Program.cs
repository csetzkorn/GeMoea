using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using EvolutionaryAlgorithm;
using EvolutionaryAlgorithm.Helpers;
using EvolutionaryAlgorithm.NSGA2;
using GeneExpression;
using GeneExpression.Crossover;
using GeneExpression.Functions;
using GeneExpression.Mutation;
using GeneExpression.Terminals;
using NCalc;
using SinglePredictorRegression;

namespace SinglePredictorRegressionApp
{
    class Program
    {
        static void Main()
        {
            var randomGenerator = new UniformRandomGenerator();
            var dataSet = GetDataSet();
            var target = GetTarget();
            var eaGeneExpressionParameters = GetEaGeneExpressionParameters(dataSet);
            var parameterTerminalFactory = new ParameterTerminalFactory(eaGeneExpressionParameters, randomGenerator);
            var genoTypeFactory = new GenoTypeFactory(eaGeneExpressionParameters, randomGenerator, parameterTerminalFactory);
            var mutator = new GenoTypeMutatorBasic1(randomGenerator, eaGeneExpressionParameters, genoTypeFactory);
            var crossOverator = new GenoTypeCrossoveratorBasic1(randomGenerator, eaGeneExpressionParameters);

            var population = GetFirstPopulation(eaGeneExpressionParameters, parameterTerminalFactory, genoTypeFactory);

            for (var generation = 0; generation < eaGeneExpressionParameters.NumberOfGeneration; generation++)
            {
                var listOfObjectiveValues = new List<IObjectiveValues>();

                var smallestMse = double.MaxValue;
                var largestMse = 0.0;
                var smallestPosition = 0;
                var rank1 = new List<string>();
                for (var c = 0; c < eaGeneExpressionParameters.PopulationSize; c++)
                {
                    var objectiveValues = GetObjectiveValues(target, dataSet, population[c]);
                    listOfObjectiveValues.Add(objectiveValues);
                }
             
                listOfObjectiveValues = Nsga2TournamentSelector.PerformSelection(eaGeneExpressionParameters.TournamentSize, listOfObjectiveValues, randomGenerator);

                for (var c = 0; c < eaGeneExpressionParameters.PopulationSize; c++)
                {
                    if (listOfObjectiveValues[c].Rank == 1)
                    {
                        rank1.Add(listOfObjectiveValues[c].IndividualGuid.ToString());
                    }

                    if (listOfObjectiveValues[c].Values[0] < smallestMse)
                    {
                        smallestMse = listOfObjectiveValues[c].Values[0];
                        smallestPosition = c;
                    }

                    if (listOfObjectiveValues[c].Values[0] > largestMse)
                    {
                        largestMse = listOfObjectiveValues[c].Values[0];
                    }
                }

                Console.WriteLine(generation + " " + smallestMse + " " + largestMse + " " + rank1.Count);
                Console.WriteLine(new PhenoTypeTree(population[smallestPosition].GenoType.GenoTypeNodes));

                var tempPopulation = new List<Individual>();
                foreach (var objectiveValues in listOfObjectiveValues)
                {
                    var indy = population.FirstOrDefault(x => x.Guid == objectiveValues.IndividualGuid);

                    tempPopulation.Add((Individual) indy.Clone());
                }
                population = tempPopulation;

                foreach (var individual in population)
                {
                    mutator.PerformMutation(ref individual.GenoType);
                }

                population = PerformCrossOver(population, randomGenerator, crossOverator);
            }
        }

        public static List<Individual> PerformCrossOver(List<Individual> population, IUniformRandomGenerator randomGenerator, IGenoTypeCrossoverator crossOverator)
        {
            var returnObject = new List<Individual>();

            do
            {
                var leftIndex = randomGenerator.GetIntegerRandomNumber(0, population.Count - 1);
                var leftIndividual = population[leftIndex];
                population.RemoveAt(leftIndex);

                Individual rightIndividual = null;
                if (population.Count > 0)
                {
                    var rightIndex = randomGenerator.GetIntegerRandomNumber(0, population.Count - 1);
                    rightIndividual = population[rightIndex];
                    population.RemoveAt(rightIndex);
                }

                
                if (rightIndividual != null)
                {
                    crossOverator.PerformCrossover(ref leftIndividual.GenoType, ref rightIndividual.GenoType);
                    returnObject.Add(rightIndividual);
                }
                returnObject.Add(leftIndividual);


            } while (population.Count >= 1);

            if (population.Count == 1)
            {
                returnObject.Add(population[0]);
            }

            return returnObject;
        }

        public static IObjectiveValues GetObjectiveValues(Target<double> target, IDataSet dataSet, Individual individual)
        {
            var phenoTypeTree = new PhenoTypeTree(individual.GenoType.GenoTypeNodes);
            var stringExpresssion = phenoTypeTree.ToString();

            // TODO make mapped columns more sophisticated
            var mappedColumnsUsedInExpression = new Dictionary<string, int>();

            foreach (var mappedColumn in dataSet.MappedColumns)
            {
                if (stringExpresssion.Contains(mappedColumn.Key))
                {
                    mappedColumnsUsedInExpression.Add(mappedColumn.Key.Replace("[","").Replace("]", "").Trim(), mappedColumn.Value);
                }
            }

            var expression = new Expression(stringExpresssion);

            var numberOfRows = dataSet.MappedData.GetLength(0);
            var mseSum = 0.0;
            var sMape = 0.0;
            for (var row = 0; row < numberOfRows; row++)
            {
                foreach (var usedMappedColumn in mappedColumnsUsedInExpression)
                {
                    expression.Parameters[usedMappedColumn.Key] = dataSet.MappedData[row, usedMappedColumn.Value];
                }

                if (!expression.HasErrors())
                {
                    var prediction = (double) expression.Evaluate();
                    mseSum = mseSum + Math.Pow(prediction - target.Values[row], 2.0);

                    sMape = sMape + (Math.Abs(prediction - target.Values[row])/ ((Math.Abs(prediction) + Math.Abs(target.Values[row]))));
                }
                else
                {
                    mseSum = mseSum + double.MaxValue;
                    sMape = sMape + double.MaxValue;
                }
            }

            var mse = mseSum / numberOfRows;
            sMape = 100.0 * (sMape / numberOfRows);

            if (double.IsNaN(mse) || double.IsInfinity(mse))
            {
                mse = double.MaxValue;
            }

            mse = Math.Log(mse + 1.0);
            sMape = Math.Log(sMape + 1.0);

            var numberOfOpenBrackets = stringExpresssion.Count(c => c == '(');

            //Console.WriteLine(mse + " " + numberOfOpenBrackets);
            string[] names = {"sMape", "OpenBrackets", "Mse"};
            double[] values = {sMape, numberOfOpenBrackets, mse};
//            string[] names = {"Mse", "OpenBrackets" };
//            double[] values = { mse, numberOfOpenBrackets };
            var objectiveValues = new ObjectiveValues(values, names, individual.Guid);
            
            return objectiveValues;
        }

        public static IDataSet GetDataSet()
        {
            var continousMeanStrategy = new MeanContinousValueMissingStrategy();
            
            var columns = new List<IColumn>
            {
                new Column("X", null, DataType.Continous, "NA", continousMeanStrategy)
            };

            var flatFileHelper = new CsvFlatFile(@"D:\Data\sinus\SimpleSinus.csv");

            return new DataSet(columns, flatFileHelper.Data);
        }

        public static Target<double> GetTarget()
        {
            return new ContinousTarget(@"D:\Data\sinus\SimpleSinusTarget.txt");
        }

        public static IEaGeneExpressionParameters GetEaGeneExpressionParameters(IDataSet dataSet)
        {
            var possibleFunctions = new List<IGenoTypeNode>
            {
                new Multiplication(),
                new Division(),
                new Plus(),
                new Minus(),
                new Cosinus(),
                new Exp(),
                new Minimum(),
                new Maximum(),
                new SquareRoot(),
                new Sinus()
            };

            var possibleTerminals = new List<IGenoTypeNode>();
            foreach (var mappedColumn in dataSet.MappedColumns)
            {
                possibleTerminals.Add(new FeatureTerminal(mappedColumn.Key));
            }

            return new EaGeneExpressionParameters(10, possibleFunctions, possibleTerminals, populationSize:500, mutationProbability:0.4, tournamentSize:10, numberOfGeneration:1000);
        }

        public static List<Individual> GetFirstPopulation(IEaGeneExpressionParameters eaGeneExpressionParameters, IParameterTerminalFactory parameterTerminalFactory, IGenoTypeFactory genoTypeFactory)
        {
            var firstPopulation = new List<Individual>();

            for (var c = 0; c < eaGeneExpressionParameters.PopulationSize; c++)
            {
                firstPopulation.Add(new Individual(genoTypeFactory.GetGenoType()));
            }

            return firstPopulation;
        }
    }
}
