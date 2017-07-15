using System;
using System.Configuration;
using System.Linq;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Diagnostics;

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
                    var classToReserve = FindClass(driver);

                    if (classToReserve != null)
                    {
                        ReserveClass(driver, classToReserve);
                        SendEmail();
                    }
                    else
                    {
                        SendEmail(noClassAvailable: true);
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

        private static IWebElement FindClass(ChromeDriver driver)
        {
            IReadOnlyCollection<IWebElement> elements = null;

            Func<bool> func = () =>
            {
                elements = driver.FindElementsByCssSelector("tr.classRow");

                return elements.Count != 0;
            };

            DriverExtensions.RetryUntilSuccess(func);

            var minutesRegex = new Regex(@"12:(\d{2})pm", RegexOptions.Compiled);
            
            var validClass = elements.FirstOrDefault(e => {
                var innerText = e.GetAttribute("innerText");

                if (!innerText.Contains("Pilates Reformer"))
                {
                    return false;
                }

                var match = minutesRegex.Match(innerText);

                if (!match.Success)
                {
                    return false;
                }

                var minutes = int.Parse(match.Groups[1].ToString());
                
                return minutes >= 15 && minutes <= 45;
            });

            return validClass;
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
