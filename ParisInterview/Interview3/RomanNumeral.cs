namespace Interview3
{
    using System;
    using System.Collections.Generic;

    public static class RomanNumeral
    {
        // Given a string input return the corresponding roman numeral value
        // This method currently does not do validation that the input is valid beyond just containing
        // valid characters.  Example, IIII will return 4 and not indicate an error
        public static int GetValue(string romanNumeral)
        {
            if (string.IsNullOrEmpty(romanNumeral))
            {
                throw new ArgumentException("Roman numeral may not be null or empty");
            }

            int index = romanNumeral.Length - 1;
            int total = 0;

            while (index >= 0)
            {
                char currentChar = romanNumeral[index];
                char previousChar = index > 0 ? romanNumeral[index - 1] : ' ';

                if (!romanNumeralValues.ContainsKey(currentChar))
                {
                    throw new ArgumentException(string.Format("The character {0} is not a valid roman numeral character", currentChar));
                }

                total += romanNumeralValues[currentChar];

                if (romanNumeralValues.ContainsKey(previousChar) && romanNumeralValues[previousChar] < romanNumeralValues[currentChar])
                {
                    total -= romanNumeralValues[previousChar];
                    index--;
                }

                index--;
            }

            return total;
        }

        // A mapping of each roman numeral to it's corresponding integer value
        private static Dictionary<char, int> romanNumeralValues = new Dictionary<char, int>
        {
            { 'M', 1000 },
            { 'D', 500 },
            { 'C', 100 },
            { 'L', 50 },
            { 'X', 10 },
            { 'V', 5 },
            { 'I', 1 },
        };
    }
}
