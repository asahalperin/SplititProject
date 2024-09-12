using NUnit.Framework;
using OpenQA.Selenium;
using SplititAutomation.Drivers;
using SplititAutomation.Utilities;
using AventStack.ExtentReports;

namespace SplititAutomation.Tests
{
    [TestFixture]
    public abstract class BaseTest
    {
        protected IWebDriver? driver;
        protected ExtentReports extent;
        public static ExtentTest test;

        [OneTimeSetUp]
        public void SetUp()
        {
            extent = ExtentManager.GetInstance();
        }

        [SetUp]
        public void Initialize()
        {
            test = extent.CreateTest(NUnit.Framework.TestContext.CurrentContext.Test.Name);

            // Check if the current test has the "Selenium" category
            var isSeleniumTest = TestContext.CurrentContext.Test.Properties["Category"].Contains("Selenium");
            if(isSeleniumTest)
                driver = CustomWebDriverManager.InitializeDriver();
        }
       
        [TearDown]
        public void CleanUp()
        {
            if (driver != null)
            {
                try
                {
                    driver.Quit();  // This should terminate chromedriver.exe
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception during driver cleanup: {ex.Message}");
                }
                finally
                {
                    driver.Dispose();  // Ensures all resources are released
                    driver = null;
                }
            }

            extent.Flush();
        }
    }
}
