using OpenQA.Selenium.Chrome;
using ReserveGymClasses.Pages;
using System;
using System.Configuration;
using System.Diagnostics;

namespace ReserveGymClasses
{
    public class Program
    {
        public static EmailStatus EmailStatus = EmailStatus.Disabled;

        public static void Main(string[] args)
        {
            ScreenshotManager screenshotManager = null;

            try
            {
                // The program is going to run hourly but this will prevent me from getting spammed every hour
                if (!Debugger.IsAttached && DateTime.Now.Hour == 22)
                {
                    EmailStatus = EmailStatus.Enabled;
                }

                var options = new ChromeOptions();
                options.AddArgument("--start-maximized");
                options.AddArgument("disable-infobars"); // Disable the bar saying Chrome is being controlled by automation software

                using (var driver = new ChromeDriver(options))
                {
                    screenshotManager = new ScreenshotManager(driver);

                    var username = ConfigurationManager.AppSettings["Username"];
                    var password = ConfigurationManager.AppSettings["Password"];

                    driver.GoToUrlAndWaitForPageLoad("https://mylocker.virginactive.co.th/#/bookaclass");
                    driver.Login(username: username, usernameSelector: "#memberID",
                        password: password, passwordSelector: "#password", submitButtonSelector: "form[name='loginForm'] button");

                    var myBookingsPage = new MyBookingsPage(driver, screenshotManager);
                    myBookingsPage.ChangeLanguageToEnglish();
                    var bookedDays = myBookingsPage.GetBookedDays();

                    // The maximum number of "hot" classes you can book at any time is three
                    // so there's no need to try to reserve as the reservations will fail
                    if (bookedDays.Length >= 3)
                    {
                        Logger.Log("Maximum number of bookings reached, can't book additional classes");
                    }
                    else
                    {
                        var bookAClassPage = new BookAClassPage(driver, screenshotManager);
                        bookAClassPage.ChangeLanguageToEnglish();
                        bookAClassPage.BookClasses(bookedDays);
                    }

                    SendEmail(screenshotManager.GetScreenshotPaths());

                    Console.WriteLine("done!");

                    if (Debugger.IsAttached)
                    {
                        Console.ReadLine();
                    }
                }
            }
            catch (Exception e) when (!Debugger.IsAttached)
            {
                // Always send an email when there's an exception
                EmailStatus = EmailStatus.Enabled;
                SendEmail(screenshotManager?.GetScreenshotPaths(), success: false, exception: e);
            }
            finally
            {
                screenshotManager?.Dispose();
            }
        }

        private static void SendEmail(string[] screenshotPaths, bool success = true, Exception exception = null)
        {
            var today = DateTimeOffset.Now.ToString("MMM d");
            var subject = $"[VirginActive][{today}] ";

            if (success)
            {
                subject += "Success";
            }
            else
            {
                subject += "Error";
            }

            var body = $"{Logger.GetLogMessages()}\n{exception?.ToString()}";

            Email.SendEmail("robkeim@gmail.com", subject, body, screenshotPaths);
        }
    }
}
