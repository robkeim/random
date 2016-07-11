using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GetGenderFromDictionary
{
    class Program
    {
        private static readonly ConcurrentDictionary<string, string> Words = new ConcurrentDictionary<string, string>();
        private static string PreviousWord = "";

        static void Main(string[] args)
        {
            try
            {
                ServicePointManager.DefaultConnectionLimit = 256;

                var alreadyProcessedWords = File.ReadAllLines(@"c:\users\rkeim\desktop\output.txt");

                foreach (var alreadyProcessedWord in alreadyProcessedWords)
                {
                    var split = alreadyProcessedWord.Split('\t');
                    Words[split[0]] = split[1];
                }

                var words = File.ReadAllLines(@".\FrenchWords.txt")
                    .Except(Words.Keys)
                    .OrderBy(w => Guid.NewGuid());

                Random random = new Random();

                using (var streamWriter = new StreamWriter(@"c:\users\rkeim\desktop\output.txt", append: true))
                {
                    foreach (var word in words)
                    {
                        ProcessWordAsync(word, streamWriter).Wait();
                        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(random.Next(2, 10)));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception={e}");
            }

            Console.WriteLine("\ndone!");
            Console.ReadLine();
        }

        private static readonly Regex MasculineRegex = new Regex(">Nom masculin", RegexOptions.Compiled);
        private static readonly Regex FeminineRegex = new Regex(">Nom féminin", RegexOptions.Compiled);
        private static readonly Regex ThrottledRegex = new Regex("Limite de connections atteinte, veuillez réessayer plus tard", RegexOptions.Compiled);

        private static async Task ProcessWordAsync(string word, StreamWriter streamWriter)
        {
            try
            {
                var url = new Uri($"http://www.le-dictionnaire.com/definition.php?mot={RemoveAccents(word)}");
                var client = new WebClient();

                // Add headers to simulate the web reqeuests that are done
                client.Headers.Add(HttpRequestHeader.Host, "www.le-dictionnaire.com");
                client.Headers.Add("Upgrade-Insecure-Requests", "1");
                client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla / 5.0(Windows NT 10.0; WOW64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 51.0.2704.103 Safari / 537.36");
                client.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                client.Headers.Add(HttpRequestHeader.Referer, $"http://www.le-dictionnaire.com/definition.php?mot={PreviousWord}");
                client.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, sdch");
                client.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.8,fr-FR;q=0.6,fr;q=0.4");

                var result = await client.DownloadStringTaskAsync(url);

                // This is used to simulate the real call pattern on the web (not sure why they're issuing this request on every lookup)
                await client.DownloadStringTaskAsync(new Uri("http://www.le-dictionnaire.com/definition.php?cookiechoices.js"));

                if (MasculineRegex.Match(result).Success)
                {
                    ProcessWord(word, "m", streamWriter);
                }
                else if (FeminineRegex.Match(result).Success)
                {
                    ProcessWord(word, "f", streamWriter);
                }
                else if (ThrottledRegex.Match(result).Success)
                {
                    throw new InvalidOperationException("Getting throttled :(");
                }
                else
                {
                    ProcessWord(word, "none", streamWriter);
                }

                PreviousWord = word;
            }
            catch (InvalidOperationException)
            {
                // Ensure throttling gets bubbled up so we kill the program
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Word={word};Exception={e}");
            }
        }

        private static void ProcessWord(string word, string value, StreamWriter streamWriter)
        {
            Words[word] = value;

            var lineToWrite = $"{word}\t{value}";
            Console.WriteLine(lineToWrite);
            streamWriter.WriteLine(lineToWrite);
            streamWriter.Flush();
            if (Words.Count % 100 == 0)
            {
                Console.WriteLine($"{Words.Count} nouns found so far");
            }
        }

        // Accent list from here: http://www.effectivelanguagelearning.com/free-language-lessons/french/lesson-3
        private static string RemoveAccents(string word)
        {
            var result = new StringBuilder(word.Length);

            foreach (var c in word)
            {
                var noAccentChar = c;
                switch (c)
                {
                    case 'à':
                    case 'â':
                        noAccentChar = 'a';
                        break;
                    case 'ç':
                        noAccentChar = 'c';
                        break;
                    case 'é':
                    case 'è':
                    case 'ê':
                    case 'ë':
                        noAccentChar = 'e';
                        break;
                    case 'î':
                    case 'ï':
                        noAccentChar = 'i';
                        break;
                    case 'ô':
                        noAccentChar = 'o';
                        break;
                    case 'ù':
                    case 'û':
                    case 'ü':
                        noAccentChar = 'u';
                        break;
                    case 'ÿ':
                        noAccentChar = 'y';
                        break;
                }

                result.Append(noAccentChar);
            }

            return result.ToString();
        }
    }
}
