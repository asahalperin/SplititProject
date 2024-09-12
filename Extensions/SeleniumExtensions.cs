using AngleSharp.Dom;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using SplititAutomation.Utilities;

namespace SplititAutomation.Extensions
{
    public static class SeleniumExtensions
    {
        public static void ClickElement(this IWebDriver driver, By locator, ExtentTest test, int timeoutInSeconds = 10)
        {
            try
            {
                var element = driver.WaitUntilElementIsClickable(locator, timeoutInSeconds);
                element.Click();
                test.Log(Status.Pass, $"Clicked on element: {element}");
            }
            catch (Exception ex) 
            {
                string screenshotPath = HandleScreenshot.TakeScreenshot(driver);
                test.Log(Status.Fail, $"Failed to click on element. Exception: {ex.Message}");
                test.AddScreenCaptureFromPath(screenshotPath);
                throw;
            }   

        }

        public static void SendKeysToElement(this IWebDriver driver, By locator, ExtentTest test, string text, int timeoutInSeconds = 10)
        {
            try
            {
                var element = driver.WaitUntilElementIsVisible(locator, timeoutInSeconds);
                element.Clear();
                element.SendKeys(text);
                test.Log(Status.Pass, $"Update text: {text} on element: {element}");
            }
            catch (Exception ex)
            {
                string screenshotPath = HandleScreenshot.TakeScreenshot(driver);
                test.Log(Status.Fail, $"Failed to update text on element. Exception: {ex.Message}");
                test.AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }
    }
}
