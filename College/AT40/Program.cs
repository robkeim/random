using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Xml;
using System.Threading;
using System.Threading.Tasks;

namespace AT40
{
    class Program
    {
        static void Main()
        {
            History history = FetchData.GetHistory();

            history = Filter.FilterDates(history, new DateTime(2006, 9, 1), new DateTime(2010, 4, 30));
            history = Filter.FilterHighest(history, 10);

            List<string> songs = Filter.GetUniqueSongs(history);

            using (TextWriter textWriter = new StreamWriter(@"C:\Users\rkeim\Desktop\top10college.txt"))
            {
                foreach (var song in songs)
                {
                    textWriter.WriteLine(song);
                }
            }

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        /*public static void GetNumberOneHits()
        {
            using (FileStream fileStream = new FileStream(DataFile, FileMode.Open))
            {
                using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas()))
                {
                    DataContractSerializer dcs = new DataContractSerializer(typeof(History));
                    History history = (History)dcs.ReadObject(reader, true);

                    Dictionary<string, int> uniqueSongs = new Dictionary<string, int>();

                    foreach (Week week in history.Weeks)
                    {
                        foreach (Song song in week.Songs)
                        {
                            DateTime foo = DateTime.Parse(week.Date);

                            if (song.Rating != "1")
                            {
                                continue;
                            }

                            string name = string.Format("{0} - {1}", song.Artist, song.Title);

                            if (!uniqueSongs.ContainsKey(name))
                            {
                                uniqueSongs[name] = 0;
                            }
                        }
                    }

                    List<string> uniqueSongsSorted = uniqueSongs.Keys.ToList();
                    uniqueSongsSorted.Sort();

                    using (TextWriter textWriter = new StreamWriter(@"C:\Users\rkeim\Desktop\numberOneHits.txt"))
                    {
                        foreach (var song in uniqueSongsSorted)
                        {
                            textWriter.WriteLine(song);
                        }
                    }
                }
            }
        }

        public static void GetUniqueSongs()
        {
            using (FileStream fileStream = new FileStream(DataFile, FileMode.Open))
            {
                using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas()))
                {
                    DataContractSerializer dcs = new DataContractSerializer(typeof(History));
                    History history = (History)dcs.ReadObject(reader, true);

                    Dictionary<string, int> uniqueSongs = new Dictionary<string,int>();

                    foreach (Week week in history.Weeks)
                    {
                        foreach (Song song in week.Songs)
                        {
                            string name = string.Format("{0} - {1}", song.Artist, song.Title);

                            if (!uniqueSongs.ContainsKey(name))
                            {
                                uniqueSongs[name] = 0;
                            }
                        }
                    }

                    List<string> uniqueSongsSorted = uniqueSongs.Keys.ToList();
                    uniqueSongsSorted.Sort();

                    using (TextWriter textWriter = new StreamWriter(@"C:\Users\rkeim\Desktop\at40songs.txt"))
                    {
                        foreach (var song in uniqueSongsSorted)
                        {
                            textWriter.WriteLine(song);
                        }
                    }
                }
            }
        }*/


        

        









        /*
        public static void GetWeeksForMonth(string date)
        {
            List<string> weeks = Web.GetWeeksForMonth(date);

            lock (lockObject)
            {
                allWeeks.AddRange(weeks);
            }
        }

        public static void GetSongsForWeek(string id)
        {
            List<Song> songs = Web.GetSongsForWeek(id);

            lock (lockObject)
            {
                allSongs.AddRange(songs);
            }
        }*/
    }
}
/*List<Week> weekData = new List<Week>();



            foreach (string date in dates)
            {
                weekData.AddRange(Web.GetValuesForMonth(date));
            }

            History history = new History { Weeks = weekData.ToArray() };

            using (FileStream fileStream = new FileStream(@"C:\Users\rkeim\Desktop\final.txt", FileMode.Create))
            {
                DataContractSerializer dcs = new DataContractSerializer(typeof(History));
                dcs.WriteObject(fileStream, history);
            }*/
/*List<Week> weekData = new List<Week>();

List<string> years = Date.GetValidDates();

foreach (string year in years)
{
    weekData.AddRange(Web.GetValuesForMonth(year));
}

History history = new History { Weeks = weekData.ToArray() };

using (FileStream fileStream = new FileStream(@"C:\Users\rkeim\Desktop\final.txt", FileMode.Create))
{
    DataContractSerializer dcs = new DataContractSerializer(typeof(History));
    dcs.WriteObject(fileStream, history);
}*/

/* using (FileStream fileStream = new FileStream(@"C:\Users\rkeim\Desktop\at40.xml", FileMode.Open))
 {
     using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas()))
     {
         DataContractSerializer dcs = new DataContractSerializer(typeof(History));
         History history = (History)dcs.ReadObject(reader, true);

         HashSet<string> uniqueSongs = new HashSet<string>();
         HashSet<string> uniqueArtists = new HashSet<string>();

         Dictionary<string, int> numSongsPerArtist = new Dictionary<string, int>();

         foreach (Week week in history.Weeks)
         {
             foreach (Song song in week.Songs)
             {
                 string name = string.Format("{0}-{1}", song.Artist, song.Title);

                 / *if (!uniqueSongs.Contains(name))
                 {
                     Console.WriteLine(name);
                 }* /

                 / *if (!uniqueArtists.Contains(song.Artist))
                 {
                     Console.WriteLine(song.Artist);
                 }* /

                 if (!numSongsPerArtist.ContainsKey(name))
                 {
                     numSongsPerArtist[name] = 0;
                 }
                 numSongsPerArtist[name]++;

                 uniqueSongs.Add(name);
                 uniqueArtists.Add(song.Artist);
             }
         }

         if (uniqueSongs.Contains("foo"))
         {
             /// TODO (rkeim):
         }

         foreach (var key in numSongsPerArtist.Keys)
         {
             Console.WriteLine(string.Format("{0} - {1}", numSongsPerArtist[key], key));
         }
     }
 }*/

/*using (StreamWriter output = new StreamWriter(@"C:\Users\rkeim\Desktop\weeks.csv"))
{
    using (FileStream fileStream = new FileStream(@"C:\Users\rkeim\Desktop\at40.xml", FileMode.Open))
    {
        using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas()))
        {
            DataContractSerializer dcs = new DataContractSerializer(typeof(History));
            History history = (History)dcs.ReadObject(reader, true);

            Dictionary<string, Dictionary<string, string>> songToWeekMapping = new Dictionary<string, Dictionary<string, string>>();

            foreach (Week week in history.Weeks)
            {
                foreach (Song song in week.Songs)
                {
                    string name = song.Title + "-" + song.Artist;

                    if (!songToWeekMapping.ContainsKey(name))
                    {
                        songToWeekMapping[name] = new Dictionary<string, string>();
                    }

                    songToWeekMapping[name][week.Date] = song.Rating;
                }
            }

            output.Write("~ ");
            foreach (Week week in history.Weeks)
            {
                output.Write(string.Format("{0}~ ", week.Date));
            }
            output.WriteLine();

            foreach (string song in songToWeekMapping.Keys)
            {
                output.Write(string.Format("{0}~ ", song));
                foreach (Week week in history.Weeks)
                {
                    string weekRating = songToWeekMapping[song].ContainsKey(week.Date) ? songToWeekMapping[song][week.Date] : string.Empty;
                    output.Write(string.Format("{0}~ ", weekRating));
                }
                output.WriteLine();
            }
        }
    }
}*/
