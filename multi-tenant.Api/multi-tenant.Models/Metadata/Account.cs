namespace multi_tenant.Models.Models;

/// <summary>
/// Account
/// </summary>
public partial class Account
{
    /// <summary>
    /// Verify Password
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public bool VerifyPassword(string password)
    {
        return Password == password;
    }
}
