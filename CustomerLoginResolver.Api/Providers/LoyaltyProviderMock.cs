using CustomerLoginResolver.Api.Models;

namespace CustomerLoginResolver.Api.Providers;

public class LoyaltyProviderMock : ILoginProvider
{
    public Task<LoginLookupResult?> FindByEmailAsync(string email, string market)
    {
       if (email.Equals("conflict@test.com", StringComparison.OrdinalIgnoreCase))
{
    return Task.FromResult<LoginLookupResult?>(new LoginLookupResult
    {
        Email = email,
        Market = market,
        AccountFound = true,
        LoginMethod = "Local",
        Source = "Loyalty SQL",
        Confidence = "High",
        RecommendedAction = "Potential conflict with external provider. Further verification required.",
        SupportMessage = "Local account found, but there may be a conflict with an external login provider. Please verify previous login method with the customer.",
        DebugInfo = "Found in Loyalty SQL - local account"
    });
}

        return Task.FromResult<LoginLookupResult?>(null);
    }
}