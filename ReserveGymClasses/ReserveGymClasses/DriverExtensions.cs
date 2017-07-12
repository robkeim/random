using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;

namespace ReserveGymClasses
{
    public static class DriverExtensions
    {
        private static readonly TimeSpan defaultTimeout = TimeSpan.FromSeconds(20);

        public static void GoToUrlAndWaitForPageLoad(this ChromeDriver driver, string url)
        {
            driver.Url = url;
            driver.WaitForPageLoad();
        }

        public static void Login(this ChromeDriver driver, string username, string usernameSelector,
            string password, string passwordSelector, string submitButtonSelector)
        {
            var usernameField = driver.FindElementByCssSelector(usernameSelector);
            usernameField.SendKeys(username);

            var passwordField = driver.FindElementByCssSelector(passwordSelector);
            passwordField.SendKeys(password);

            var submitButton = driver.FindElementByCssSelector(submitButtonSelector);

            // The splash screen shows up and goes away automatically so wait for it to be gone
            RetryAction(() => submitButton.Click(), defaultTimeout);

            driver.WaitForPageLoad();
        }

        private static void WaitForPageLoad(this ChromeDriver driver)
        {
            var wait = new WebDriverWait(driver, defaultTimeout);
            if (!wait.Until(d => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete")))
            {
                throw new TimeoutException($"Page did not complete loading");
            }
        }

        private static void RetryAction(Action action, TimeSpan timeout)
        {
            var stopwatch = Stopwatch.StartNew();
            Exception exception;

            do
            {
                try
                {
                    action();
                    return;
                }
                catch (Exception e)
                {
                    // Keep only the last exception
                    exception = e;
                }
            }
            while (stopwatch.ElapsedMilliseconds < timeout.TotalMilliseconds);

            throw exception;
        }
    }
}
