using System;

namespace algorithms.UnionFind
{
    public class WeightedQuickUnionUF
    {
        private int[] parent;   // parent[i] = parent of i
        private int[] size;     // size[i] = number of elements in subtree rooted at i

        public WeightedQuickUnionUF(int n)
        {
            Count = n;
            parent = new int[n];
            size = new int[n];
            for (int i = 0; i < n; i++)
            {
                parent[i] = i;
                size[i] = 1;
            }
        }

        public int Count { get; private set; }

        public int Find(int p)
        {
            Validate(p);
            while (p != parent[p])
                p = parent[p];
            return p;
        }

        public bool IsConnected(int p, int q)
        {
            return Find(p) == Find(q);
        }

        // validate that p is a valid index
        private void Validate(int p)
        {
            int n = parent.Length;
            if (p < 0 || p >= n)
            {
                throw new ArgumentOutOfRangeException("index " + p + " is not between 0 and " + (n - 1));
            }
        }

        public void Union(int p, int q)
        {
            int rootP = Find(p);
            int rootQ = Find(q);
            if (rootP == rootQ) return;

            // make smaller root point to larger one
            if (size[rootP] < size[rootQ])
            {
                parent[rootP] = rootQ;
                size[rootQ] += size[rootP];
            }
            else
            {
                parent[rootQ] = rootP;
                size[rootP] += size[rootQ];
            }
            Count--;
        }
    }
}
