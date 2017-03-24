using System;
using System.Globalization;

namespace DateFormatTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var date = new DateTime(2017, 3, 20);
            var format = "MMMMd日";
            var locales = new[] { "en-US", "fr-FR", "ja-JP", "th-TH", "zh-CHS" };

            foreach (var locale in locales)
            {
                var cultureInfo = new CultureInfo(locale);
                var formattedDate = date.ToString(format, cultureInfo);
                Console.WriteLine($"{locale}: {formattedDate}");
            }

            Console.WriteLine("\ndone!");
            Console.ReadLine();
        }
    }
}
