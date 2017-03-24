using System;

namespace DotNetForNinjas
{
    // In C#, there are two different types reference types and value types.
    //
    // Reference types include:
    // - classes
    // - interfaces
    // - delegates
    //
    // Value types include:
    // - structs
    // - enums
    //
    // In functions, all parameters can either be passed by reference or by value.
    // By default, all parameters are passed by value and the ref and out keywords
    // are used to pass something by reference.
    //
    // For more information you can look at these links:
    // Passing parameters: https://msdn.microsoft.com/en-us/library/0f66670z.aspx
    // Value types: https://msdn.microsoft.com/en-us/library/s1ax56ch.aspx
    // Passing value types: https://msdn.microsoft.com/en-us/library/9t0za5es.aspx
    // Reference types: https://msdn.microsoft.com/en-us/library/490f96s2.aspx
    // Passing reference types: https://msdn.microsoft.com/en-us/library/s6938f28.aspx
    public class ReferenceAndValueTypes
    {
        /// <summary>
        /// This highlights the difference between reference types and value types in C#.
        /// </summary>
        public void ClassVsStruct()
        {
            // Class
            Console.WriteLine("--- class ---");
            var classExample = new ClassExample { Value = 1 };
            Console.WriteLine($"Before: {classExample}");
            UpdateValue(classExample, 2);
            Console.WriteLine($"After: {classExample}");

            Console.ReadKey();

            // Struct
            Console.WriteLine("\n--- struct ---");
            var sructExample = new StructExample { Value = 3 };
            Console.WriteLine($"Before: {sructExample}");
            UpdateValue(sructExample, 4);
            Console.WriteLine($"After: {sructExample}");
        }

        private void UpdateValue(IValue valueToUpdate, int newValue)
        {
            valueToUpdate.Value = newValue;
            Console.WriteLine($"In function: {valueToUpdate}");
        }
    }

    public class ClassExample : IValue
    {
        public int Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public struct StructExample : IValue
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
