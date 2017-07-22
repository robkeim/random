using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ReserveGymClasses.Pages
{
    public class MyBookingsPage : BasePage
    {
        private string url = $"{rootDir}/#/mybookings";

        public MyBookingsPage(ChromeDriver driver)
            : base(driver)
        {
        }

        public int[] GetBookedDays()
        {
            _driver.GoToUrlAndWaitForPageLoad(url);

            IWebElement table = null;

            DriverExtensions.RetryUntilSuccess(() => { table = _driver.FindElementsByTagName("table").Single(e => e.Displayed); });

            var rows = table.FindElements(By.CssSelector("tr"));

            var results = new List<int>(rows.Count);

            var regex = new Regex(@"(\d+)", RegexOptions.Compiled);

            foreach (var row in rows)
            {
                var day = row.FindElements(By.CssSelector("td")).FirstOrDefault()?.GetAttribute("innerText");

                if (day == null)
                {
                    continue;
                }

                var match = regex.Match(day);

                if (!match.Success)
                {
                    continue;
                }

                results.Add(int.Parse(match.Groups[1].ToString()));
            }

            return results.ToArray();
        }
    }
}
