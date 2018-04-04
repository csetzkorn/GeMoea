using System;
using System.Collections.Generic;
using EvolutionaryAlgorithm;
using Xunit;

namespace UnitTests.Nsga2Crowder
{
    public class Nsga2CrowderTests
    {
        [Fact]
        public void Test()
        {
            var listOfObjectiveValues = new List<IObjectiveValues>(); ;

//            string[] names = { "a", "b" };
//            double[] values = { 0.25,0.25 };
//            var objectiveValues = new ObjectiveValues(values, names, Guid.NewGuid());
//            listOfObjectiveValues.Add(objectiveValues);
//
//            names = new []{ "a", "b" };
//            values = new []{ 0.5, 0.25 };
//            objectiveValues = new ObjectiveValues(values, names, Guid.NewGuid());
//            listOfObjectiveValues.Add(objectiveValues);
//
//            names = new[] { "a", "b" };
//            values = new[] { 1.0, 1.0 };
//            objectiveValues = new ObjectiveValues(values, names, Guid.NewGuid());
//            listOfObjectiveValues.Add(objectiveValues);

            var actuals = EvolutionaryAlgorithm.NSGA2.Nsga2Crowder.CalculateCrowdingDistances(listOfObjectiveValues);
        }

    }
}
