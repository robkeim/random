using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview1
{
    class Program
    {
        static void Main(string[] args)
        {
            QueueUsingArray<int> myQueue = new QueueUsingArray<int>();
            //myQueue.Print();

            myQueue.Enqueue(0);
            myQueue.Enqueue(1);
            myQueue.Dequeue();
            myQueue.Enqueue(2);
            myQueue.Print();
            myQueue.PrintArray();

            // 2 1
            /*myQueue.Enqueue(1);
            myQueue.Print();

            myQueue.Enqueue(2);
            myQueue.Print();*/

            Console.ReadLine();
        }
    }
}
