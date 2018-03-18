namespace GeneExpression
{
    public class ParameterTerminal : ITerminal
    {
        public double ParameterValue { get; set; }

        public ParameterTerminal(double parameterValue)
        {
            ParameterValue = parameterValue;
        }

        public override string ToString()
        {
            // ReSharper disable once SpecifyACultureInStringConversionExplicitly
            return ParameterValue.ToString();
        }
    }
}
