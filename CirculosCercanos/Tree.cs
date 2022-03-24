using System.Collections.Generic;

namespace CirculosCercanos
{
    public class Tree
    {
        public TreeNode Root { get; }

        public Tree(Circle root)
        {
            Root = new TreeNode(root, null);
        }


        public List<Circle> DFS(TreeNode father)
        {
            father.C.Visited = true;
            foreach (TreeNode son in father.Sons)
            {
                if (son.C.IsDestination)
                {
                    List<Circle> res = new List<Circle>();
                    res.Add(son.C);
                    return res;
                }
                if (!son.C.Visited)
                {
                    List<Circle> possibleRes = DFS(son);

                    if (possibleRes != null)
                    {
                        possibleRes.Add(son.C);
                        return possibleRes;
                    }
                }
            }

            return null;
        }

    }

    public class TreeNode
    {
        public Circle C { get; }
        private TreeNode _father;
        public List<TreeNode> Sons { get; }

        public TreeNode(Circle c, TreeNode father)
        {
            this.C = c;
            this._father = father;
            Sons = new List<TreeNode>();
        }

        public TreeNode AddSon(Circle c)
        {
            TreeNode son = new TreeNode(c, this);
            Sons.Add(son);
            return son;
        }
        
    }
}