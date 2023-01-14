using core.Models.DefaultResponses;

namespace multi_tenant.Models.Login;

/// <summary>
/// Login Response
/// </summary>
public class LoginResponse : IDefaultResponse
{
    /// <summary>
    /// Access Token
    /// </summary>
    public string AccessToken { get; set; } = null!;

    /// <summary>
    /// Expiry
    /// </summary>
    public DateTime Expiry { get; set; }

    /// <summary>
    /// Errors
    /// </summary>
    public List<ErrorInfo> Errors { get; set; } = new List<ErrorInfo>();
}