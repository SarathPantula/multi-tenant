using Microsoft.EntityFrameworkCore;
using multi_tenant.Models.Models;

namespace multi_tenant.Repositories.User;

/// <summary>
/// User Repository
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly MultiTenantContext _context;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="multiTenantContext"></param>
    public UserRepository(MultiTenantContext multiTenantContext)
    {
        _context = multiTenantContext;
    }

    /// <summary>
    /// GetUserByUserNameAsync
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Account?> GetUserByUserNameAsync(string userName)
    {
        return await _context.Accounts.SingleOrDefaultAsync(account =>
        account.Username == userName);
    }
}