//using Newtonsoft.Json.Linq;
//using NUnit.Framework;
//using RestSharp;
//using SplititAutomation.Pages;
//using SplititAutomation.Utilities;
//using System.Threading.Tasks;
//using static System.Runtime.InteropServices.JavaScript.JSType;

//namespace SplititAutomation.Tests
//{
//    public class MerchantTests : BaseTest
//    {
//        private string token;

//        // This method provides test cases for the test method
//        public static IEnumerable<TestCaseData> GetTestCases()
//        {
//            for (int i = 1; i <= 20; i++)
//            {
//                string cardNumber = "4111111111111111";
//                int cardExpMonth = 2;
//                int cardExpYear = 25;
//                string expirationDate = "02/25";
//                string email = $"user{i}@example.com";
//                int amount = 100 + i; // Varying the amount slightly with each iteration
//                yield return new TestCaseData(amount, email, cardNumber, cardExpMonth, cardExpYear, expirationDate)
//                             .SetName($"TestApiWithTokenAsync_Iteration_{i}");
//            }
//        }

//        // Additional test case with the new card details
//        public static IEnumerable<TestCaseData> GetSpecialCardTestCase()
//        {
//            string cardNumber = "4222222222222220";
//            int cardExpMonth = 10;
//            int cardExpYear = 25;
//            string expirationDate = "10/25";
//            string email = $"test@valid.com";
//            int amount = 100;
//            yield return new TestCaseData(amount, email, cardNumber, cardExpMonth, cardExpYear, expirationDate)
//                .SetName("TestApiWithToken_SpecialCardDetails");
//        }


//        [Test, Category("Selenium"), Order(1)]
//        public async Task LoginAndExtractTokenTest()
//        {
//            // Navigate to the login page
//            driver.Navigate().GoToUrl("https://pos.sandbox.splitit.com/");

//            // Perform the login
//            PreLoginPage preLoginPage = new PreLoginPage(driver);
//            preLoginPage.Login(test);
//            LoginPage loginPage = new LoginPage(driver);
//            loginPage.Login("qa@splitit.com", "A1qazxsw23434!", test);

//            // Extract the access token
//            token = await TokenExtractor.ExtractAccessTokenAsync(driver);
//            Assert.IsNotNull(token, "Access token should not be null");

//            // Store the token using TokenManager
//            TokenManager.SetToken(token);

//            test.Log(AventStack.ExtentReports.Status.Pass, $"Access token extracted and stored: {token}");
//        }

//        [Test, Order(2)]
//        [TestCaseSource(nameof(GetTestCases))]
//        [TestCaseSource(nameof(GetSpecialCardTestCase))]
//        public void TestApiWithToken(int amount, string email, string cardNumber, int cardExpMonth, int cardExpYear, string expirationDate)
//        {
//            token = TokenManager.GetToken();
//            Assert.IsNotNull(token, "Access token should be available for the API test");

//            var client = new RestClient("https://api.sandbox.splitit.com");

//            // Log the amount and email
//            test.Log(AventStack.ExtentReports.Status.Info, $"Running test with Amount: {amount}, Email: {email}, CardNumber: {cardNumber}, Expiration: {cardExpMonth}/{cardExpYear}");

//            //// Get Merchant Reference
//            //var response = client.GetMerchantReferenceAsync(token).Result;
//            //Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Failed to get merchant reference");
//            //test.Log(AventStack.ExtentReports.Status.Pass, "Successfully got merchant reference.");

//            //// Get Terminal Policies
//            //response = client.GetTerminalPoliciesAsync(token).Result;
//            //Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Failed to get terminal policies");
//            //test.Log(AventStack.ExtentReports.Status.Pass, "Successfully got terminal policies.");

//            // Initiate Installment Plan with varied amount and email
//            var response = client.InitiateInstallmentPlanAsync(token, amount, email).Result;
//            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Failed to initiate installment plan");
//            var installmentPlanNumber = JObject.Parse(response.Content)["InstallmentPlan"]["InstallmentPlanNumber"]?.ToString();
//            Assert.IsNotNull(installmentPlanNumber, "InstallmentPlanNumber should not be null");
//            test.Log(AventStack.ExtentReports.Status.Pass, $"Successfully initiated installment plan. InstallmentPlanNumber: {installmentPlanNumber}");

//            //// Request Payment
//            //response = client.RequestPaymentAsync(token, installmentPlanNumber).Result;
//            //Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Failed to request payment");
//            //test.Log(AventStack.ExtentReports.Status.Pass, "Successfully requested payment.");

//            // Create Installment Plan
//            response = client.CreateInstallmentPlanAsync(token, installmentPlanNumber, cardNumber, cardExpMonth, cardExpYear, expirationDate).Result;
//            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Failed to create installment plan");
//            test.Log(AventStack.ExtentReports.Status.Pass, "Successfully created installment plan.");
//        }
//    }
//}
