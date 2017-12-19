using System;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace SyncExperiments
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Prod:");
            var json = GetRunningExperiments(true);
            ExtractExperimentNames(json, true);

            Console.WriteLine("\nDev:");
            json = GetRunningExperiments(false);
            ExtractExperimentNames(json);

            Console.WriteLine("\ndone!");
            Console.ReadLine();
        }

        private static void ExtractExperimentNames(string json, bool prod = false)
        {
            dynamic obj = JObject.Parse(json);
            obj = obj.result;

            for (int i = 0; i < obj.Count; i++)
            {
                string experimentName = obj[i].experimentName;

                if (experimentName.ToUpperInvariant().StartsWith("HALO-"))
                {
                    TimeSpan length = DateTime.Now - DateTime.Parse(obj[i].startDate.ToString());

                    Console.WriteLine($"{experimentName} - {Math.Floor(length.TotalDays)} days - {obj[i].description}");
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
    }
}
