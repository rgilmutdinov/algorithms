using System;

namespace algorithms.UnionFind
{
    public class QuickFindUF
    {
        private int[] id;    // id[i] = component identifier of i
        
        public QuickFindUF(int n)
        {
            Count = n;
            id = new int[n];
            for (int i = 0; i < n; i++)
                id[i] = i;
        }

        public int Count { get; private set; }

        public int Find(int p)
        {
            Validate(p);
            return id[p];
        }

        // validate that p is a valid index
        private void Validate(int p)
        {
            int n = id.Length;
            if (p < 0 || p >= n)
            {
                throw new ArgumentOutOfRangeException("index " + p + " is not between 0 and " + (n - 1));
            }
        }

        public bool IsConnected(int p, int q)
        {
            Validate(p);
            Validate(q);
            return id[p] == id[q];
        }

        public void Union(int p, int q)
        {
            Validate(p);
            Validate(q);
            int pID = id[p];   // needed for correctness
            int qID = id[q];   // to reduce the number of array accesses

            // p and q are already in the same component
            if (pID == qID) return;

            for (int i = 0; i < id.Length; i++)
                if (id[i] == pID) id[i] = qID;
            Count--;
        }
    }
}
