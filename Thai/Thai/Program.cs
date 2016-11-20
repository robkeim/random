using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Thai
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var googleClient = new WebClient())
            using (var soundOfTextClient = new WebClient())
            {
                googleClient.Encoding = Encoding.UTF8;

                foreach (var englishPhrase in File.ReadAllLines(@"c:\users\rkeim\desktop\input.txt"))
                {
                    Console.WriteLine($"{englishPhrase}");
                    var result = googleClient.DownloadString($"https://translate.googleapis.com/translate_a/single?client=gtx&sl=en&tl=th&dt=t&q={HttpUtility.UrlEncode(englishPhrase)}");
                    var match = Regex.Match(result, "\"(.*?)\"");
                    var thaiPhrase = match.Groups[1].ToString();
                    soundOfTextClient.DownloadFile($"http://soundoftext.com/static/sounds/th/{HttpUtility.UrlEncode(thaiPhrase)}.mp3", $@"c:\users\rkeim\desktop\{englishPhrase} - {thaiPhrase}.mp3");
                }
            }

            Console.WriteLine("\ndone!");
            Console.ReadLine();
        }
    }
}
