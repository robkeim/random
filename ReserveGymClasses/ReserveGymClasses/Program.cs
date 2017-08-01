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
            try
            {
                // The program is going to run hourly but this will prevent me from getting spammed every hour
                if (!Debugger.IsAttached && DateTime.Now.Hour == 22)
                {
                    EmailStatus = EmailStatus.Enabled;
                }

                var options = new ChromeOptions();
                options.AddArgument("--start-maximized");

                using (var driver = new ChromeDriver(options))
                {
                    var username = ConfigurationManager.AppSettings["Username"];
                    var password = ConfigurationManager.AppSettings["Password"];

                    driver.GoToUrlAndWaitForPageLoad("https://mylocker.virginactive.co.th/#/bookaclass");
                    driver.Login(username: username, usernameSelector: "#memberID",
                        password: password, passwordSelector: "#password", submitButtonSelector: "form[name='loginForm'] button");

                    var myBookingsPage = new MyBookingsPage(driver);
                    myBookingsPage.ChangeLanguageToEnglish();
                    var bookedDays = myBookingsPage.GetBookedDays();

                    var bookAClassPage = new BookAClassPage(driver);
                    bookAClassPage.ChangeLanguageToEnglish();
                    bookAClassPage.BookClasses(bookedDays);

                    SendEmail();

                    Console.WriteLine("done!");

                    if (Debugger.IsAttached)
                    {
                        Console.ReadLine();
                    }
                }
            }
            catch (Exception e) when (!Debugger.IsAttached)
            {
                SendEmail(success: false, exception: e);
            }
        }

        private static void SendEmail(bool success = true, Exception exception = null)
        {
            var today = DateTime.Now.AddDays(6).ToString("MMM d");
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

            Email.SendEmail("robkeim@gmail.com", subject, body);
        }
    }
}
