using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT40
{
    public static class Date
    {
        private const int INITIAL_YEAR = 2001;
        private const int INITIAL_MONTH = 8;

        public static List<string> GetValidDates()
        {
            List<string> results = new List<string>();

            int year = INITIAL_YEAR;
            int month = INITIAL_MONTH;

            int curYear = DateTime.UtcNow.Year;
            int curMonth = DateTime.UtcNow.Month;

            while (year <= curYear)
            {
                if (year == curYear && month > curMonth)
                {
                    break;
                }

                results.Add(string.Format("{0:D4}/{1:D2}", year, month));

                month++;

                if (month > 12)
                {
                    month = 1;
                    year++;
                }
            }

            return results;
        }
    }
}
