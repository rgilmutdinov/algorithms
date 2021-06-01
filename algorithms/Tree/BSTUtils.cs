using System;
using System.Collections.Generic;
using System.Text;

namespace algorithms.Tree
{
    public class BSTUtils {
        public class TreeNode {
            public int val;
            public TreeNode left;
            public TreeNode right;

            public TreeNode(int val, TreeNode left = null, TreeNode right = null) {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }

        public static TreeNode Search(TreeNode root, int val) {
            if (root == null) return null;
            
            if (root.val < val) {
                return Search(root.left, val);
            }

            if (root.val > val) {
                return Search(root.right, val);
            }

            return root;
        }

        public static TreeNode Insert(TreeNode root, int val) {
            if (root == null) {
                return new TreeNode(val);
            }

            if (root.val < val) {
                root.right = Insert(root.right, val);
            } else {
                root.left = Insert(root.left, val);
            }

            return root;
        }

        public static TreeNode Delete(TreeNode root, int val)
        {
            if (root == null) return null;
            if (root.val < val) {
                root.right = Delete(root.right, val);
            } else if (root.val > val) {
                root.left = Delete(root.left, val);
            } else {
                if (root.left == null) return root.right;
                if (root.right == null) return root.left;

                TreeNode s = FindSuccessor(root);
                root.val = s.val;
                root.right = Delete(root.right, val);
            }

            return root;
        }

        private static TreeNode FindSuccessor(TreeNode root) {
            root = root.right;
            while (root?.left != null) {
                root = root.left;
            }

            return root;
        }
    }
}
