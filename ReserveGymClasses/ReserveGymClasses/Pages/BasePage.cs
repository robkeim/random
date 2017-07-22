using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ReserveGymClasses.Pages
{
    public class BasePage
    {
        protected readonly ChromeDriver _driver;
        protected const string rootDir = "https://mylocker.virginactive.co.th";

        public BasePage(ChromeDriver driver)
        {
            _driver = driver;
        }

        public void ChangeLanguageToEnglish()
        {
            var element = _driver.FindElement(By.CssSelector("header a.language"));

            if (element.GetAttribute("class").Contains("english"))
            {
                element.Click();
            }
            else
            {
                Logger.Log("Page already in English");
            }
        }
    }
}
