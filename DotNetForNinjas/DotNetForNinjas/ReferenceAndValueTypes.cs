using System;

namespace DotNetForNinjas
{
    public static class ReferenceAndValueTypes
    {
        /// <summary>
        /// This highlights the difference between reference types and value types in C#.
        /// </summary>
        public static void ClassVsStruct()
        {
            // Class
            Console.WriteLine("--- class ---");
            var exampleClass = new ExampleClass { Value = 1 };
            Console.WriteLine($"Before: {exampleClass}");
            UpdateValue(exampleClass, 2);
            Console.WriteLine($"After: {exampleClass}");

            Console.ReadKey();

            // Struct
            Console.WriteLine("\n--- struct ---");
            var exampleStruct = new ExampleStruct { Value = 3 };
            Console.WriteLine($"Before: {exampleStruct}");
            UpdateValue(exampleStruct, 4);
            Console.WriteLine($"After: {exampleStruct}");
        }

        private static void UpdateValue(IValue valueToUpdate, int newValue)
        {
            valueToUpdate.Value = newValue;
            Console.WriteLine($"In function: {valueToUpdate}");
        }

        public class ExampleClass : IValue
        {
            public int Value { get; set; }

            public override string ToString()
            {
                return Value.ToString();
            }
        }

        public struct ExampleStruct : IValue
        {
            public int Value { get; set; }

            public override string ToString()
            {
                return Value.ToString();
            }
        }

        public interface IValue
        {
            int Value { get; set; }
        }
    }
}
