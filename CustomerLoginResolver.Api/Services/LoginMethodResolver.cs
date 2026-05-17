using CustomerLoginResolver.Api.Models;
using CustomerLoginResolver.Api.Providers;

namespace CustomerLoginResolver.Api.Services;

public class LoginMethodResolver
{
    private readonly DocoProviderMock _docoProvider;
    private readonly LoyaltyProviderMock _loyaltyProvider;
    private readonly LawProviderMock _lawProvider;

    public LoginMethodResolver(
        DocoProviderMock docoProvider,
        LoyaltyProviderMock loyaltyProvider,
        LawProviderMock lawProvider)
    {
        _docoProvider = docoProvider;
        _loyaltyProvider = loyaltyProvider;
        _lawProvider = lawProvider;
    }

    public async Task<LoginLookupResult> ResolveAsync(string email, string market)
    {
        var docoResult = await _docoProvider.FindByEmailAsync(email, market);

        // doco.db is the primary source of truth.
        // If email is found there, we stop and do not check other sources.
        if (docoResult is not null)
        {
            return docoResult;
        }

        // If doco.db has no result, check Loyalty and LAW.
        // We check both to detect possible conflicts.
        var loyaltyResult = await _loyaltyProvider.FindByEmailAsync(email, market);
        var lawResult = await _lawProvider.FindByEmailAsync(email, market);

        if (loyaltyResult is not null && lawResult is not null)
{
    return new LoginLookupResult
    {
        Email = email,
        Market = market,
        AccountFound = true,
        LoginMethod = "Multiple",
        Source = "Loyalty + LAW",
        Confidence = "Low",
        RecommendedAction = "Multiple login methods detected. Manual verification is required.",
        SupportMessage = "Conflicting login methods were detected across systems. Please verify the previous login method with the customer before suggesting a reset or cleanup.",
        DebugInfo = $"Loyalty={loyaltyResult.LoginMethod}, LAW={lawResult.LoginMethod}"
    };
}

        if (loyaltyResult is not null)
        {
            return loyaltyResult;
        }

        if (lawResult is not null)
        {
            return lawResult;
        }

        return new LoginLookupResult
        {
            Email = email,
            Market = market,
            AccountFound = false,
            LoginMethod = null,
            Source = "None",
            Confidence = "Low",
            RecommendedAction = "Escalate for manual investigation.",
            SupportMessage = "No account or login method was found in the available sources. Please escalate for manual investigation."
        };
    }
}