using System;

namespace DotNetForNinjas
{
    // Reference types include:
    // - classes
    // - interfaces
    // - delegates
    //
    // For more information you can look at these links:
    // Reference types: https://msdn.microsoft.com/en-us/library/490f96s2.aspx
    // Passing reference types: https://msdn.microsoft.com/en-us/library/s6938f28.aspx
    public static class ReferenceTypes
    {
        #region Pass by value

        /// <summary>
        /// This highlights passing reference types by value
        /// </summary>
        public static void PassByValue()
        {
            Console.WriteLine("--- update value ---");
            var exampleClass = new ExampleClass { Value = 1 };
            Console.WriteLine($"Before: {exampleClass}");
            UpdateValue(exampleClass, 2);
            Console.WriteLine($"After: {exampleClass}");

            Console.ReadKey();

            Console.WriteLine("\n--- update reference ---");
            exampleClass = new ExampleClass { Value = 1 };
            Console.WriteLine($"Before: {exampleClass}");
            UpdateReference(exampleClass, 2);
            Console.WriteLine($"After: {exampleClass}");
        }

        // Reference types are mutable so the function can modify the object
        private static void UpdateValue(ExampleClass exampleClass, int newValue)
        {
            exampleClass.Value = newValue;
            Console.WriteLine($"In function: {exampleClass}");
        }

        // Modifing the value (in this case the reference of example class) has no effect
        private static void UpdateReference(ExampleClass exampleClass, int newValue)
        {
            exampleClass = new ExampleClass { Value = newValue };
            Console.WriteLine($"In function: {exampleClass}");
        }

        #endregion

        #region Pass by reference

        /// <summary>
        /// This highlights passing reference types by reference. This is not a common useful
        /// scenario, but it's included for completeness.
        /// </summary>
        public static void PassByReference()
        {
            Console.WriteLine("--- update reference with ref in class ---");
            var exampleClass = new ExampleClass { Value = 1 };
            Console.WriteLine($"Before: {exampleClass}");
            UpdateReferenceWithRef(ref exampleClass, 2);
            Console.WriteLine($"After: {exampleClass}");
        }
        
        private static void UpdateReferenceWithRef(ref ExampleClass exampleClass, int newValue)
        {
            exampleClass = new ExampleClass { Value = newValue };
            Console.WriteLine($"In function: {exampleClass}");
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
