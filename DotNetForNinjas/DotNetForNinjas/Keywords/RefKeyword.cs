using System;

namespace DotNetForNinjas
{
    // For more information you can look at this link:
    // https://msdn.microsoft.com/en-us/library/14akc2c7.aspx
    public static class RefKeyword
    {
        // Unlike out params, ref params are guaranteed to be intialized before the function
        // is called so they can be accessed right away
        private static void AccessRefBeforeModifying(ref int value)
        {
            var result = value + 1;
        }

        // Since ref params are guaranteed to be initialized, there is no require to modify
        // or set them like with out params
        private static void NoNeedToAssignRefBeforeExiting(ref int value)
        {
        }

        public static void RealWorldExample()
        {
            var a = 1;
            var b = 2;

            Console.WriteLine($"a = {a}, b = {b}");
            Swap(ref a, ref b);
            Console.WriteLine($"a = {a}, b = {b}");
        }

        private static void Swap<T>(ref T t1, ref T t2)
        {
            T temp = t1;
            t1 = t2;
            t2 = temp;
        }
    }
}
