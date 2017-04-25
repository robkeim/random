using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Thai
{
    public class Program
    {
        private const string RootDir = @"c:\users\rkeim\desktop\thai\";

        public static void Main(string[] args)
        {
            Translate();
            //DownloadAudio();

            Console.WriteLine("\ndone!");
            Console.ReadLine();
        }

        private static void Translate()
        {
            Console.WriteLine("Translating...");

            using (var client = new WebClient())
            using (var resultsOutput = new StreamWriter(Path.Combine(RootDir, "_translations.txt")))
            {
                client.Encoding = Encoding.UTF8;

                foreach (var englishText in File.ReadAllLines(Path.Combine(RootDir, "_english.txt")))
                {
                    Console.WriteLine(englishText);

                    // Google Translate offers a paid API, but this is the same call that Google translate is using on the web so it's free
                    var result = client.DownloadString($"https://translate.googleapis.com/translate_a/single?client=gtx&sl=en&tl=th&dt=t&q={HttpUtility.UrlEncode(englishText)}");
                    var match = Regex.Match(result, "\"(.*?)\"");
                    var thaiText = match.Groups[1].ToString();

                    resultsOutput.WriteLine($"{englishText} - {thaiText}");
                }
            }
        }

        private static void DownloadAudio()
        {
            Console.WriteLine("\nDownloading audio...");

            using (var soundOfTextClient = new WebClient())
            {
                foreach (var line in File.ReadAllLines(Path.Combine(RootDir, "_translations.txt")))
                {
                    var match = Regex.Match(line, "(.*?) - (.*)");

                    Console.WriteLine(match.Groups[1]);
                    var thaiText = match.Groups[2].ToString();
                    var encodedThaiText = HttpUtility.UrlEncode(thaiText);

                    // This first call is required to populate the cache where the sound is retrieved in the following call
                    soundOfTextClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
                    soundOfTextClient.UploadString("http://soundoftext.com/sounds", $"text={encodedThaiText}&lang=th");

                    // The download links have spaces replaced by underscores
                    thaiText = thaiText.Replace(" ", "_");
                    encodedThaiText = HttpUtility.UrlEncode(thaiText);

                    // Remove invalid characters from path before saving file
                    var path = line
                        .Replace("/", " or ")
                        .Replace("?", string.Empty);

                    soundOfTextClient.DownloadFile($"http://soundoftext.com/static/sounds/th/{encodedThaiText}.mp3", Path.Combine(RootDir, "audio", $"{path}.mp3"));
                }
            }
        }
    }
}
