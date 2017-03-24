namespace DotNetForNinjas.Operators
{
    public static class TernaryOperator
    {
        private static void HowToUse()
        {
            var country = "en";

            // The following pattern is so common in .NET they introduced an operator
            // to make the syntax more compact
            string language;

            if (country == "th")
            {
                language = "th";
            }
            else
            {
                language = "en";
            }

            // That can be shortend to the following:
            language = country == "th"
                ? "th"
                : "en";
        }
    }
}
