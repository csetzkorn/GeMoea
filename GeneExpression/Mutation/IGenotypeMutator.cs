namespace GeneExpression.Mutation
{
    public interface IGenoTypeMutator
    {
        void PerformMutation(ref IGenoType genoType);
    }
}
