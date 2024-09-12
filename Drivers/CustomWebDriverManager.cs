using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SplititAutomation.Drivers
{
    public static class CustomWebDriverManager
    {
        public static IWebDriver InitializeDriver()
        {
            // Use WebDriverManager to download and set up the correct version of ChromeDriver
            // It looks like the support is up to chrome version 114 - So i download chrome 114 and turned of Updates.
            new DriverManager().SetUpDriver(new ChromeConfig());

            // Open browser maximized
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            return new ChromeDriver(options);
        }

        // Quit browser
        public static void QuitDriver(IWebDriver driver)
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}
