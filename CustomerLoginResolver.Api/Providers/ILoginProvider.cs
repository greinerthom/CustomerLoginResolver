using CustomerLoginResolver.Api.Models;

namespace CustomerLoginResolver.Api.Providers;

public interface ILoginProvider
{
    Task<LoginLookupResult?> FindByEmailAsync(string email, string market);
}