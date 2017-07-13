using OpenQA.Selenium;

namespace ReserveGymClasses
{
    public static class WebElementExtensions
    {
        public static IWebElement GetParent(this IWebElement element)
        {
            return element.FindElement(By.XPath("./.."));
        }
    }
}
