using System;
using System.Configuration;
using System.Linq;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace ReserveGymClasses
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var driver = new ChromeDriver())
            {
                var username = ConfigurationManager.AppSettings["Username"];
                var password = ConfigurationManager.AppSettings["Password"];

                driver.GoToUrlAndWaitForPageLoad("https://mylocker.virginactive.co.th/#/bookaclass");
                driver.Login(username: username, usernameSelector: "#memberID",
                    password: password, passwordSelector: "#password", submitButtonSelector: "form[name='loginForm'] button");

                var dayToSelect = DateTime.Now.Day + 1;

                SelectDay(driver, dayToSelect);

                Console.WriteLine("done!");
                Console.ReadLine();
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
    }
}
