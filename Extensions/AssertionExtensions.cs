using AventStack.ExtentReports;
using OpenQA.Selenium;
using SplititAutomation.Utilities;


namespace SplititAutomation.Extensions
{
    public static class AssertionExtensions
    {
        public static void AssertResponse(bool isTrue, ExtentTest test)
        {
            try
            {
                Assert.That(isTrue, "Boolen is False");
                test.Log(Status.Pass, $"Response is correct");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                test.Log(Status.Fail, $"Incorrect response");
                throw;
            }
        }

        public static void AssertElementExists(this IWebDriver driver, ExtentTest test, By by, string message, int timeoutInSeconds = 10)
        {
            try
            {
                var element = driver.WaitUntilElementIsClickable(by, timeoutInSeconds);
                // Find the elements matching the provided locator
                var elements = driver.FindElements(by);
                bool elementExists = elements.Count > 0;
                string assertMessage = message ?? $"Expected element by {by.ToString()} to exist, but it does not.";
                // Assert that the element exists
                Assert.IsTrue(elementExists, assertMessage);
                test.Log(Status.Pass, $"Element is exist");
            }
            catch (Exception ex)
            {
                string screenshotPath = HandleScreenshot.TakeScreenshot(driver);
                Console.WriteLine(ex.Message);
                test.Log(Status.Fail, $"Element is not exist");
                test.AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }
    }
}
