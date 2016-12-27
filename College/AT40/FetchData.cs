using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;

namespace AT40
{
    class FetchData
    {
        private static string DataFile = @"C:\Users\rkeim\Desktop\at40data.xml";
        private static List<Week> allWeeks = new List<Week>();
        private static Object lockObject = new Object();

        public static History GetHistory()
        {
            EnsureData();

            using (FileStream fileStream = new FileStream(DataFile, FileMode.Open))
            {
                using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas()))
                {
                    DataContractSerializer dcs = new DataContractSerializer(typeof(History));
                    return (History)dcs.ReadObject(reader, true);
                }
            }
        }

        private static void EnsureData()
        {
            // Don't refetch the data if it already exists
            if (File.Exists(DataFile))
            {
                return;
            }

            List<string> dates = Date.GetValidDates();

            // TODO rkeim remove
            //dates = dates.Take(10).ToList();

            Parallel.ForEach(dates, date => GetWeekDataForMonth(date));
            allWeeks.Sort();

            History history = new History { Weeks = allWeeks.ToArray() };

            using (FileStream fileStream = new FileStream(DataFile, FileMode.Create))
            {
                DataContractSerializer dcs = new DataContractSerializer(typeof(History));
                dcs.WriteObject(fileStream, history);
            }
        }

        private static void GetWeekDataForMonth(string date)
        {
            List<Week> weeks = Web.GetValuesForMonth(date);

            lock (lockObject)
            {
                allWeeks.AddRange(weeks);
            }
        }
    }
}
