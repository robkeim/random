using System;
using System.Collections.Generic;

namespace ReserveGymClasses
{
    public static class Logger
    {
        private static List<string> _messages = new List<string>();

        public static void Log(string message)
        {
            _messages.Add(message);
            Console.WriteLine(message);
        }

        public static string GetLogMessages()
        {
            return string.Join("\n", _messages);
        }
    }
}
