using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace algorithms.Tree
{
    public class BST<TKey, TValue> where TKey : IComparable<TKey>
    {
        private Node root;             // root of BST

        private class Node
        {
            internal TKey key;           // sorted by key
            internal TValue val;         // associated data
            internal Node left, right;  // left and right subtrees
            internal int size;          // number of nodes in subtree

            public Node(TKey key, TValue val, int size)
            {
                this.key = key;
                this.val = val;
                this.size = size;
            }
        }

        /**
         * Initializes an empty symbol table.
         */
        public BST()
        {
        }

        /**
         * Returns true if this symbol table is empty.
         * @return {@code true} if this symbol table is empty; {@code false} otherwise
         */
        public bool IsEmpty
        {
            get
            {
                return Size == 0;
            }
        }

        /**
         * Returns the number of key-value pairs in this symbol table.
         * @return the number of key-value pairs in this symbol table
         */
        public int Size => GetSize(root);

        // return number of key-value pairs in BST rooted at x
        private int GetSize(Node x)
        {
            if (x == null) return 0;
            return x.size;
        }

        /**
         * Does this symbol table contain the given key?
         *
         * @param  key the key
         * @return {@code true} if this symbol table contains {@code key} and
         *         {@code false} otherwise
         * @throws IllegalArgumentException if {@code key} is {@code null}
         */
        public bool Contains(TKey key)
        {
            if (key == null) throw new ArgumentException("argument to Contains() is null");
            return Get(key) != null;
        }

        /**
         * Returns the value associated with the given key.
         *
         * @param  key the key
         * @return the value associated with the given key if the key is in the symbol table
         *         and {@code null} if the key is not in the symbol table
         * @throws IllegalArgumentException if {@code key} is {@code null}
         */
        public TValue Get(TKey key)
        {
            return Get(root, key);
        }

        private TValue Get(Node x, TKey key)
        {
            if (key == null) throw new ArgumentException("calls Get() with a null key");
            if (x == null) return default(TValue);
            int cmp = key.CompareTo(x.key);
            if (cmp < 0) return Get(x.left, key);
            else if (cmp > 0) return Get(x.right, key);
            else return x.val;
        }

        /**
         * Inserts the specified key-value pair into the symbol table, overwriting the old 
         * value with the new value if the symbol table already contains the specified key.
         * Deletes the specified key (and its associated value) from this symbol table
         * if the specified value is {@code null}.
         *
         * @param  key the key
         * @param  val the value
         * @throws IllegalArgumentException if {@code key} is {@code null}
         */
        public void Put(TKey key, TValue val)
        {
            if (key == null) throw new ArgumentException("calls Put() with a null key");
            if (val == null)
            {
                Delete(key);
                return;
            }
            root = Put(root, key, val);
            Debug.Assert(Check());
        }

        private Node Put(Node x, TKey key, TValue val)
        {
            if (x == null) return new Node(key, val, 1);
            int cmp = key.CompareTo(x.key);
            if (cmp < 0) x.left = Put(x.left, key, val);
            else if (cmp > 0) x.right = Put(x.right, key, val);
            else x.val = val;
            x.size = 1 + GetSize(x.left) + GetSize(x.right);
            return x;
        }


        /**
         * Removes the smallest key and associated value from the symbol table.
         *
         * @throws NoSuchElementException if the symbol table is empty
         */
        public void DeleteMin()
        {
            if (IsEmpty) throw new InvalidOperationException("Symbol table underflow");
            root = DeleteMin(root);
            Debug.Assert(Check());
        }

        private Node DeleteMin(Node x)
        {
            if (x.left == null) return x.right;
            x.left = DeleteMin(x.left);
            x.size = GetSize(x.left) + GetSize(x.right) + 1;
            return x;
        }

        /**
         * Removes the largest key and associated value from the symbol table.
         *
         * @throws NoSuchElementException if the symbol table is empty
         */
        public void DeleteMax()
        {
            if (IsEmpty) throw new InvalidOperationException("Symbol table underflow");
            root = DeleteMax(root);
            Debug.Assert(Check());
        }

        private Node DeleteMax(Node x)
        {
            if (x.right == null) return x.left;
            x.right = DeleteMax(x.right);
            x.size = GetSize(x.left) + GetSize(x.right) + 1;
            return x;
        }

        /**
         * Removes the specified key and its associated value from this symbol table     
         * (if the key is in this symbol table).    
         *
         * @param  key the key
         * @throws IllegalArgumentException if {@code key} is {@code null}
         */
        public void Delete(TKey key)
        {
            if (key == null) throw new ArgumentException("calls Delete() with a null key");
            root = Delete(root, key);
            Debug.Assert(Check());
        }

        private Node Delete(Node x, TKey key)
        {
            if (x == null) return null;

            int cmp = key.CompareTo(x.key);
            if (cmp < 0) x.left = Delete(x.left, key);
            else if (cmp > 0) x.right = Delete(x.right, key);
            else
            {
                if (x.right == null) return x.left;
                if (x.left == null) return x.right;
                Node t = x;
                x = Min(t.right);
                x.right = DeleteMin(t.right);
                x.left = t.left;
            }
            x.size = GetSize(x.left) + GetSize(x.right) + 1;
            return x;
        }


        /**
         * Returns the smallest key in the symbol table.
         *
         * @return the smallest key in the symbol table
         * @throws NoSuchElementException if the symbol table is empty
         */
        public TKey Min()
        {
            if (IsEmpty) throw new InvalidOperationException("calls Min() with empty symbol table");
            return Min(root).key;
        }

        private Node Min(Node x)
        {
            if (x.left == null) return x;
            else return Min(x.left);
        }

        /**
         * Returns the largest key in the symbol table.
         *
         * @return the largest key in the symbol table
         * @throws NoSuchElementException if the symbol table is empty
         */
        public TKey Max()
        {
            if (IsEmpty) throw new InvalidOperationException("calls Max() with empty symbol table");
            return Max(root).key;
        }

        private Node Max(Node x)
        {
            if (x.right == null) return x;
            else return Max(x.right);
        }

        /**
         * Returns the largest key in the symbol table less than or equal to {@code key}.
         *
         * @param  key the key
         * @return the largest key in the symbol table less than or equal to {@code key}
         * @throws NoSuchElementException if there is no such key
         * @throws IllegalArgumentException if {@code key} is {@code null}
         */
        public TKey Floor(TKey key)
        {
            if (key == null) throw new ArgumentException("argument to Floor() is null");
            if (IsEmpty) throw new InvalidOperationException("calls Floor() with empty symbol table");
            Node x = Floor(root, key);
            if (x == null) throw new InvalidOperationException("argument to Floor() is too small");
            else return x.key;
        }

        private Node Floor(Node x, TKey key)
        {
            if (x == null) return null;
            int cmp = key.CompareTo(x.key);
            if (cmp == 0) return x;
            if (cmp < 0) return Floor(x.left, key);
            Node t = Floor(x.right, key);
            if (t != null) return t;
            else return x;
        }

        public TKey Floor2(TKey key)
        {
            TKey x = Floor2(root, key, default(TKey));
            if (x == null) throw new InvalidOperationException("argument to Floor() is too small");
            else return x;

        }

        private TKey Floor2(Node x, TKey key, TKey best)
        {
            if (x == null) return best;
            int cmp = key.CompareTo(x.key);
            if (cmp < 0) return Floor2(x.left, key, best);
            else if (cmp > 0) return Floor2(x.right, key, x.key);
            else return x.key;
        }

        /**
         * Returns the smallest key in the symbol table greater than or equal to {@code key}.
         *
         * @param  key the key
         * @return the smallest key in the symbol table greater than or equal to {@code key}
         * @throws NoSuchElementException if there is no such key
         * @throws IllegalArgumentException if {@code key} is {@code null}
         */
        public TKey Ceiling(TKey key)
        {
            if (key == null) throw new ArgumentException("argument to Ceiling() is null");
            if (IsEmpty) throw new InvalidOperationException("calls Ceiling() with empty symbol table");
            Node x = Ceiling(root, key);
            if (x == null) throw new InvalidOperationException("argument to Floor() is too large");
            else return x.key;
        }

        private Node Ceiling(Node x, TKey key)
        {
            if (x == null) return null;
            int cmp = key.CompareTo(x.key);
            if (cmp == 0) return x;
            if (cmp < 0)
            {
                Node t = Ceiling(x.left, key);
                if (t != null) return t;
                else return x;
            }
            return Ceiling(x.right, key);
        }

        /**
         * Return the key in the symbol table of a given {@code Rank}.
         * This key has the property that there are {@code Rank} Keys in
         * the symbol table that are smaller. In other words, this key is the
         * ({@code Rank}+1)st smallest key in the symbol table.
         *
         * @param  Rank the order statistic
         * @return the key in the symbol table of given {@code Rank}
         * @throws IllegalArgumentException unless {@code Rank} is between 0 and
         *        <em>n</em>–1
         */
        public TKey Select(int rank)
        {
            if (rank < 0 || rank >= Size)
            {
                throw new ArgumentException("argument to Select() is invalid: " + rank);
            }
            return Select(root, rank);
        }

        // Return key in BST rooted at x of given Rank.
        // Precondition: Rank is in legal range.
        private TKey Select(Node x, int rank)
        {
            if (x == null) return default(TKey);
            int leftSize = GetSize(x.left);
            if (leftSize > rank) return Select(x.left, rank);
            else if (leftSize < rank) return Select(x.right, rank - leftSize - 1);
            else return x.key;
        }

        /**
         * Return the number of Keys in the symbol table strictly less than {@code key}.
         *
         * @param  key the key
         * @return the number of Keys in the symbol table strictly less than {@code key}
         * @throws IllegalArgumentException if {@code key} is {@code null}
         */
        public int Rank(TKey key)
        {
            if (key == null) throw new ArgumentException("argument to Rank() is null");
            return Rank(key, root);
        }

        // Number of Keys in the subtree less than key.
        private int Rank(TKey key, Node x)
        {
            if (x == null) return 0;
            int cmp = key.CompareTo(x.key);
            if (cmp < 0) return Rank(key, x.left);
            else if (cmp > 0) return 1 + GetSize(x.left) + Rank(key, x.right);
            else return GetSize(x.left);
        }

        /**
         * Returns all Keys in the symbol table as an {@code Iterable}.
         * To iterate over all of the Keys in the symbol table named {@code st},
         * use the foreach notation: {@code for (Key key : st.Keys())}.
         *
         * @return all Keys in the symbol table
         */
        public IEnumerable<TKey> Keys()
        {
            if (IsEmpty) return new Queue<TKey>();
            return Keys(Min(), Max());
        }

        /**
         * Returns all Keys in the symbol table in the given range,
         * as an {@code Iterable}.
         *
         * @param  lo minimum endpoint
         * @param  hi maximum endpoint
         * @return all Keys in the symbol table between {@code lo} 
         *         (inclusive) and {@code hi} (inclusive)
         * @throws IllegalArgumentException if either {@code lo} or {@code hi}
         *         is {@code null}
         */
        public IEnumerable<TKey> Keys(TKey lo, TKey hi)
        {
            if (lo == null) throw new ArgumentException("first argument to Keys() is null");
            if (hi == null) throw new ArgumentException("second argument to Keys() is null");

            Queue<TKey> queue = new Queue<TKey>();
            Keys(root, queue, lo, hi);
            return queue;
        }

        private void Keys(Node x, Queue<TKey> queue, TKey lo, TKey hi)
        {
            if (x == null) return;
            int cmplo = lo.CompareTo(x.key);
            int cmphi = hi.CompareTo(x.key);
            if (cmplo < 0) Keys(x.left, queue, lo, hi);
            if (cmplo <= 0 && cmphi >= 0) queue.Enqueue(x.key);
            if (cmphi > 0) Keys(x.right, queue, lo, hi);
        }

        /**
         * Returns the number of Keys in the symbol table in the given range.
         *
         * @param  lo minimum endpoint
         * @param  hi maximum endpoint
         * @return the number of Keys in the symbol table between {@code lo} 
         *         (inclusive) and {@code hi} (inclusive)
         * @throws IllegalArgumentException if either {@code lo} or {@code hi}
         *         is {@code null}
         */
        public int GetSize(TKey lo, TKey hi)
        {
            if (lo == null) throw new ArgumentException("first argument to GetSize() is null");
            if (hi == null) throw new ArgumentException("second argument to GetSize() is null");

            if (lo.CompareTo(hi) > 0) return 0;
            if (Contains(hi)) return Rank(hi) - Rank(lo) + 1;
            else return Rank(hi) - Rank(lo);
        }

        /**
         * Returns the height of the BST (for debugging).
         *
         * @return the height of the BST (a 1-node tree has height 0)
         */
        public int Height => GetHeight(root);

        private int GetHeight(Node x)
        {
            if (x == null) return -1;
            return 1 + Math.Max(GetHeight(x.left), GetHeight(x.right));
        }

        /**
         * Returns the Keys in the BST in level order (for debugging).
         *
         * @return the Keys in the BST in level order traversal
         */
        public IEnumerable<TKey> LevelOrder()
        {
            Queue<TKey> keys = new Queue<TKey>();
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                Node x = queue.Dequeue();
                if (x == null) continue;
                keys.Enqueue(x.key);
                queue.Enqueue(x.left);
                queue.Enqueue(x.right);
            }
            return keys;
        }

        /*************************************************************************
          *  Check integrity of BST data structure.
          ***************************************************************************/
        private bool Check()
        {
            if (!IsBST()) Console.WriteLine("Not in symmetric order");
            if (!IsSizeConsistent()) Console.WriteLine("Subtree counts not consistent");
            if (!IsRankConsistent()) Console.WriteLine("Ranks not consistent");
            return IsBST() && IsSizeConsistent() && IsRankConsistent();
        }

        // does this binary tree satisfy symmetric order?
        // Note: this test also ensures that data structure is a binary tree since order is strict
        private bool IsBST()
        {
            return IsBST(root, default(TKey), default(TKey));
        }

        // is the tree rooted at x a BST with all Keys strictly between Min and Max
        // (if Min or Max is null, treat as empty constraint)
        // Credit: Bob Dondero's elegant solution
        private bool IsBST(Node x, TKey min, TKey max)
        {
            if (x == null) return true;
            if (min != null && x.key.CompareTo(min) <= 0) return false;
            if (max != null && x.key.CompareTo(max) >= 0) return false;
            return IsBST(x.left, min, x.key) && IsBST(x.right, x.key, max);
        }

        // are the size fields correct?
        private bool IsSizeConsistent() { return IsSizeConsistent(root); }
        private bool IsSizeConsistent(Node x)
        {
            if (x == null) return true;
            if (x.size != GetSize(x.left) + GetSize(x.right) + 1) return false;
            return IsSizeConsistent(x.left) && IsSizeConsistent(x.right);
        }

        // check that ranks are consistent
        private bool IsRankConsistent()
        {
            for (int i = 0; i < Size; i++)
                if (i != Rank(Select(i))) return false;
            foreach (TKey key in Keys())
                if (key.CompareTo(Select(Rank(key))) != 0) return false;
            return true;
        }
    }
}
