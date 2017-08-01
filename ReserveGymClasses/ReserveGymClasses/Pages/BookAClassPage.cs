using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace ReserveGymClasses.Pages
{
    public class BookAClassPage : BasePage
    {
        private string url = $"{rootDir}/#/bookaclass";

        public BookAClassPage(ChromeDriver driver)
            : base(driver)
        {
        }

        public void BookClasses(int[] alreadyReservedDays)
        {
            _driver.GoToUrlAndWaitForPageLoad(url);

            var now = DateTimeOffset.UtcNow;

            //  Reservations can be made starting at 10PM for a week in advance
            var maxDaysInFuture = DateTimeOffset.Now.Hour >= 22 ? 8 : 7;

            var daysToReserve = Enumerable.Range(1, maxDaysInFuture)
                .Select(i => now.AddDays(i))
                .Where(i => !alreadyReservedDays.Contains(i.Day))
                .ToArray();

            foreach (var dayToReserve in daysToReserve)
            {
                BookClassesForDay(dayToReserve);
            }
        }

        private void BookClassesForDay(DateTimeOffset day)
        {
            SelectDay(day.Day);
            var classesToBook = FindClasses(IsWeekendDay(day.DayOfWeek));

            Logger.Log($"\n{day.Day}:");

            foreach (var classToBook in classesToBook)
            {
                if (!classToBook.IsInTimeRange)
                {
                    Logger.Log($"\t{classToBook.Time}: not valid time");
                }
                else if (classToBook.IsBooked)
                {
                    Logger.Log($"\t{classToBook.Time}: fully booked");
                }
                else
                {
                    Logger.Log($"\t{classToBook.Time}: ready to book!");
                    ReserveClass(classToBook.Element);
                }
            }
        }

        private static bool IsWeekendDay(DayOfWeek dayOfWeek)
        {
            return dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday;
        }

        private void SelectDay(int day)
        {
            // Best approach here is to propably walk through all of the found elements until I get the element I need
            IReadOnlyCollection<IWebElement> elements = null;

            Func<bool> func = () =>
            {
                elements = _driver.FindElementsByCssSelector("span.day");

                return elements.Count != 0;
            };

            DriverExtensions.RetryUntilSuccess(func);

            var element = elements.Single(e => e.Text == day.ToString());
            element = element.GetParent();
            element.Click();
            _driver.WaitForPageLoad();
            _driver.TakeScreenshot();
        }

        private ClassToBook[] FindClasses(bool isWeekendDay)
        {
            IReadOnlyCollection<IWebElement> elements = null;

            Func<bool> func = () =>
            {
                elements = _driver.FindElementsByCssSelector("tr.classRow");

                return elements.Count > 1;
            };

            DriverExtensions.RetryUntilSuccess(func);

            var timeRegex = new Regex(@"(\d\d?:\d{2}[ap]m)", RegexOptions.Compiled);

            var validClasses = elements.Select(e => {
                var innerText = e.GetAttribute("innerText");

                if (!innerText.Contains("Pilates Reformer"))
                {
                    return null;
                }

                var match = timeRegex.Match(innerText);

                if (!match.Success)
                {
                    throw new FormatException($"Unable to find time in: {innerText}");
                }

                var foo = match.Groups[1].ToString();

                var time = ParseTimeForCurrentDay(match.Groups[1].ToString());
                bool isValidTime;

                if (isWeekendDay)
                {
                    var minTime = ParseTimeForCurrentDay("11:00am");

                    isValidTime = time >= minTime;
                }
                else
                {
                    var minLunch = ParseTimeForCurrentDay("12:15pm");
                    var maxLunch = ParseTimeForCurrentDay("1:00pm");
                    var minEvening = ParseTimeForCurrentDay("6:45pm");
                    var maxEvening = ParseTimeForCurrentDay("7:30pm");

                    isValidTime = (minLunch <= time && time <= maxLunch) // During lunch
                                        || (time >= minEvening && time <= maxEvening); // In the evening
                }

                if (!isValidTime)
                {
                    return new ClassToBook(null, isBooked: false, time: match.Groups[1].ToString(), isInTimeRange: false);
                }

                var cssClasses = e.GetAttribute("class"); // Ignore it if it contains rowfull

                if (cssClasses.Contains("rowFull"))
                {
                    return new ClassToBook(null, isBooked: true, time: match.Groups[1].ToString(), isInTimeRange: true);
                }

                return new ClassToBook(e, isBooked: false, time: match.Groups[1].ToString(), isInTimeRange: true);
            }).Where(e => e != null)
            .ToArray();

            return validClasses;
        }

        private static DateTimeOffset ParseTimeForCurrentDay(string input)
        {
            return DateTimeOffset.ParseExact(input, "h:mmtt", CultureInfo.CurrentCulture);
        }

        private void ReserveClass(IWebElement classToReserve)
        {
            classToReserve.Click();

            var tmp = classToReserve.FindElement(By.XPath(@"./following-sibling::*"));
            var linkToPopup = tmp.FindElement(By.CssSelector("a"));
            linkToPopup.Click();

            tmp = _driver.FindElementByCssSelector("div.modal-dialog");
            var book = tmp.FindElement(By.CssSelector("button[ng-click='vm.makeBooking()']"));

            DriverExtensions.RetryUntilSuccess(() => book.Click());
            // TODO rkeim: need to dismiss the dialog here
        }
    }
}
