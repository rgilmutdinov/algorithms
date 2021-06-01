using System.Collections.Generic;

namespace algorithms.HashSet
{
    public class SimpleListHashSet
    {
        class Bucket
        {
            private LinkedList<int> list;

            public Bucket()
            {
                list = new LinkedList<int>();
            }

            public void Insert(int key)
            {
                LinkedListNode<int> node = list.Find(key);
                if (node == null)
                {
                    list.AddFirst(key);
                }
            }

            public bool Remove(int key)
            {
                return list.Remove(key);
            }

            public bool Contains(int key)
            {
                return list.Find(key) != null;
            }
        }

        private const int KeyRange = 739;
        private readonly Bucket[] buckets;

        /** Initialize your data structure here. */
        public SimpleListHashSet()
        {
            buckets = new Bucket[KeyRange];
            for (int i = 0; i < KeyRange; i++)
            {
                buckets[i] = new Bucket();
            }
        }

        public void Add(int key)
        {
            int bucketIndex = GetHash(key);
            buckets[bucketIndex].Insert(key);
        }

        public void Remove(int key)
        {
            int bucketIndex = GetHash(key);
            buckets[bucketIndex].Remove(key);
        }

        /** Returns true if this set contains the specified element */
        public bool Contains(int key)
        {
            int bucketIndex = GetHash(key);
            return buckets[bucketIndex].Contains(key);
        }

        private int GetHash(int key)
        {
            return key % KeyRange;
        }
    }
}
