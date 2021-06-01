using System;

namespace algorithms.UnionFind
{
    public class UF
    {
        private int[] parent;  // parent[i] = parent of i
        private byte[] rank;   // Rank[i] = Rank of subtree rooted at i (never more than 31)

        /// <summary>
        /// Initializes an empty union-find data structure with
        /// <c>n</c> elements <c>0</c> through <c>n - 1</c>.
        /// Initially, each elements is in its own set.
        /// </summary>
        /// <param name="n">the number of elements</param>
        /// <exception cref="ArgumentException">n &lt; 0</exception>
        public UF(int n)
        {
            if (n < 0) throw new ArgumentException();
            Count = n;
            parent = new int[n];
            rank = new byte[n];
            for (int i = 0; i < n; i++)
            {
                parent[i] = i;
                rank[i] = 0;
            }
        }

        /// <summary>
        /// Returns the canonical element of the set containing element <c>p</c>.
        /// </summary>
        /// <param name="p">an element</param>
        /// <returns>the canonical element of the set containing <c>p</c></returns>
        /// <exception cref="ArgumentOutOfRangeException">unless <c>0 &lt;= p &lt; n</c></exception>
        public int Find(int p)
        {
            Validate(p);
            while (p != parent[p])
            {
                parent[p] = parent[parent[p]];    // path compression by halving
                p = parent[p];
            }
            return p;
        }

        /// <summary>
        /// Returns the number of sets (between <c>1</c> and <c>n</c>).
        /// </summary>
        public int Count { get; private set; }


        /// <summary>
        /// Returns true if the two elements are in the same set.
        /// </summary>
        /// <param name="p">one element</param>
        /// <param name="q">the other element</param>
        /// <returns><c>true</c> if <c>p</c> and <c>q</c> are in the same set; <c>false</c> otherwise</returns>
        /// <exception cref="ArgumentOutOfRangeException">unless both <c>0 &lt;= p &lt; n</c> and <c>0 &lt;= q &lt;</c></exception>
        public bool IsConnected(int p, int q)
        {
            return Find(p) == Find(q);
        }

        /// <summary>
        /// Merges the set containing element <c>p</c> with the the set containing element <c>q</c>.
        /// </summary>
        /// <param name="p">one element</param>
        /// <param name="q">the other element</param>
        /// <exception cref="ArgumentOutOfRangeException">unless both <c>0 &lt;= p &lt; n</c> and <c>0 &lt;= q &lt;</c></exception>
        public void Union(int p, int q)
        {
            int rootP = Find(p);
            int rootQ = Find(q);
            if (rootP == rootQ) return;

            // make root of smaller Rank point to root of larger Rank
            if (rank[rootP] < rank[rootQ]) parent[rootP] = rootQ;
            else if (rank[rootP] > rank[rootQ]) parent[rootQ] = rootP;
            else
            {
                parent[rootQ] = rootP;
                rank[rootP]++;
            }
            Count--;
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
    }
}
