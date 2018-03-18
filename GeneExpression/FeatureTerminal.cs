namespace GeneExpression
{
    public class FeatureTerminal : ITerminal
    {
        public string FeatureName { get; set; }

        public FeatureTerminal(string featureName)
        {
            FeatureName = featureName;
        }

        public override string ToString()
        {
            return FeatureName;
        }
    }
}
