namespace SplititAutomation.Utilities
{
    public static class TokenManager
    {
        private static string accessToken;

        public static void SetToken(string token)
        {
            accessToken = token;
        }

        public static string GetToken()
        {
            return accessToken;
        }
    }
}
