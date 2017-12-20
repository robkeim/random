using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;

namespace SyncExperiments
{
    public class Program
    {
        public static Dictionary<string, Experiment> _experiments = new Dictionary<string, Experiment>();

        public static void Main(string[] args)
        {
            Console.WriteLine("Fetching prod experiments...");
            ExtractExperiments(GetRunningExperiments(true), true);
            Console.WriteLine("Fetching dev experiments...");
            ExtractExperiments(GetRunningExperiments(false));
            Console.WriteLine();
            PrintSummary();

            Console.WriteLine("\ndone!");
            Console.ReadLine();
        }
        
        private static void ExtractExperiments(string json, bool prod = false)
        {
            dynamic obj = JObject.Parse(json);
            obj = obj.result;

            for (int i = 0; i < obj.Count; i++)
            {
                string name = obj[i].experimentName;

                if (name.ToUpperInvariant().StartsWith("HALO-"))
                {
                    string description = obj[i].description;
                    var startDate = DateTimeOffset.Parse(obj[i].startDate.ToString());

                    Experiment exp;

                    if (!_experiments.TryGetValue(name, out exp))
                    {
                        exp = new Experiment();
                    }

                    exp.Name = name;
                    exp.Description = description;

                    if (prod)
                    {
                        exp.ProdStartDate = startDate;
                    }
                    else
                    {
                        exp.DevStartDate = startDate;
                    }

                    _experiments[name] = exp;
                }
            }
        }

        private static string GetRunningExperiments(bool isProduction)
        {
            var url = isProduction
                ? "http://expapi.agoda.local/rest/experiment/dashboard/running"
                : "http://bk-expapp-1001:39002/rest/experiment/dashboard/running";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            using (var httpResponse = (HttpWebResponse)httpWebRequest.GetResponseAsync().Result)
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                return streamReader.ReadToEnd();
            }
        }

        private static void PrintSummary()
        {
            var now = DateTimeOffset.UtcNow;
            var exps = _experiments.Values.OrderBy(e => e.Name).ToArray();

            foreach (var exp in exps)
            {
                var description = exp.Description.Length > 100
                    ? $"{exp.Description.Substring(0, 100)}..."
                    : exp.Description;

                var devDate = exp.DevStartDate != null
                    ? $"{Math.Round((now - exp.DevStartDate.Value).TotalDays, 0)} days"
                    : "No current run";

                var prodDate = exp.ProdStartDate != null
                    ? $"{Math.Round((now - exp.ProdStartDate.Value).TotalDays, 0)} days"
                    : "No current run";

                Console.WriteLine($"\n{exp.Name} - {description}");
                Console.WriteLine($"    Dev:  {devDate}");
                Console.WriteLine($"    Prod: {prodDate}");
            }
        }
    }
}
