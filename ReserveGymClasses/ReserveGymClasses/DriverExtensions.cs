using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

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
            RetryUntilSuccess(() => submitButton.Click());

            driver.WaitForPageLoad();
        }
        
        public static void RetryUntilSuccess(Action action)
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
            while (stopwatch.ElapsedMilliseconds < defaultTimeout.TotalMilliseconds);

            throw exception;
        }

        public static void RetryUntilSuccess(Func<bool> func)
        {
            var stopwatch = Stopwatch.StartNew();
            bool result;

            do
            {
                result = func();
            }
            while (!result && stopwatch.ElapsedMilliseconds < defaultTimeout.TotalMilliseconds);

            if (!result)
            {
                throw new TimeoutException("Couldn't complete action in required time");
            }
        }

        public static void WaitForPageLoad(this ChromeDriver driver)
        {
            var wait = new WebDriverWait(driver, defaultTimeout);
            // Wait for DOM
            if (!wait.Until(d => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete")))
            {
                throw new TimeoutException($"Page did not complete loading");
            }

            // Wait for JQuery
            if (!wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return (window.jQuery != null) && (jQuery.active === 0)").Equals(true)))
            {
                throw new TimeoutException($"JQuery did not complete loading");
            }

            // Wait for Ajax calls
            if (!wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return $.active == 0")).Equals(true))
            {
                throw new TimeoutException($"Ajax calls did not complete");
            }

            // Wait for Angular (inspired from here: https://stackoverflow.com/a/41617476)
            if (!wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return (window.angular !== undefined) && (angular.element(document.body).injector() !== undefined) && (angular.element(document.body).injector().get('$http').pendingRequests.length === 0)").Equals(true)))
            {
                throw new TimeoutException($"Angular did not complete");
            }
        }

        // TODO rkeim: move screenshot code to a separate class
        private static int screenshotNumber = 0;
        private static string screenshotFormat = DateTimeOffset.Now.ToString("yyyy-MM-dd-HHmm");
        public static void TakeScreenshot(this ChromeDriver driver)
        {
            if (!Directory.Exists(@".\screenshots"))
            {
                Directory.CreateDirectory(@".\screenshots");
            }

            // Scroll the the bottom of the scrollable header minus the status header
            driver.ExecuteScript("$(window).scrollTop($('.vaPageHeader')[0].scrollHeight - $('header').height())");

            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile($@".\screenshots\{screenshotFormat}_{screenshotNumber++}.png", ScreenshotImageFormat.Png);
        }

        public static string[] GetScreenshotPaths()
        {
            var results = new List<string>(screenshotNumber);

            for (var i = 0; i < screenshotNumber; i++)
            {
                results.Add($@".\screenshots\{screenshotFormat}_{i}.png");
            }

            return results.ToArray();
        }
    }
}
