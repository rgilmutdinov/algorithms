using System;

namespace algorithms.UnionFind
{
    public class QuickUnionUF
    {
        private int[] parent;  // parent[i] = parent of i

        public QuickUnionUF(int n)
        {
            parent = new int[n];
            Count = n;
            for (int i = 0; i < n; i++)
            {
                parent[i] = i;
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

        // validate that p is a valid index
        private void Validate(int p)
        {
            int n = parent.Length;
            if (p < 0 || p >= n)
            {
                throw new ArgumentOutOfRangeException("index " + p + " is not between 0 and " + (n - 1));
            }
        }

        public bool IsConnected(int p, int q)
        {
            return Find(p) == Find(q);
        }

        public void Union(int p, int q)
        {
            int rootP = Find(p);
            int rootQ = Find(q);
            if (rootP == rootQ) return;
            parent[rootP] = rootQ;
            Count--;
        }
    }
}
