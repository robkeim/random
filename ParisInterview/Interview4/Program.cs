using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview4
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] test = new int[] { 2 };

            List<int> missingElements = Array.FindMissingElements(test);

            Console.WriteLine("Missing elements:");

            if (missingElements.Count == 0)
            {
                Console.WriteLine("None");
            }

            foreach (int element in missingElements)
            {
                Console.Write(string.Format("{0} ", element));
            }

            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
