using AventStack.ExtentReports;
using OpenQA.Selenium;
using SplititAutomation.Extensions;

namespace SplititAutomation.Pages
{
    public class PreLoginPage
    {
        private readonly IWebDriver _driver;

        private readonly By newTransactionButton = By.XPath("//span[text()='New Transaction']");

        public PreLoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Login(ExtentTest test)
        {
            _driver.ClickElement(newTransactionButton, test);
            _driver.AssertElementExists(test, LoginPage.loginButton, "Login button is exist on page");
        }
    }
}
