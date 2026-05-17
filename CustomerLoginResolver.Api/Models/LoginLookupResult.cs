namespace CustomerLoginResolver.Api.Models;

public class LoginLookupResult
{
    public string Email { get; set; } = string.Empty;
    public string Market { get; set; } = string.Empty;
    public bool AccountFound { get; set; }
    public string? LoginMethod { get; set; }
    public string Source { get; set; } = string.Empty;
    public string Confidence { get; set; } = string.Empty;
    public DateTime? LastSeenUtc { get; set; }
    public string RecommendedAction { get; set; } = string.Empty;
    public string SupportMessage { get; set; } = string.Empty;
    public string? DebugInfo { get; set; }
}