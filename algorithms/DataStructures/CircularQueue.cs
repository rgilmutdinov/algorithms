namespace algorithms.DataStructures
{
    public class CircularQueue
    {
        private int[] data;
        private int front;
        private int capacity;
        private int count;

        public CircularQueue(int k)
        {
            data = new int[k];
            capacity = k;
            count = 0;
            front = 0;
        }

        public bool EnQueue(int value)
        {
            if (IsFull())
            {
                return false;
            }

            data[(front + count) % capacity] = value;
            count++;
            return true;
        }

        public bool DeQueue()
        {
            if (IsEmpty())
            {
                return false;
            }

            front = (front + 1) % capacity;
            count--;
            return true;
        }

        public int Front()
        {
            if (IsEmpty())
            {
                return -1;
            }

            return data[front];
        }

        public int Rear()
        {
            if (IsEmpty())
            {
                return -1;
            }

            return data[(front + count - 1) % capacity];
        }

        public bool IsEmpty()
        {
            return count == 0;
        }

        public bool IsFull()
        {
            return count == capacity;
        }
    }
}
