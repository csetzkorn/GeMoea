namespace GeneExpression
{
    public interface IPhenoTypeNode
    {
        string Expression { get; set; }
        IPhenoTypeNode Left { get; set; }
        IPhenoTypeNode Right { get; set; }
        bool Middle { get; set; }
    }
}
