using AventStack.ExtentReports;
using OpenQA.Selenium;
using SplititAutomation.Pages;

namespace SplititAutomation.Utilities
{
    public static class TokenExtractor
    {
        public static Task<string> ExtractAccessTokenAsync(IWebDriver driver, ExtentTest test)
        {
            ChooseMerchant chooseMerchant = new ChooseMerchant(driver);
            chooseMerchant.validateLoggedIn(test);
            // Execute JavaScript to retrieve the token from localStorage or sessionStorage
            var token = ((IJavaScriptExecutor)driver).ExecuteScript("return localStorage.getItem('access_token');") as string;

            // Alternatively, if the token is in sessionStorage:
            // var token = ((IJavaScriptExecutor)driver).ExecuteScript("return sessionStorage.getItem('access_token');") as string;

            return Task.FromResult(token);
        }
    }
}
