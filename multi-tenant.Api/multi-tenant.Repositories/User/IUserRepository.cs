using multi_tenant.Models.Models;

namespace multi_tenant.Repositories.User;

/// <summary>
/// IUserRepository
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Get User By UserName Async
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    Task<Account?> GetUserByUserNameAsync(string userName);
}
