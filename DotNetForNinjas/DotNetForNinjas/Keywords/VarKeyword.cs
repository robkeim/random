using System.Collections.Generic;

namespace DotNetForNinjas
{
    // For more information you can look at this link:
    // https://msdn.microsoft.com/en-us/library/bb383973.aspx
    public static class VarKeyword
    {
        private static void HowToUseVar()
        {
            // Var is a "shortcut" to avoid having to declare the type of the variable
            int a = 0;
            var b = 0;

            // Both of those statements are equivalent as the compiler knows that
            // b is an int

            // Var cannot be used when declaring but not initializing a value because
            // the compiler is unable to determine what type it should be

            // var noValue;
            // The above line of code causes the following compiler error:
            // Implicitly-typed local variable must be initialized

            // The var keyword was introduced to save "repetitiveness" when delcaring a value.
            // Consdier the following difference:
            Dictionary<string, HashSet<string>> a2 = new Dictionary<string, HashSet<string>>();
            var b2 = new Dictionary<string, HashSet<string>>();

            // In this example it's clear from reading the "new ..." what type of object it's going
            // to be so having it twice in the same line is redundant.

            // However, with great power comes great responsibility (and this is where opinions
            // diverage and discussions become heated on the value of the var keyword). In some cases
            // using var isn't very obvious in terms of what type of object is going to be returned.

            // Consider the following example:
            var result = CalculateAndReturnResult();

            // What type is result? If this method were in another file you would be required to
            // navigate to that file in order to understand the type. You could also make the argument
            // that the method isn't explicitly named (which I did on purpose to illustrate a point),
            // but the take away is that you should use var when it's easy to understand what the
            // type is.
        }

        #region In another file

        private static ComplexObject CalculateAndReturnResult()
        {
            return new ComplexObject();
        }

        private class ComplexObject
        {
        }

        #endregion
    }
}
