using CustomerLoginResolver.Api.Models;

namespace CustomerLoginResolver.Api.Providers;

public class DocoProviderMock : ILoginProvider
{
    public Task<LoginLookupResult?> FindByEmailAsync(string email, string market)
    {
        if (email.Equals("google@test.com", StringComparison.OrdinalIgnoreCase))
        {
            return Task.FromResult<LoginLookupResult?>(new LoginLookupResult
            {
                Email = email,
                Market = market,
                AccountFound = true,
                LoginMethod = "Google",
                Source = "doco.db",
                Confidence = "High",
                RecommendedAction = "Ask the customer to log in using Google.",
                SupportMessage = "The account exists and is linked to Google login. Please ask the customer to sign in using Google.",
                DebugInfo = "Found in doco.db - provider mapping available"
            });
        }

        return Task.FromResult<LoginLookupResult?>(null);
    }
}