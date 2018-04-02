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

        public override object Clone()
        {
            var returnObject = new FeatureTerminal(FeatureName)
            {
                Middle = Middle
            };

            return returnObject;
        }
    }
}
