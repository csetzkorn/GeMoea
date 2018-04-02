using System.Collections.Generic;
using System.Linq;

namespace EvolutionaryAlgorithm.NSGA2
{
    public class Nsga2Crowder
    {
        public static List<IObjectiveValues> CalculateCrowdingDistances(List<IObjectiveValues> objectiveValuesList)
        {
            var numberOfObjectives = objectiveValuesList[0].Values.Length;
            var numberOfArrays = objectiveValuesList.Count;
            var mins = new double[numberOfObjectives];
            var maxs = new double[numberOfObjectives];

            for (var i = 0; i < numberOfArrays; i++)
            {
                if (i == 0)
                {
                    for (var i1 = 0; i1 < numberOfObjectives; i1++)
                    {
                        mins[i1] = objectiveValuesList[i].Values[i1];
                        maxs[i1] = objectiveValuesList[i].Values[i1];
                    }
                }
                else
                {
                    for (var i1 = 0; i1 < numberOfObjectives; i1++)
                    {
                        if (mins[i1] > objectiveValuesList[i].Values[i1])
                        {
                            mins[i1] = objectiveValuesList[i].Values[i1];
                        }
                        if (maxs[i1] < objectiveValuesList[i].Values[i1])
                        {
                            maxs[i1] = objectiveValuesList[i].Values[i1];
                        }
                    }
                }
            }

            for (var dimension = 0; dimension < numberOfObjectives; dimension++)
            {
                objectiveValuesList = objectiveValuesList.OrderBy(i => i.Values[dimension]).ToList();

                for (var i = 0; i < numberOfArrays; i++)
                {

                    if (i == 0 || i == (numberOfArrays - 1))
                    {
                        objectiveValuesList[i].CrowdingDistance = objectiveValuesList[i].CrowdingDistance + float.MaxValue;
                    }
                    else
                    {
                        objectiveValuesList[i].CrowdingDistance =
                        (objectiveValuesList[i + 1].Values[dimension] +
                         objectiveValuesList[i - 1].Values[dimension]) / (maxs[dimension] - mins[dimension]);
                    }
                }
            }

            return objectiveValuesList;
        }
    }
}
