using System;
using System.Configuration;
using OpenQA.Selenium.Chrome;

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

                Console.WriteLine("done!");
                Console.ReadLine();
            }
        }
    }
}
