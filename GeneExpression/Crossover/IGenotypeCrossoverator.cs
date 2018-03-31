namespace GeneExpression.Crossover
{
    public interface IGenoTypeCrossoverator
    {
        void PerformCrossover(ref IGenoType genoTypeLeft, ref IGenoType genoTypeRight);
    }
}
