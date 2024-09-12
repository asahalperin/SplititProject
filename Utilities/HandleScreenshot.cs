using OpenQA.Selenium;


namespace SplititAutomation.Utilities
{
    public class HandleScreenshot
    {
        public static string TakeScreenshot(IWebDriver driver)
        {
            try
            {
                string screenshotDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");
                if (!Directory.Exists(screenshotDirectory))
                {
                    Directory.CreateDirectory(screenshotDirectory);
                }

                string screenshotPath = Path.Combine(screenshotDirectory, $"Screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png");
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);

                return screenshotPath;
            }
            catch (Exception ex)
            {
                // Handle failure to take screenshot (log this elsewhere if needed)
                return string.Empty;
            }
        }
    }
}
