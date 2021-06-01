using System.Collections.Generic;

namespace algorithms.BinaryTree.Traversal
{
    public class PreorderTraversal
    {
        private void Preorder(TreeNode root, List<int> L)
        {
            if (root != null)
            {
                L.Add(root.val);
                Preorder(root.left, L);
                Preorder(root.right, L);
            }
        }

        public IList<int> Preorder(TreeNode root)
        {
            List<int> L = new List<int>();
            Preorder(root, L);
            return L;
        }
    }

    public class PreorderTraversalIterative
    {
        public IList<int> Preorder(TreeNode root)
        {
            List<int> L = new List<int>();
            if (root == null)
            {
                return L;
            }

            Stack<TreeNode> s = new Stack<TreeNode>();
            s.Push(root);
            while (s.Count > 0)
            {
                TreeNode n = s.Pop();
                L.Add(n.val);
                if (n.right != null)
                {
                    s.Push(n.right);
                }

                if (n.left != null)
                {
                    s.Push(n.left);
                }
            }
            return L;
        }
    }
}
