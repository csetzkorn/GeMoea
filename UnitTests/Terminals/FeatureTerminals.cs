using GeneExpression;
using Xunit;

namespace UnitTests.Terminals
{
    public class FeatureTerminals
    {
        [Fact]
        public void CanCreateFeatureTerminal()
        {
            var feature = new FeatureTerminal("X1"); 
        }

        [Fact]
        public void CanCreateAndObtainStringFromFeature()
        {
            var featureString = "X1";
            var feature = new FeatureTerminal(featureString);

            Assert.Equal(featureString, feature.ToString());
        }
    }

}
