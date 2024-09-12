using Newtonsoft.Json.Linq;
using RestSharp;
using SplititAutomation.Extensions;
using SplititAutomation.Pages;
using SplititAutomation.Utilities;


namespace SplititAutomation.Tests
{
    public class MerchantTests : BaseTest
    {
        private string token;

        // This method provides test cases for the test method
        public static IEnumerable<TestCaseData> GetTestCases()
        {
            for (int i = 1; i <= 20; i++)
            {
                string cardNumber = "4111111111111111";
                int cardExpMonth = 2;
                int cardExpYear = 25;
                string expirationDate = "02/25";
                string email = $"user{i}@example.com";
                int amount = 100 + i; // Varying the amount slightly with each iteration
                yield return new TestCaseData(amount, email, cardNumber, cardExpMonth, cardExpYear, expirationDate)
                             .SetName($"TestApiWithTokenAsync_Iteration_{i}");
            }
        }

        // Additional test case with the new card details
        public static IEnumerable<TestCaseData> GetSpecialCardTestCase()
        {
            string cardNumber = "4222222222222220";
            int cardExpMonth = 10;
            int cardExpYear = 25;
            string expirationDate = "10/25";
            string email = $"test@valid.com";
            int amount = 100;
            yield return new TestCaseData(amount, email, cardNumber, cardExpMonth, cardExpYear, expirationDate)
                .SetName("TestApiWithToken_SpecialCardDetails");
        }


        [Test, Category("Selenium"), Order(1)]
        public async Task LoginAndExtractTokenTest()
        {
            // Navigate to the login page
            driver.Navigate().GoToUrl("https://pos.sandbox.splitit.com/");

            // Perform the login
            PreLoginPage preLoginPage = new PreLoginPage(driver);
            preLoginPage.Login(test);
            LoginPage loginPage = new LoginPage(driver);
            loginPage.Login("qa@splitit.com", "A1qazxsw23434!", test);

            // Extract the access token
            token = await TokenExtractor.ExtractAccessTokenAsync(driver, test);
            Assert.IsNotNull(token, "Access token should not be null");

            // Store the token using TokenManager
            TokenManager.SetToken(token);

            test.Log(AventStack.ExtentReports.Status.Pass, $"Access token extracted and stored: {token}");
        }

        [Test, Order(2)]
        [TestCaseSource(nameof(GetTestCases))]
        [TestCaseSource(nameof(GetSpecialCardTestCase))]
        public async Task TestApiWithToken(int amount, string email, string cardNumber, int cardExpMonth, int cardExpYear, string expirationDate)
        {
            token = TokenManager.GetToken();
            Assert.IsNotNull(token, "Access token should be available for the API test");

            var client = new RestClient("https://api.sandbox.splitit.com");

            // Log the amount and email
            test.Log(AventStack.ExtentReports.Status.Info, $"Running test with Amount: {amount}, Email: {email}, CardNumber: {cardNumber}, Expiration: {cardExpMonth}/{cardExpYear}");

            // Initiate Installment Plan with varied amount and email
            var response = await client.InitiateInstallmentPlanAsync(token, amount, email);
            bool responseStatusCode = response.StatusCode.Equals(System.Net.HttpStatusCode.OK);
            AssertionExtensions.AssertResponse(responseStatusCode, test);
            var installmentPlanNumber = JObject.Parse(response.Content)["InstallmentPlan"]["InstallmentPlanNumber"]?.ToString();
            Assert.IsNotNull(installmentPlanNumber, "InstallmentPlanNumber should not be null");
            test.Log(AventStack.ExtentReports.Status.Pass, $"Successfully initiated installment plan. InstallmentPlanNumber: {installmentPlanNumber}");

            // Create Installment Plan
            response = await client.CreateInstallmentPlanAsync(token, installmentPlanNumber, cardNumber, cardExpMonth, cardExpYear, expirationDate);
            responseStatusCode = response.StatusCode.Equals(System.Net.HttpStatusCode.OK);
            AssertionExtensions.AssertResponse(responseStatusCode, test);
            test.Log(AventStack.ExtentReports.Status.Pass, "Successfully created installment plan.");
        }
    }
}
