using System;
using System.Collections.Generic;
using System.Linq;

namespace EvolutionaryAlgorithm.NSGA2
{
    public class Nsga2Crowder
    {
        private static List<IObjectiveValues> NormaliseObjectiveValues(List<IObjectiveValues> objectiveValuesList, int numberOfObjectives, int numberOfObjectiveVectors, ref decimal[] mins, ref decimal[] maxs)
        {

            var secondLargestNumber = objectiveValuesList.Select(x => x.Values[0]).Distinct().ToList().OrderByDescending(x  => x).Skip(1).FirstOrDefault();

            for (var c = 0; c < numberOfObjectiveVectors; c++)
            {
                if (objectiveValuesList[c].Invalid)
                {
                    objectiveValuesList[c].Values[0] = secondLargestNumber;
                }
            }

            for (var c1 = 0; c1 < numberOfObjectiveVectors; c1++)
            {
                for (var c2 = 0; c2 < numberOfObjectives; c2++)
                {
                    // TODO improve this
                    if (c2 == 0 && objectiveValuesList[c1].Invalid)
                    {
                        objectiveValuesList[c1].Values[c2] = secondLargestNumber - 1;
                    }

                    var currentValue = objectiveValuesList[c1].Values[c2];
                    if (currentValue < mins[c2])
                    {
                        mins[c2] = currentValue;
                    }
                    if (currentValue > maxs[c2])
                    {
                        maxs[c2] = currentValue;
                    }
                }
            }

            for (var c1 = 0; c1 < numberOfObjectiveVectors; c1++)
            {
                for (var c2 = 0; c2 < numberOfObjectives; c2++)
                {
                    objectiveValuesList[c1].Values[c2] =
                        (objectiveValuesList[c1].Values[c2] - mins[c2]) / (maxs[c2] - mins[c2]);
                }
            }

            return objectiveValuesList;
        }

        public static List<IObjectiveValues> CalculateCrowdingDistances(List<IObjectiveValues> objectiveValuesList)
        {           
            var numberOfObjectives = objectiveValuesList[0].Values.Length;
            var numberOfObjectiveVectors = objectiveValuesList.Count;
            var mins = new decimal[numberOfObjectives];
            var maxs = new decimal[numberOfObjectives];

            for (var c = 0; c < numberOfObjectives; c++)
            {
                mins[c] = decimal.MaxValue;
                maxs[c] = 0;
            }

            //var test1 = objectiveValuesList.OrderBy(i => i.Values[0]).ToList();

            objectiveValuesList = NormaliseObjectiveValues(objectiveValuesList, numberOfObjectives, numberOfObjectiveVectors, ref mins, ref maxs);

            //var test2 = objectiveValuesList.OrderBy(i => i.Values[0]).ToList();

            for (var c1 = 0; c1 < numberOfObjectiveVectors; c1++)
            {
                objectiveValuesList[c1].CrowdingDistance = 0;
            }

            for (var dimension = 0; dimension < numberOfObjectives; dimension++)
            {
                objectiveValuesList = objectiveValuesList.OrderBy(i => i.Values[dimension]).ToList();

                for (var i = 0; i < numberOfObjectiveVectors; i++)
                {
                    if (objectiveValuesList[i].Invalid)
                    {
                        objectiveValuesList[i].CrowdingDistance = 0;
                    }
                    else
                    {
                        if (i == 0 || i == (numberOfObjectiveVectors - 1))
                        {
                            objectiveValuesList[i].CrowdingDistance = decimal.MaxValue;
                        }
                        else
                        {
                            try
                            {
                                objectiveValuesList[i].CrowdingDistance = objectiveValuesList[i].CrowdingDistance +
                                                                          ((objectiveValuesList[i + 1].Values[dimension] -
                                                                            objectiveValuesList[i - 1].Values[dimension]) / (maxs[dimension] - mins[dimension]));
                            }
                            catch (Exception e)
                            {
                                objectiveValuesList[i].CrowdingDistance = 0;
                            }

                        }
                    }
                }
            }

            return objectiveValuesList;
        }
    }
}
