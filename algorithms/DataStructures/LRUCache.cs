using System.Collections.Generic;

namespace algorithms.DataStructures
{
    public class LRUCache
    {
        class ListNode
        {
            public ListNode() { }
            public ListNode(int key, int value)
            {
                this.key = key;
                this.value = value;
            }

            public int key;
            public int value;
            public ListNode next;
            public ListNode prev;
        }

        private Dictionary<int, ListNode> cache;
        private ListNode head;
        private ListNode tail;
        private int size;
        private int capacity;

        public LRUCache(int capacity)
        {
            this.capacity = capacity;
            this.size = 0;

            cache = new Dictionary<int, ListNode>();
            head = new ListNode();
            tail = new ListNode();
            head.next = tail;
            tail.prev = head;
        }

        public int Get(int key)
        {
            if (cache.TryGetValue(key, out ListNode node))
            {
                MoveToHead(node);
                return node.value;
            }

            return -1;
        }

        public void Put(int key, int value)
        {
            if (cache.TryGetValue(key, out ListNode node))
            {
                node.value = value;
                MoveToHead(node);
            }
            else
            {
                ListNode newNode = new ListNode(key, value);
                AddNode(newNode);
                cache.Add(key, newNode);

                size++;
                if (size > capacity)
                {
                    ListNode nodeToRemove = PopTail();
                    cache.Remove(nodeToRemove.key);

                    size--;
                }
            }
        }

        private void RemoveNode(ListNode node)
        {
            ListNode prev = node.prev;
            ListNode next = node.next;

            prev.next = next;
            next.prev = prev;
        }

        private void AddNode(ListNode node)
        {
            node.prev = head;
            node.next = head.next;

            head.next.prev = node;
            head.next = node;
        }

        private void MoveToHead(ListNode node)
        {
            RemoveNode(node);
            AddNode(node);
        }

        private ListNode PopTail()
        {
            ListNode node = tail.prev;
            RemoveNode(node);
            return node;
        }
    }
}
