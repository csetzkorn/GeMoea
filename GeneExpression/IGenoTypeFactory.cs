namespace GeneExpression
{
    public interface IGenoTypeFactory
    {
        IGenoType GetGenoType();

        // TODO factor out into node factory?
        IGenoTypeNode GetTerminalNode();
        IGenoTypeNode GetFunctionNode();
        IGenoTypeNode GetFunctionOrTerminalNode();
    }
}
