namespace EvolutionaryAlgorithm.Helpers
{
    public interface IUniformRandomGenerator
    {
        int GetIntegerRandomNumber(int low, int high);
        double GetContinousRandomNumber(double low, double high);
    }
}
