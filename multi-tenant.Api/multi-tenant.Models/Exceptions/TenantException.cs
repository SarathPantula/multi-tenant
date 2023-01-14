using core.Exceptions;

namespace multi_tenant.Models.Exceptions;

/// <summary>
/// Tenant Exception
/// </summary>
public class TenantException : GenericException
{
    /// <summary>
    /// 
    /// </summary>
    public TenantException(int errorCode, string message) : base(errorCode, message)
    {
    }
    /// <summary>
    /// 
    /// </summary>
    public TenantException(int errorCode, string message, Exception innerException) : base(errorCode, message, innerException)
    {
    }
}
