using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT40
{
    class Filter
    {
        public static History FilterDates(History history, DateTime? startDate, DateTime? endDate)
        {
            if (startDate != null)
            {
                history.Weeks = history.Weeks.Where(week => DateTime.Parse(week.Date) >= startDate).ToArray();
            }

            if (endDate != null)
            {
                history.Weeks = history.Weeks.Where(week => DateTime.Parse(week.Date) <= endDate).ToArray();
            }

            return history;
        }

        public static History FilterHighest(History history, int rating)
        {
            List<Week> weeks = new List<Week>();

            foreach (Week week in history.Weeks)
            {
                weeks.Add(new Week { Date = week.Date, Songs = week.Songs.Where(song => int.Parse(song.Rating) <= rating).ToArray() });
            }

            history.Weeks = weeks.ToArray();

            return history;
        }

        public static List<string> GetUniqueSongs(History history)
        {
            List<string> uniqueSongs = new List<string>();

            foreach (Week week in history.Weeks)
            {
                foreach (Song song in week.Songs)
                {
                    uniqueSongs.Add(string.Format("{0} - {1}", song.Artist, song.Title));
                }
            }

            uniqueSongs = uniqueSongs.Distinct().ToList();
            uniqueSongs.Sort();

            return uniqueSongs;
        }
    }
}
