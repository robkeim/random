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

                foreach (var englishPhrase in File.ReadAllLines(@"c:\users\rkeim\desktop\thai\input.txt"))
                {
                    Console.WriteLine($"{englishPhrase}");
                    var googleResult = googleClient.DownloadString($"https://translate.googleapis.com/translate_a/single?client=gtx&sl=en&tl=th&dt=t&q={HttpUtility.UrlEncode(englishPhrase)}");
                    var match = Regex.Match(googleResult, "\"(.*?)\"");
                    var thaiPhrase = match.Groups[1].ToString();
                    var encodedThaiPhrase = HttpUtility.UrlEncode(thaiPhrase);

                    // This first call is required to populate the cache where the sound is retrieved in the following call
                    soundOfTextClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
                    soundOfTextClient.UploadString("http://soundoftext.com/sounds", $"text={encodedThaiPhrase}&lang=th");
                    
                    soundOfTextClient.DownloadFile($"http://soundoftext.com/static/sounds/th/{encodedThaiPhrase}.mp3", $@"c:\users\rkeim\desktop\thai\{englishPhrase} - {thaiPhrase}.mp3");
                }
            }

            Console.WriteLine("\ndone!");
            Console.ReadLine();
        }
    }
}
