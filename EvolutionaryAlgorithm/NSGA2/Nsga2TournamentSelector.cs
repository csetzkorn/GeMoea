using System.Collections.Generic;
using System.Linq;
using EvolutionaryAlgorithm.Helpers;

namespace EvolutionaryAlgorithm.NSGA2
{
    public class Nsga2TournamentSelector
    {
        public static List<IObjectiveValues> PerformSelection(int tournamentSize, List<IObjectiveValues> originalObjectiveValues, IUniformRandomGenerator randomGenerator)
        {
            originalObjectiveValues = originalObjectiveValues.Where(x => x.Invalid == false).ToList();

            var populationSize = originalObjectiveValues.Count;
            originalObjectiveValues = Nsga2Ranker.Rank(originalObjectiveValues);
            originalObjectiveValues = Nsga2Crowder.CalculateCrowdingDistances(originalObjectiveValues);
            var newPopulationObjectiveValues = new List<IObjectiveValues>();

            do
            {
                var indices = new List<int>();
                var pickedObjectiveValues = new List<IObjectiveValues>();
                do
                {
                    var index = randomGenerator.GetIntegerRandomNumber(0, originalObjectiveValues.Count - 1);
                    if (indices.Contains(index)) continue;
                    indices.Add(index);
                    pickedObjectiveValues.Add(originalObjectiveValues[index]);
                } while (indices.Count() < tournamentSize);

                pickedObjectiveValues = pickedObjectiveValues.OrderBy(i => i.Rank).ToList();

                if (pickedObjectiveValues[0].Rank == pickedObjectiveValues[1].Rank)
                {
                    if (pickedObjectiveValues[1].CrowdingDistance > pickedObjectiveValues[0].CrowdingDistance)
                    {
                        newPopulationObjectiveValues.Add(pickedObjectiveValues[1]);
                    }
                    else
                    {
                        newPopulationObjectiveValues.Add(pickedObjectiveValues[0]);
                    }
                }
                else
                {
                    newPopulationObjectiveValues.Add(pickedObjectiveValues[0]);
                }
                
            } while (newPopulationObjectiveValues.Count < populationSize);

            return newPopulationObjectiveValues;
        }
    }
}
