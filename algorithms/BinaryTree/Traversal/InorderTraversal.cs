using System.Collections.Generic;

namespace algorithms.BinaryTree.Traversal
{
    public class InorderTraversal
    {
        private List<int> L = new List<int>();
        public IList<int> Inorder(TreeNode root)
        {
            if (root != null)
            {
                Inorder(root.left);
                L.Add(root.val);
                Inorder(root.right);
            }
            return L;
        }
    }

    public class InorderTraversalIterative
    {
        public IList<int> Inorder(TreeNode root)
        {
            List<int> L = new List<int>();
            Stack<TreeNode> S = new Stack<TreeNode>();
            while (S.Count > 0 || root != null)
            {
                while (root != null)
                {
                    S.Push(root);
                    root = root.left;
                }

                root = S.Pop();
                L.Add(root.val);
                root = root.right;
            }
            return L;
        }
    }
}
