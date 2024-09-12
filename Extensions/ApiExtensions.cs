using RestSharp;

public static class ApiExtensions
{
    public static async Task<RestResponse> GetMerchantReferenceAsync(this RestClient client, string token)
    {
        var request = new RestRequest("/api/Merchant/GetRef", Method.Get);
        request.AddHeader("Authorization", $"Bearer {token}");
        request.AddParameter("IncludeNewMerchants", "true");
        request.AddParameter("LimitResults", "50");
        request.AddParameter("MerchantQueryCriteria.Status", "Live");

        return await client.ExecuteAsync(request);
    }

    public static async Task<RestResponse> GetTerminalPoliciesAsync(this RestClient client, string token)
    {
        var request = new RestRequest("/api/Terminal/Policies", Method.Get);
        request.AddHeader("Authorization", $"Bearer {token}");
        request.AddParameter("IncludeTerminalPolicies", "true");
        request.AddParameter("TerminalQueryCriteria.MerchantId", "35630");

        return await client.ExecuteAsync(request);
    }

    public static async Task<RestResponse> InitiateInstallmentPlanAsync(this RestClient client, string token, int amount, String email)
    {
        var request = new RestRequest("/api/InstallmentPlan/Initiate", Method.Post);
        request.AddHeader("Authorization", $"Bearer {token}");
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Api-Key", "54af0384-7a03-4307-a18d-47d84190e53b");
        request.AddHeader("Merchant-Id", "35630");

        var initiateBody = new
        {
            PlanData = new
            {
                RefOrderNumber = "",
                PurchaseMethod = "Ecommerce",
                Amount = new { Value = amount, CurrencyCode = "GBP" },
                NumberOfInstallments = 6,
                FirstInstallmentAmount = new { Value = 0, CurrencyCode = "GBP" }
            },
            PaymentWizardData = new { RequestedNumberOfInstallments = "2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12" },
            ConsumerData = new { Email = email, PhoneNumber = "", CultureName = "en-US", FullName = "" }
        };
        request.AddJsonBody(initiateBody);
        return await client.ExecuteAsync(request);
    }

    public static async Task<RestResponse> RequestPaymentAsync(this RestClient client, string token, string installmentPlanNumber)
    {
        var request = new RestRequest("/api/InstallmentPlan/RequestPayment", Method.Post);
        request.AddHeader("Authorization", $"Bearer {token}");
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Api-Key", "54af0384-7a03-4307-a18d-47d84190e53b");
        request.AddHeader("Merchant-Id", "35630");

        var requestPaymentBody = new
        {
            InstallmentPlanNumber = installmentPlanNumber,
            PaymentApprovalEmail = "asahalperin@splitittest.com"
        };
        request.AddJsonBody(requestPaymentBody);

        return await client.ExecuteAsync(request);
    }

    public static async Task<RestResponse> CreateInstallmentPlanAsync(this RestClient client, string token, string installmentPlanNumber, string cardNumber, int cardExpMonth, int cardExpYear, string expirationDate)
    {
        var request = new RestRequest("/api/InstallmentPlan/Create", Method.Post);
        request.AddHeader("Authorization", $"Bearer {token}");
        request.AddHeader("Content-Type", "application/json");

        var createPlanBody = new
        {
            InstallmentPlanNumber = installmentPlanNumber,
            BillingAddress = new
            {
                AddressLine = "Tamar St 9",
                AddressLine2 = (string)null,
                City = "Yehud-Monosson",
                Country = "IL",
                State = "Center District",
                Zip = "6019000"
            },
            CreditCardDetails = new
            {
                CardNumber = cardNumber,
                ExpDate = expirationDate,
                CardCvv = "555",
                CardHolderFullName = "Asa H",
                CardExpMonth = cardExpMonth,
                CardExpYear = cardExpYear
            },
            PlanData = new
            {
                NumberOfInstallments = 7,
                ExtendedParams = new
                {
                    forterToken = "be3d7fa3fdaf4884a84126968b4f7161_1726067218871__UDF4_9ck"
                }
            },
            ConsumerData = new
            {
                CultureName = "en-US",
                FullName = "Asa H",
                Email = "testsplitit@test.com",
                PhoneNumber = "+972542655548"
            },
            PaymentWizardData = new { IsOpenedInIframe = true },
            PlanApprovalEvidence = new { AreTermsAndConditionsApproved = true }
        };
        request.AddJsonBody(createPlanBody);

        return await client.ExecuteAsync(request);
    }
}
