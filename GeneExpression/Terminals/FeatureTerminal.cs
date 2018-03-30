namespace GeneExpression.Terminals
{
    public class FeatureTerminal : Terminal
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
