namespace Interview2
{
    public static class RegexMatch
    {
        // Given a pattern test to see if it matches the given message
        // Two wildcard characters are supported:
        //   ? matches any single character
        //   * matches zero or more characters
        public static bool IsMatch(string pattern, string message)
        {
            if (pattern == string.Empty)
            {
                return message == string.Empty;
            }

            char currentChar = pattern[0];

            if (currentChar == '?')
            {
                if (message == string.Empty)
                {
                    return false;
                }
                else
                {
                    return IsMatch(pattern.Substring(1), message.Substring(1));
                }
            }
            else if (currentChar == '*')
            {
                // First assume that * is done consuming
                if (IsMatch(pattern.Substring(1), message))
                {
                    return true;
                }
                else
                {
                    if (message == string.Empty)
                    {
                        return false;
                    }
                    else
                    {
                        return IsMatch(pattern, message.Substring(1));
                    }
                }
            }
            else
            {
                if (message != string.Empty && currentChar == message[0])
                {
                    return IsMatch(pattern.Substring(1), message.Substring(1));
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
