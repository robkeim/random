using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace AT40
{
    public static class Web
    {
        public static List<string> GetWeeksForMonth(string date)
        {
            List<string> weeks = new List<string>();

            string url = "http://www.at40.com/top-40/" + date;
            string response = GetResponseForUrl(url);

            Regex regex = new Regex(@"<a href=""/top-40/chart/(\d+)"">(.*?)</a>");
            MatchCollection matches = regex.Matches(response);

            foreach (Match match in matches)
            {
                weeks.Add(match.Groups[1].Value);
            }

            return weeks;
        }

        public static List<Week> GetValuesForMonth(string date)
        {
            List<Week> results = new List<Week>();

            string url = "http://www.at40.com/top-40/" + date;
            string response = GetResponseForUrl(url);

            Regex regex = new Regex(@"<a href=""/top-40/chart/(\d+)"">(.*?)</a>");
            MatchCollection matches = regex.Matches(response);

            foreach (Match match in matches)
            {
                Console.WriteLine(match.Groups[1].Value + " - " + match.Groups[2].Value);

                Week week = new Week();
                week.Date = DateTime.Parse(match.Groups[2].Value).ToString("yyyy-MM-dd");
                week.Songs = GetSongsForWeek(match.Groups[1].Value).ToArray();
                results.Add(week);
            }

            return results;
        }

        public static List<Song> GetSongsForWeek(string id)
        {
            List<Song> results = new List<Song>();

            string url = "http://www.at40.com/top-40/chart/" + id;
            string response = GetResponseForUrl(url);

            Regex regex = new Regex(@"<td.*?>(\d+)</td>.*?<a href='/artist.*?>(.*?)</a>.*?<br />(.*?)</td>", RegexOptions.Singleline);
            MatchCollection matches = regex.Matches(response);

            foreach (Match match in matches)
            {
                Song song = new Song
                {
                    Rating = match.Groups[1].Value,
                    Title = match.Groups[3].Value.Trim(),
                    Artist = match.Groups[2].Value.Trim()
                };

                results.Add(song);
            }

            return results;
        }

        private static string GetResponseForUrl(string url)
        {
            using (StreamReader streamReader = new StreamReader(WebRequest.Create(url).GetResponse().GetResponseStream()))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
