using System.Collections.Generic;

namespace GeneExpression
{
    public class PhenoTypeTree : IPhenoTypeTree
    {
        public IPhenoTypeNode RootNode { get; set; }

        public PhenoTypeTree(List<IGenoTypeNode> genoTypeNodes)
        {
            RootNode = Parse(genoTypeNodes);
        }

        private IPhenoTypeNode Parse(List<IGenoTypeNode> genoTypeNodes)
        {
            var nodeQueue = new Queue<IPhenoTypeNode>();
            
            var root = new PhenoTypeNode();
            nodeQueue.Enqueue(root);

            foreach (var genoTypeNode in genoTypeNodes)
            {
                if (nodeQueue.Count == 0)
                {
                    break;
                }

                var node = nodeQueue.Dequeue();
                node.Expression = genoTypeNode.ToString(); ;
                node.Middle = genoTypeNode.Middle;

                var nodeArity = genoTypeNode.GetArity();

                if (nodeArity == 1)
                {
                    node.Left = new PhenoTypeNode();
                    nodeQueue.Enqueue(node.Left);
                }
                else if (nodeArity == 2)
                {
                    node.Left = new PhenoTypeNode();
                    nodeQueue.Enqueue(node.Left);
                    node.Right = new PhenoTypeNode();
                    nodeQueue.Enqueue(node.Right);
                }
            }

            return root;
        }

        public override string ToString()
        {
            return RootNode.ToString();
        }
    }
}
