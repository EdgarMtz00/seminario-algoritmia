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
        
        public List<Circle> ListFathers()
        {
            TreeNode t = this;
            List<Circle> result = new List<Circle>() {t.C};
            while (t._father != null)
            {
                t = t._father;
                result.Add(t.C);
            }

            return result;
        }
    }
}