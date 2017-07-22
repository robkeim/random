using OpenQA.Selenium;

namespace ReserveGymClasses
{
    public class ClassToBook
    {
        public ClassToBook(IWebElement element, bool isBooked, string time, bool isInTimeRange)
        {
            Element = element;
            IsBooked = isBooked;
            Time = time;
            IsInTimeRange = isInTimeRange;
        }

        public IWebElement Element { get; }

        public bool IsBooked { get; }

        public string Time { get; }

        public bool IsInTimeRange { get; }
    }
}
