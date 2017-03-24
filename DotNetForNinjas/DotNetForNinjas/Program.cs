using System;

namespace DotNetForNinjas
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Reference and value types
            // ReferenceAndValueTypes.ClassVsStruct();

            // Reference types
            // ReferenceTypes.PassByValue();

            // Value types
            // ValueTypes.PassByValue();
            // ValueTypes.PassByReference();

            // The out keyword
            // OutKeyword.RealWorldOutExample();
            
            // The ref keyword
            // RefKeyword.RealWorldExample();

            Console.WriteLine("\ndone!");
            Console.ReadLine();
        }

        private static int? GetValueFromQueryString(string queryString)
        {
            int result;
            return int.TryParse(queryString, out result)
                ? (int?)result
                : null;
        }
    }
}
