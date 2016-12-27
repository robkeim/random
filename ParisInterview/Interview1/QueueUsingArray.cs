using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview1
{
    public class QueueUsingArray<T>
    {
        private T[] elements;
        private int headIndex;
        private int numElements;

        // Create a new instance of the MyQueue class
        public QueueUsingArray(int initialSize = 2)
        {
            elements = new T[initialSize];
            headIndex = 0;
            numElements = 0;
        }

        public void Enqueue(T element)
        {
            if (numElements == elements.Length)
            {
                this.ResizeArray();
            }

            elements[(headIndex + numElements) % elements.Length] = element;
            numElements++;
        }

        public T Dequeue()
        {
            if (numElements == 0)
            {
                throw new InvalidOperationException("Cannot perform a dequeue when the queue is empty");
            }

            T returnValue = elements[headIndex];

            headIndex = (headIndex + 1) % elements.Length;
            numElements--;

            return returnValue;
        }

        public void Print()
        {
            if (numElements == 0)
            {
                Console.WriteLine("Queue is empty");
                return;
            }

            for (int i = 0; i < numElements; i++)
            {
                Console.Write(string.Format("{0} ", elements[(headIndex + i) % elements.Length]));
            }
            Console.WriteLine();
        }

        public void PrintArray()
        {
            Console.WriteLine("This is printing out the contents of the raw underlying array NOT the current state of the queue");
            for (int i = 0; i < elements.Length; i++)
            {
                Console.Write(string.Format("{0} ", elements[i]));
            }

            Console.WriteLine();
        }

        private void ResizeArray()
        {
            T[] newArray = new T[elements.Length * 2];

            for (int i = 0; i < elements.Length; i++)
            {
                newArray[i] = elements[(headIndex + i) % numElements];
            }

            elements = newArray;
            headIndex = 0;
        }
    }
}
