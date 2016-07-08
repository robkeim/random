using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
        private static readonly ConcurrentBag<string> Nouns = new ConcurrentBag<string>();
        static void Main(string[] args)
        {
            try
            {
                ServicePointManager.DefaultConnectionLimit = 256;

                var words = System.IO.File.ReadAllLines(@".\FrenchWords.txt");

                foreach (var word in words)
                {
                    ProcessWord(word).Wait();
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception={e}");
            }

            using (var streamWriter = new StreamWriter(@"c:\users\rkeim\desktop\output.txt"))
            {
                foreach (var noun in Nouns.OrderBy(n => n))
                {
                    Console.WriteLine(noun);
                    streamWriter.WriteAsync(noun);
                }
            }

            Console.WriteLine("\ndone!");
            Console.ReadLine();
        }

        // The site throttled my traffic almost immediately after I started sending requests, so I'm not going to be able
        // to run calls in parallel
        private static async Task ProcessWords(IEnumerable<string> words, int numConcurrentCalls)
        {
            var tasks = words.Select(ProcessWord);

            var running = new LinkedList<Task>();

            foreach (var task in tasks)
            {
                running.AddLast(task);

                // Restrict only 
                if (running.Count < numConcurrentCalls)
                {
                    continue;
                }

                await Task.WhenAny(running);

                var finished = running.First(t => t.IsCompleted);
                await finished;
                running.Remove(finished);
            }

            await Task.WhenAll(running);
        }

        private static readonly Regex MasculineRegex = new Regex(">Nom masculin", RegexOptions.Compiled);
        private static readonly Regex FeminineRegex = new Regex(">Nom féminin", RegexOptions.Compiled);
        private static readonly Regex ThrottledRegex = new Regex("Limite de connections atteinte, veuillez réessayer plus tard", RegexOptions.Compiled);

        private static async Task ProcessWord(string word)
        {
            try
            {
                var url = new Uri($"http://www.le-dictionnaire.com/definition.php?mot={RemoveAccents(word)}");
                var client = new WebClient();
                var result = await client.DownloadStringTaskAsync(url);

                if (MasculineRegex.Match(result).Success)
                {
                    Nouns.Add($"{word}\tm");

                    Console.WriteLine($"{word}\tm");
                    if (Nouns.Count % 100 == 0)
                    {
                        Console.WriteLine($"{Nouns.Count} nouns found so far");
                    }
                }
                else if (FeminineRegex.Match(result).Success)
                {
                    Nouns.Add($"{word}\tf");

                    Console.WriteLine($"{word}\tf");
                    if (Nouns.Count % 100 == 0)
                    {
                        Console.WriteLine($"{Nouns.Count} nouns found so far");
                    }
                }
                else if (ThrottledRegex.Match(result).Success)
                {
                    throw new InvalidOperationException("Getting throttled :(");
                }
                else
                {
                    Console.WriteLine($"{word}\tnone");
                }
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
