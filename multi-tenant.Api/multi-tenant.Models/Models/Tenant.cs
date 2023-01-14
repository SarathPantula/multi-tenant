namespace multi_tenant.Models.Models;

/// <summary>
/// Tenant
/// </summary>
public partial class Tenant
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Accounts
    /// </summary>
    public virtual ICollection<Account> Accounts { get; } = new List<Account>();
}
