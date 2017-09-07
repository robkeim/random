using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

namespace WhoDidIt
{
    public static class ExperimentParser
    {
        public static string[] GetBExperimentsForUserId(string userId)
        {
            var rawResponse = SendRequest(userId);

            var deserializedResponse = JsonConvert.DeserializeObject<Response>(rawResponse);

            return deserializedResponse.Result
                .Where(r => r.Variant == 'B')
                .Select(r => r.ExpName)
                .OrderBy(r => r)
                .ToArray();
        }

        private static string SendRequest(string userId)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://expapi.agoda.local/rest/experiment/seen/user03");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStreamAsync().Result))
            {
                string json = $"{{\"user03\":\"{userId}\"}}";

                streamWriter.Write(json);
                streamWriter.Flush();
            }

            using (var httpResponse = (HttpWebResponse)httpWebRequest.GetResponseAsync().Result)
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                return streamReader.ReadToEnd();
            }
        }

        public class Response
        {
            public List<Result> Result { get; set; }
        }

        public class Result
        {
            public string ExpName { get; set; }

            public char Variant { get; set; }
        }
    }
}
