namespace DotNetForNinjas
{
    // For more information you can look at this link:
    // https://msdn.microsoft.com/en-us/library/t3c3bfhx.aspx
    public static class OutKeyword
    {
        // Out params cannot be accessed before being assigned
        private static void MustAssignBeforeAccess(out int value)
        {
            // var result = value + 1;

            // The above line produces this compiler error:
            // Out parameter 'value' might not be initialized before accessing

            value = 0;
        }

        // Out params must be have a value set before exiting the function
        private static void MustAssignBeforeExiting(out int value)
        {
            value = 0;

            // Commenting out the above line results in the following compiler error
            // Parameter 'value' must be assigned upon exit
        }

        public static void RealWorldOutExample()
        {
            // In order to parse values from strings (ints, double, bools, DateTimes, etc)
            // .NET provides Parse and TryParse methods.

            // The Parse method takes a string and returns the parsed value
            int parseResult = int.Parse("1");

            // Result has a value of 1 after this call as you'd expect.

            // But now what happens if you try to parse a nonsense value?
            parseResult = int.Parse("you can't parse me!");

            // An ugly exception:
            // An unhandled exception of type 'System.FormatException' occurred in mscorlib.dll

            // The TryParse method attempts to parse the string and returns a boolean indicating
            // if the parse succeeded or not.
            int tryParseResult;
            bool wasSuccessful = int.TryParse("1", out tryParseResult);

            // In this case wasSuccessful will be set to try and tryParseResult will be set to 1.

            wasSuccessful = int.TryParse("you can't parse me!", out tryParseResult);

            // No exception is thrown, and wasSuccessful will be set to false and tryParseResult
            // will be the default value for the type (in this case 0).
        }

        // Here's a conceptual way to think about what TryParse is doing, but in reality
        // the .NET framework is not using try/catch as it's too time consuming. If you'd
        // like to "look under the hood" and see how int.TryParse is implemented you can
        // look directly at the source code here:
        // https://referencesource.microsoft.com/#mscorlib/system/int32.cs
        private static bool IntTryParsePseudocode(string s, out int value)
        {
            try
            {
                value = int.Parse(s);
                return true;
            }
            catch
            {
                value = 0;
                return false;
            }
        }
    }
}
