using CustomerLoginResolver.Api.Models;

namespace CustomerLoginResolver.Api.Providers;

public class LawProviderMock : ILoginProvider
{
    public Task<LoginLookupResult?> FindByEmailAsync(string email, string market)
    {
        if (email.Equals("conflict@test.com", StringComparison.OrdinalIgnoreCase))
{
    return Task.FromResult<LoginLookupResult?>(new LoginLookupResult
    {
        Email = email,
        Market = market,
        AccountFound = false,
        LoginMethod = "Google",
        Source = "LAW.Name",
        Confidence = "Medium",
        LastSeenUtc = DateTime.UtcNow.AddDays(-1),
        RecommendedAction = "Possible mismatch between local and external account.",
        SupportMessage = "Recent logs indicate Google login, but local account also exists. Please ask the customer which method they previously used.",
        DebugInfo = "Found in LAW - provider inferred from event name"
    });
}
        return Task.FromResult<LoginLookupResult?>(null);
    }
}