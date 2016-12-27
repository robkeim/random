using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneScreen
{
    // Implements a queue class using only a stack
    public class QueueUsingStack<T>
    {
        private Stack<T> stack;
        private bool isInverted;
        private T topElement;

        // Initialize a new instance of the queue class
        public QueueUsingStack()
        {
            stack = new Stack<T>();
        }

        // Add an element to the queue
        public void Enqueue(T element)
        {
            if (isInverted)
            {
                Stack<T> tmp = new Stack<T>();
                while (stack.Count > 0)
                {
                    tmp.Push(stack.Pop());
                }

                tmp.Push(element);
                isInverted = false;
                stack = tmp;
            }
            else
            {
                stack.Push(element);
            }

            if (stack.Count == 1)
            {
                topElement = element;
            }
        }

        // Remove and return the first element from the queue, and throw a invalid operation
        // exception in the event that the queue is empty
        public T Dequeue()
        {
            if (stack.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            if (isInverted)
            {
                T elt = stack.Pop();

                if (stack.Count > 0)
                {
                    topElement = stack.Peek();
                }

                return elt;
            }
            else
            {
                Stack<T> tmp = new Stack<T>();
                while (stack.Count > 1)
                {
                    tmp.Push(stack.Pop());
                }

                T elt = stack.Pop();
                stack = tmp;
                isInverted = true;

                if (stack.Count > 0)
                {
                    topElement = stack.Peek();
                }

                return elt;
            }
        }

        // Return the first element from the queue, and throw a invalid operation
        // exception in the event that the queue is empty
        public T Peek()
        {
            if (stack.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            throw new NotImplementedException();
        }

        // Remove all of the elements from the queue
        public void Clear()
        {
            stack.Clear();
        }

        // Print the current state of the queue (used for debugging purposes only)
        public void Print()
        {
            if (stack.Count == 0)
            {
                Console.Write("Queue is empty");
            }

            IEnumerable<T> toPrint = isInverted ? stack : stack.Reverse();

            foreach (var elt in toPrint)
            {
                Console.Write("{0} ", elt);
            }
            Console.WriteLine();
        }
    }
}
