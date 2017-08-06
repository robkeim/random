using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;

namespace ReserveGymClasses
{
    public class ScreenshotManager : IDisposable
    {
        private readonly ChromeDriver _driver;
        private readonly string screenshotFormat = DateTimeOffset.Now.ToString("yyyy-MM-dd-HHmm");
        private readonly string screenshotOutputDirectory = Path.Combine(Path.GetTempPath(), "ReserveGymClasses");
        private int screenshotNumber = 0;

        public ScreenshotManager(ChromeDriver driver)
        {
            _driver = driver;
        }

        public void TakeScreenshot()
        {
            if (!Directory.Exists(screenshotOutputDirectory))
            {
                Directory.CreateDirectory(screenshotOutputDirectory);
            }

            // De-zoom to show the whole page
            _driver.ExecuteScript("document.body.style.zoom = document.body.style.zoom=top.window.screen.height / document.body.scrollHeight;");

            var screenshot = _driver.GetScreenshot();
            screenshot.SaveAsFile(Path.Combine(screenshotOutputDirectory, $"{screenshotFormat}_{screenshotNumber++}.png"), ScreenshotImageFormat.Png);

            _driver.ExecuteScript("document.body.style.zoom = 1;");
        }

        public string[] GetScreenshotPaths()
        {
            var results = new List<string>(screenshotNumber);

            for (var i = 0; i < screenshotNumber; i++)
            {
                results.Add(Path.Combine(screenshotOutputDirectory, $"{screenshotFormat}_{i}.png"));
            }

            return results.ToArray();
        }

        public void Dispose()
        {
            Directory.Delete(screenshotOutputDirectory, recursive: true);
        }
    }
}
