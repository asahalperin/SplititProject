using AventStack.ExtentReports;
using OpenQA.Selenium;
using SplititAutomation.Extensions;

namespace SplititAutomation.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        private readonly By emailField = By.Id("Username");
        private readonly By passwordField = By.Id("Password");
        public static readonly By loginButton = By.XPath("//*[@name='button'][text() = 'Login']");
        private readonly By acceptCookies = By.Id("onetrust-accept-btn-handler");


        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Login(string email, string password, ExtentTest test)
        {
            //IWebElement cookies = _driver.FindElement(acceptCookies);
            _driver.SendKeysToElement(emailField, test, email);
            _driver.SendKeysToElement(passwordField, test, password);
            _driver.ClickElement(acceptCookies, test);
            _driver.ClickElement(loginButton, test);
            _driver.AssertElementExists(test, ChooseMerchant.chooseMerchantField, "Choose Merchant is exist on page");
        }
    }
}
