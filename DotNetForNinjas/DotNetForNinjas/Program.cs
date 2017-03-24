using System;

namespace DotNetForNinjas
{
    class Program
    {
        public static void Main(string[] args)
        {
            // ReferenceAndValueTypes
            var example = new ReferenceAndValueTypes();
            example.ClassVsStruct();

            Console.WriteLine("\ndone!");
            Console.ReadLine();
        }
    }
}
