using AventStack.ExtentReports;
using OpenQA.Selenium;
using SplititAutomation.Extensions;

namespace SplititAutomation.Pages
{
    public class ChooseMerchant
    {
        private readonly IWebDriver _driver;

        public static readonly By chooseMerchantField = By.Id("mat-input-0");

        public ChooseMerchant(IWebDriver driver)
        {
            _driver = driver;
        }

        public void validateLoggedIn(ExtentTest test)
        {
            _driver.AssertElementExists(test, chooseMerchantField, "Choose Merchant is exist on page");
        }
    }
}
