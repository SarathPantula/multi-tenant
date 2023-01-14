namespace multi_tenant.Models.Models;

/// <summary>
/// Account
/// </summary>
public partial class Account
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// TenantId
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// FullName
    /// </summary>
    public string FullName { get; set; } = null!;

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Username
    /// </summary>
    public string Username { get; set; } = null!;

    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    /// Tenant
    /// </summary>
    public virtual Tenant Tenant { get; set; } = null!;
}