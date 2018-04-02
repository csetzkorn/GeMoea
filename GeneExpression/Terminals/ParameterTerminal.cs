namespace GeneExpression.Terminals
{
    public class ParameterTerminal : Terminal
    {
        public double Value { get; set; }

        public ParameterTerminal(double value)
        {
            Value = value;
        }

        public override string ToString()
        {
            // ReSharper disable once SpecifyACultureInStringConversionExplicitly
            return Value.ToString();
        }

        public override object Clone()
        {
            var returnObject = new ParameterTerminal(Value)
            {
                Middle = Middle
            };

            return returnObject;
        }
    }
}
