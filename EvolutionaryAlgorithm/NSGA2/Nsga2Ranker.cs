using System.Collections.Generic;

namespace EvolutionaryAlgorithm.NSGA2
{
    public class Nsga2Ranker
    {
        public static List<IObjectiveValues> Rank(List<IObjectiveValues> objectiveValuesList)
        {
            foreach (var ovs in objectiveValuesList)
            {
                ovs.Reset();
            }

            for (var i = 0; i < objectiveValuesList.Count; i++)
            {
                var ov1 = objectiveValuesList[i];
                for (var j = 0; j < objectiveValuesList.Count; j++)
                {
                    if (i != j)
                    {
                        var ov2 = objectiveValuesList[j];

                        var comparisonResults = ov1.CompareTo(ov2);

                        if (comparisonResults == 1)
                        {
                            ov1.DominatingObjectiveValues.Add(ov2);
                            ov2.DominatedByCount++;
                        }
                    }
                }
            }

            var currentRank = 1;
            while (true)
            {
                var rankZeroCounter = 0;
                var ovsToBeReduced = new List<IObjectiveValues>();
                foreach (var ovsOuter in objectiveValuesList)
                {
                    if (ovsOuter.Rank == 0)
                    {
                        rankZeroCounter++;
                        if (ovsOuter.DominatedByCount == 0)
                        {
                            ovsOuter.Rank = currentRank;
                            foreach (var ovsInner in ovsOuter.DominatingObjectiveValues)
                            {
                                if (ovsInner.DominatedByCount > 0)
                                {
                                    ovsToBeReduced.Add(ovsInner);
                                }
                            }
                        }
                    } 
                }

                //objectiveValuesList = objectiveValuesList.OrderByDescending(i => i.Rank).ToList();

                foreach (var ovs in ovsToBeReduced)
                {
                    ovs.DominatedByCount = ovs.DominatedByCount - 1;
                }

                currentRank++;
                if (rankZeroCounter == 0) break;
            }

            //objectiveValuesList = objectiveValuesList.OrderBy(i => i.Rank).ToList();

            return objectiveValuesList;
        }
    }
}
