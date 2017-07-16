using System;
using System.Configuration;
using System.Linq;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Globalization;

namespace ReserveGymClasses
{
    public class Program
    {
        public static EmailStatus EmailStatus = EmailStatus.Enabled;

        public static void Main(string[] args)
        {
            try
            {
                using (var driver = new ChromeDriver())
                {
                    var username = ConfigurationManager.AppSettings["Username"];
                    var password = ConfigurationManager.AppSettings["Password"];

                    driver.GoToUrlAndWaitForPageLoad("https://mylocker.virginactive.co.th/#/bookaclass");
                    driver.Login(username: username, usernameSelector: "#memberID",
                        password: password, passwordSelector: "#password", submitButtonSelector: "form[name='loginForm'] button");

                    var dayToSelect = DateTime.Now.Day + 6;

                    SelectDay(driver, dayToSelect);
                    var classesToReserve = FindClases(driver);

                    if (classesToReserve == null || !classesToReserve.Any())
                    {
                        SendEmail(noClassAvailable: true);
                    }
                    else
                    {
                        // TODO rkeim: emails should be consolidated to 1x/day
                        foreach (var classtoReserve in classesToReserve)
                        {
                            ReserveClass(driver, classtoReserve);
                            SendEmail();
                        }
                    }

                    Console.WriteLine("done!");

                    if (Debugger.IsAttached)
                    {
                        Console.ReadLine();
                    }
                }
            }
            catch (Exception e)
            {
                SendEmail(success: false, exception: e);
            }
        }

        private static void SelectDay(ChromeDriver driver, int day)
        {
            // Best approach here is to propably walk through all of the found elements until I get the element I need
            IReadOnlyCollection<IWebElement> elements = null;

            Func<bool> func = () =>
            {
                elements = driver.FindElementsByCssSelector("span.day");

                return elements.Count != 0;
            };

            DriverExtensions.RetryUntilSuccess(func);

            var element = elements.Single(e => e.Text == day.ToString());
            element = element.GetParent();
            element.Click();
            driver.WaitForPageLoad();
        }

        private static IWebElement[] FindClases(ChromeDriver driver)
        {
            IReadOnlyCollection<IWebElement> elements = null;

            Func<bool> func = () =>
            {
                elements = driver.FindElementsByCssSelector("tr.classRow");

                return elements.Count != 0;
            };

            DriverExtensions.RetryUntilSuccess(func);

            var timeRegex = new Regex(@"(\d\d?:\d{2}[ap]m)", RegexOptions.Compiled);
            
            var validClass = elements.Where(e => {
                var innerText = e.GetAttribute("innerText");

                if (!innerText.Contains("Pilates Reformer"))
                {
                    return false;
                }

                var match = timeRegex.Match(innerText);

                if (!match.Success)
                {
                    throw new FormatException($"Unable to find time in: {innerText}");
                }
                
                var time = ParseTimeForCurrentDay(match.Groups[1].ToString());

                var minLunch = ParseTimeForCurrentDay("12:15pm");
                var maxLunch = ParseTimeForCurrentDay("1:00pm");
                var minEvening = ParseTimeForCurrentDay("6:30pm");

                return (minLunch <= time && time <= maxLunch) // During lunch
                        || time >= minEvening; // In the evening
            });

            return validClass.ToArray();
        }

        private static DateTimeOffset ParseTimeForCurrentDay(string input)
        {
            return DateTimeOffset.ParseExact(input, "h:mmtt", CultureInfo.CurrentCulture);
        }

        private static void ReserveClass(ChromeDriver driver, IWebElement classToReserve)
        {
            classToReserve.Click();

            var tmp = classToReserve.FindElement(By.XPath(@"./following-sibling::*"));
            var linkToPopup = tmp.FindElement(By.CssSelector("a"));
            linkToPopup.Click();

            tmp = driver.FindElementByCssSelector("div.modal-dialog");
            var book = tmp.FindElement(By.CssSelector("button[ng-click='vm.makeBooking()']"));

            DriverExtensions.RetryUntilSuccess(() => book.Click());
        }

        private static void SendEmail(bool success = true, bool noClassAvailable = false, Exception exception = null)
        {
            var today = DateTime.Now.AddDays(6).ToString("MMM d");
            var subject = $"[VirginActive][{today}] ";

            if (noClassAvailable)
            {
                subject += "No classes available";
            }
            else if (success)
            {
                subject += "Success";
            }
            else
            {
                subject += "Error";
            }

            Email.SendEmail("robkeim@gmail.com", subject, exception?.ToString());
        }
    }
}
