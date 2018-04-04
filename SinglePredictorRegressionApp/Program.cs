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

            var populationP = GetFirstPopulation(eaGeneExpressionParameters, parameterTerminalFactory, genoTypeFactory);

            for (var generation = 0; generation < eaGeneExpressionParameters.NumberOfGeneration; generation++)
            {
                var populationQ = new List<Individual>();
                foreach (var individual in populationP)
                {
                    populationQ.Add((Individual)individual.Clone());
                }

                populationQ = PerformCrossOver(populationQ, randomGenerator, crossOverator);

                foreach (var individual in populationQ)
                {
                    mutator.PerformMutation(ref individual.GenoType);
                }

                var listOfObjectiveValuesQ = new List<IObjectiveValues>();
                for (var c = 0; c < populationP.Count; c++)
                {
                    var objectiveValues = GetObjectiveValues(target, dataSet, populationQ[c]);
                    listOfObjectiveValuesQ.Add(objectiveValues);
                }
                listOfObjectiveValuesQ = Nsga2TournamentSelector.PerformSelection(eaGeneExpressionParameters.TournamentSize, listOfObjectiveValuesQ, randomGenerator);

                var listOfObjectiveValuesP = new List<IObjectiveValues>();
                for (var c = 0; c < populationP.Count; c++)
                {
                    var objectiveValues = GetObjectiveValues(target, dataSet, populationP[c]);
                    listOfObjectiveValuesP.Add(objectiveValues);
                }
                listOfObjectiveValuesP = Nsga2Ranker.Rank(listOfObjectiveValuesP);
                listOfObjectiveValuesP = Nsga2Crowder.CalculateCrowdingDistances(listOfObjectiveValuesP);

                var combinedlistOfObjectiveValues = new List<IObjectiveValues>();
                foreach (var objectiveValues in listOfObjectiveValuesP)
                {
                    combinedlistOfObjectiveValues.Add(objectiveValues);
                }
                foreach (var objectiveValues in listOfObjectiveValuesQ)
                {
                    combinedlistOfObjectiveValues.Add(objectiveValues);
                }

                combinedlistOfObjectiveValues = combinedlistOfObjectiveValues.OrderBy(i => i.Rank).ThenByDescending(i => i.CrowdingDistance).ToList();

                var tempPopulation = new List<Individual>();
                var counter = 0;
                var smallestMse = decimal.MaxValue;
                decimal largestMse = 0;
                var smallestPosition = -1;
                foreach (var objectiveValues in combinedlistOfObjectiveValues)
                {
                    var indy = populationP.FirstOrDefault(x => x.Guid == objectiveValues.IndividualGuid) ?? populationQ.FirstOrDefault(x => x.Guid == objectiveValues.IndividualGuid);

                    tempPopulation.Add((Individual)indy.Clone());
                    counter++;

                    var ovs = GetObjectiveValues(target, dataSet, indy);
                    if (ovs.Values[0] < smallestMse)
                    {
                        smallestMse = ovs.Values[0];
                        smallestPosition = counter;
                    }

                    if (ovs.Values[0] > largestMse)
                    {
                        largestMse = ovs.Values[0];
                    }

                    if (counter == eaGeneExpressionParameters.PopulationSize)
                    {
                        break;
                    }
                }
                populationP = tempPopulation;

                Console.WriteLine(generation + " " + smallestMse + " " + largestMse);
                Console.WriteLine(new PhenoTypeTree(populationP[smallestPosition].GenoType.GenoTypeNodes));
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
                    returnObject.Add((Individual) rightIndividual.Clone());
                }
                returnObject.Add((Individual) leftIndividual.Clone());
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

            var numberOfRows = (decimal) dataSet.MappedData.GetLength(0);
            var mae = 0m;
            var rmse = 0m;
            var maxError = 0m;
            for (var row = 0; row < numberOfRows; row++)
            {
                foreach (var usedMappedColumn in mappedColumnsUsedInExpression)
                {
                    expression.Parameters[usedMappedColumn.Key] = dataSet.MappedData[row, usedMappedColumn.Value];
                }

                if (!expression.HasErrors())
                {
                    var prediction = (double) expression.Evaluate();

                    if (double.IsNaN(prediction) || double.IsInfinity(prediction))
                    {
                        mae = decimal.MaxValue;
                        rmse = decimal.MaxValue;
                        maxError = decimal.MaxValue;
                        break;
                    }

                    var error = prediction - target.Values[row];
                    var absError = Math.Abs(error);

                    if (absError > (double) maxError)
                    {
                        if (absError <= (double) decimal.MaxValue)
                        {
                            maxError = (decimal)absError; 
                        }
                        else
                        {
                            maxError = decimal.MaxValue;
                        }
                    }
                    
                    try
                    {
                        mae = mae + (decimal) Math.Abs(error);
                        rmse = rmse + (decimal)Math.Pow(error, 2);
                    }
                    catch (Exception e)
                    {
                        mae = decimal.MaxValue;
                        rmse = decimal.MaxValue;
                        maxError = decimal.MaxValue;
                        break;
                    }
                }
                else
                {
                    mae = decimal.MaxValue;
                    rmse = decimal.MaxValue;
                    maxError = decimal.MaxValue;
                    break;
                }
            }

            if (mae != decimal.MaxValue)
            {
                mae = mae / numberOfRows;
            }

            if (rmse != decimal.MaxValue)
            {
                rmse = Sqrt( rmse / numberOfRows);
            }

            var numberOfNodes = PhenoTypeTree.GetNumberOfNodes(individual.GenoType.GenoTypeNodes);
            var numberOfCharacters = stringExpresssion.Length;
            //var distinctNumberOfFeatures = mappedColumnsUsedInExpression.Count;

            //Console.WriteLine(mse + " " + numberOfOpenBrackets);
            //            string[] names = { "sMape", "OpenBrackets", "Mse" };
            //            double[] values = { sMape, numberOfOpenBrackets, mse };
            string[] names = { "mae", "numberOfNodes", "numberOfCharacters" };
            decimal[] values = { mae, numberOfNodes, numberOfCharacters };
            var objectiveValues = new ObjectiveValues(values, names, individual.Guid);
            
            return objectiveValues;
        }

        public static decimal Sqrt(decimal x, decimal epsilon = 0.0M)
        {
            if (x < 0) throw new OverflowException("Cannot calculate square root from a negative number");

            decimal current = (decimal)Math.Sqrt((double)x), previous;
            do
            {
                previous = current;
                if (previous == 0.0M) return 0;
                current = (previous + x / previous) / 2;
            }
            while (Math.Abs(previous - current) > epsilon);
            return current;
        }

        public static IDataSet GetDataSet()
        {
            var continousMeanStrategy = new MeanContinousValueMissingStrategy();
            
            var columns = new List<IColumn>
            {
                new Column("X", null, DataType.Continous, "NA", continousMeanStrategy)
            };

            var flatFileHelper = new CsvFlatFile(@"D:\Data\Sinus\SimpleSinus.csv");

            return new DataSet(columns, flatFileHelper.Data);
        }

        public static Target<double> GetTarget()
        {
            return new ContinousTarget(@"D:\Data\Sinus\SimpleSinusTarget.txt");
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
                new SquareRoot(),
                new Sinus()
            };

            var possibleTerminals = new List<IGenoTypeNode>();
            foreach (var mappedColumn in dataSet.MappedColumns)
            {
                possibleTerminals.Add(new FeatureTerminal(mappedColumn.Key));
            }

            return new EaGeneExpressionParameters(20, possibleFunctions, possibleTerminals, populationSize:1000, mutationProbability:0.5, tournamentSize:3, numberOfGeneration:500);
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
