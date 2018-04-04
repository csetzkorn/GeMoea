using System.Collections.Generic;
using System.Linq;
using EvolutionaryAlgorithm.Helpers;

namespace EvolutionaryAlgorithm.NSGA2
{
    public class Nsga2TournamentSelector
    {
        public static List<IObjectiveValues> PerformSelection(int tournamentSize, List<IObjectiveValues> originalObjectiveValues, IUniformRandomGenerator randomGenerator)
        {
            var populationSize = originalObjectiveValues.Count;
            originalObjectiveValues = Nsga2Ranker.Rank(originalObjectiveValues);
            originalObjectiveValues = Nsga2Crowder.CalculateCrowdingDistances(originalObjectiveValues);
            var newPopulation = new List<IObjectiveValues>();

            do
            {
                var indices = new List<int>();
                var individuals = new List<IObjectiveValues>();
                do
                {
                    var index = randomGenerator.GetIntegerRandomNumber(0, originalObjectiveValues.Count - 1);
                    if (indices.Contains(index) == false)
                    {
                        indices.Add(index);
                        individuals.Add(originalObjectiveValues[index]);
                    }

                } while (indices.Count() < tournamentSize);

                individuals = individuals.OrderBy(i => i.Rank).ToList();
                //.ThenByDescending(i => i.CrowdingDistance).
                if (individuals[0].Rank == individuals[1].Rank)
                {
                    if (individuals[1].CrowdingDistance > individuals[0].CrowdingDistance)
                    {
                        newPopulation.Add(individuals[1]);
                    }
                    else
                    {
                        newPopulation.Add(individuals[0]);
                    }
                }
                else
                {
                    newPopulation.Add(individuals[0]);
                }
                
            } while (newPopulation.Count < populationSize);

            return newPopulation;
        }
    }
}
