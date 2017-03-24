using System;

namespace DotNetForNinjas
{
    // Value types include:
    // - structs
    // - enums
    //
    // For more information you can look at these links:
    // Value types: https://msdn.microsoft.com/en-us/library/s1ax56ch.aspx
    // Passing value types: https://msdn.microsoft.com/en-us/library/9t0za5es.aspx
    public static class ValueTypes
    {
        #region Pass by value

        /// <summary>
        /// This highlights passing value types by value
        /// </summary>
        public static void PassByValue()
        {
            Console.WriteLine("--- update value ---");
            var value = 1;
            Console.WriteLine($"Before: {value}");
            UpdateValue(value, 2);
            Console.WriteLine($"After: {value}");
        }

        // Value types are immutable so the function cannot modify the object
        private static void UpdateValue(int value, int newValue)
        {
            value = newValue;
            Console.WriteLine($"In function: {value}");
        }

        #endregion

        #region Pass by reference

        /// <summary>
        /// This highlights passing value types by reference.
        /// </summary>
        public static void PassByReference()
        {
            Console.WriteLine("--- update value with ref keyword ---");
            var value = 1;
            Console.WriteLine($"Before: {value}");
            UpdateValueWithRef(ref value, 2);
            Console.WriteLine($"After: {value}");

            Console.ReadKey();

            Console.WriteLine("\n--- update value with out keyword ---");
            int valueToUpdate;
            Console.WriteLine("Before: not initialized");
            UpdateValueWithOut(out valueToUpdate, 2);
            Console.WriteLine($"After: {valueToUpdate}");
        }
        
        private static void UpdateValueWithRef(ref int value, int newValue)
        {
            value = newValue;
            Console.WriteLine($"In function: {value}");
        }

        private static void UpdateValueWithOut(out int value, int newValue)
        {
            value = newValue;
            Console.WriteLine($"In function: {value}");
        }
        
        #endregion

        public class ExampleClass
        {
            public int Value { get; set; }

            public override string ToString()
            {
                return Value.ToString();
            }
        }
    }
}