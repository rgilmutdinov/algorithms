using System.Collections.Generic;

namespace algorithms.BinaryTree.Traversal
{
    public class PostorderTraversal
    {
        private List<int> L = new List<int>();
        public IList<int> Postorder(TreeNode root)
        {
            if (root == null)
            {
                return L;
            }

            Postorder(root.left);
            Postorder(root.right);
            L.Add(root.val);

            return L;
        }
    }

    // Iterative Preorder Traversal: Tweak the Order of the Output
    public class PostorderTraversalStack
    {
        public LinkedList<int> Postorder(TreeNode root)
        {
            LinkedList<int> L = new LinkedList<int>();
            Stack<TreeNode> S = new Stack<TreeNode>();

            if (root == null) {
                return L;
            }

            S.Push(root);
            while (S.Count > 0)
            {
                root = S.Pop();
                L.AddFirst(root.val);
                if (root.left != null) S.Push(root.left);
                if (root.right != null) S.Push(root.right);
            }

            return L;
        }
    }

    public class PostorderTraversalIterative
    {
        public IList<int> Postorder(TreeNode root)
        {
            List<int> L = new List<int>();
            if (root == null)
            {
                return L;
            }

            Stack<TreeNode> S = new Stack<TreeNode>();
            while (S.Count > 0 || root != null)
            {
                if (root != null)
                {
                    S.Push(root);
                    root = root.left;
                }
                else
                {
                    TreeNode temp = S.Peek().right;
                    if (temp == null)
                    {
                        temp = S.Pop();
                        L.Add(temp.val);
                        while (S.Count > 0 && S.Peek().right == temp)
                        {
                            temp = S.Pop();
                            L.Add(temp.val);
                        }
                    }
                    else
                    {
                        root = temp;
                    }
                }
            }

            return L;
        }

        public IList<int> PostorderTraversal1(TreeNode root)
        {
            List<int> res = new List<int>();
            Stack<TreeNode> S = new Stack<TreeNode>();
            while (S.Count > 0 || root != null)
            {
                // find leaf nodes
                while (root != null)
                {
                    S.Push(root);
                    if (root.left != null)
                    {
                        root = root.left;
                    }
                    else
                    {
                        root = root.right;
                    }
                }
                TreeNode node = S.Pop();
                res.Add(node.val);
                if (S.Count > 0 && S.Peek().left == node)
                {
                    root = S.Peek().right;
                }
            }
            return res;
        }
    }
}
