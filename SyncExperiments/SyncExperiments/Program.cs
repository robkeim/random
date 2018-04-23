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
        private const string EXPERIMENT_PREFIX = "HALO-";
        private static Dictionary<string, Experiment> _experiments = new Dictionary<string, Experiment>();

        public static void Main(string[] args)
        {
            Console.WriteLine("Fetching prod experiments...");
            ExtractExperiments(GetRunningExperiments(true), true);

            Console.WriteLine("Fetching dev experiments...");
            ExtractExperiments(GetRunningExperiments(false));

            Console.WriteLine("Fetching current run details..");
            HydrateCurrentRunDetails();

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

                if (name.ToUpperInvariant().StartsWith(EXPERIMENT_PREFIX))
                {
                    string description = obj[i].description;
                    var startDate = DateTimeOffset.Parse(obj[i].startDate.ToString());
                    
                    if (!_experiments.TryGetValue(name, out Experiment exp))
                    {
                        exp = new Experiment();
                    }

                    exp.Name = name;
                    exp.Description = description;

                    string id = obj[i].experimentId.ToString();

                    if (prod)
                    {
                        exp.Prod = new ExperimentDetails
                        {
                            StartDate = startDate,
                            Id = id
                        };
                    }
                    else
                    {
                        exp.Dev = new ExperimentDetails
                        {
                            StartDate = startDate,
                            Id = id
                        };
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

            return DownloadUrl(url);
        }

        private static void HydrateCurrentRunDetails()
        {
            foreach (var key in _experiments.Keys)
            {
                var exp = _experiments[key];

                if (exp.Prod != null)
                {
                    HydrateRun(true, exp.Prod);
                }

                if (exp.Dev != null)
                {
                    HydrateRun(false, exp.Dev);
                }
            }
        }

        private static void HydrateRun(bool isProduction, ExperimentDetails expDetails)
        {
            var json = GetExperimentDetails(isProduction, expDetails.Id);
            json = $"{{\"result\":{json}}}";
            dynamic obj = JObject.Parse(json);
            obj = obj.result;
            var array = (JArray) obj;
            obj = array.Single(el => el["status"].ToString() == "Active");

            expDetails.AllClusters = bool.Parse(obj["allClustersFlag"].ToString());
            expDetails.FlatB = (obj["integrationVariant"] ?? string.Empty).ToString() == "B";
            expDetails.TrafficRate = int.Parse(obj["trafficRate"].ToString());
            expDetails.InEvaluation = bool.Parse(obj["isEvaluationRun"].ToString());
        }

        private static string GetExperimentDetails(bool isProduction, string expId)
        {
            var url = isProduction
                ? $"http://calculon.agoda.local/api/api/experimentrun/getall/?experimentId={expId}"
                : $"http://dev.calculon.agoda.local/api/api/experimentrun/getall/?experimentId={expId}";

            return DownloadUrl(url);
        }

        private static string DownloadUrl(string url)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
            using (var httpResponse = (HttpWebResponse)httpWebRequest.GetResponseAsync().Result)
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                return streamReader.ReadToEnd();
            }
        }

        private static void PrintSummary()
        {
            var exps = _experiments.Values.OrderBy(e => e.Name).ToArray();

            // Unequal experiments
            var unequalExps = exps.Where(e => !Equals(e.Prod, e.Dev));
            exps = exps.Except(unequalExps).ToArray();

            // In development
            var expsInDevelopment = unequalExps.Where(e =>
                (e.Dev != null && e.Dev.OneHundredPercentAll)
                && (e.Prod == null || e.Prod.OneHundredPercentFlatBPrelive || e.Prod.ZeroPercentPrelive));
            unequalExps = unequalExps.Except(expsInDevelopment);
            
            // Experiments to integrate
            var expsToIntegrate = exps.Where(e => e.Prod != null
                && e.Prod.FlatBAll);
            exps = exps.Except(expsToIntegrate).ToArray();
            
            // Running experiments
            var runningExps = exps.Where(e => e.Prod != null && e.Prod.OneHundredPercentAll && !e.Prod.InEvaluation);
            exps = exps.Except(runningExps).ToArray();

            // Evaluation run
            var evaluationRunExps = exps.Where(e => e.Prod != null && e.Prod.OneHundredPercentAll && e.Prod.InEvaluation);
            exps = exps.Except(evaluationRunExps).ToArray();

            PrintExperiments(unequalExps, "Invalid configuration");
            PrintExperiments(exps, "Unknown configuration");
            PrintExperiments(expsInDevelopment, "In development");
            PrintExperiments(runningExps, "Running");
            PrintExperiments(evaluationRunExps, "Evaluation run");
            PrintExperiments(expsToIntegrate, "Awaiting integration");
        }

        private static bool Equals(ExperimentDetails exp1, ExperimentDetails exp2)
        {
            if (exp1 == null && exp2 == null)
            {
                return true;
            }

            if (exp1 == null || exp2 == null)
            {
                return false;
            }

            return exp1.AllClusters == exp2.AllClusters
                   && exp1.FlatB == exp2.FlatB
                   && exp1.TrafficRate == exp2.TrafficRate;
        }

        private static void PrintExperiments(IEnumerable<Experiment> exps, string title)
        {
            if (!exps.Any())
            {
                return;
            }

            Console.WriteLine($"\n=== {title.ToUpperInvariant()} ===");

            foreach (var exp in exps)
            {
                PrintExperiment(exp);
            }
        }

        private static void PrintExperiment(Experiment exp)
        {
            var description = exp.Description.Length > 100
                ? $"{exp.Description.Substring(0, 100)}..."
                : exp.Description;

            Console.WriteLine($"\n{exp.Name}: {description}");
            Console.WriteLine($"    Dev:  {FormatExperimentDetails(exp.Dev)}");
            Console.WriteLine($"    Prod: {FormatExperimentDetails(exp.Prod)}");
        }

        private static string FormatExperimentDetails(ExperimentDetails expDetails)
        {
            if (expDetails == null)
            {
                return "NO ACTIVE RUN";
            }

            var flatB = expDetails.FlatB ? "FlatB" : string.Empty;
            var servers = expDetails.AllClusters ? "All" : "Prelive";
            var days = Math.Round((DateTimeOffset.UtcNow - expDetails.StartDate).TotalDays, 0);

            return $"{days} days - {expDetails.TrafficRate}% {servers} {flatB}";
        }
    }
}
