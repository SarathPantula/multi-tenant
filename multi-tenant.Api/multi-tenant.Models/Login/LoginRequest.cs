using MediatR;

namespace multi_tenant.Models.Login;

/// <summary>
/// Login Request
/// </summary>
public class LoginRequest : IRequest<LoginResponse>
{
    /// <summary>
    /// Username
    /// </summary>
    public string Username { get; set; } = null!;

    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; } = null!;
}