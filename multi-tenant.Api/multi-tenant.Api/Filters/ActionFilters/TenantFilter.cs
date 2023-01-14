using core.Base.UnitOfWork;
using core.Extensions;
using multi_tenant.Models.Enums;
using multi_tenant.Models.Exceptions;
using multi_tenant.Models.Models;

namespace multi_tenant.Api.Filters.ActionFilters;

/// <summary>
/// Tenant Filter
/// </summary>
public class TenantFilter : IMiddleware
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Tenant Filter
    /// </summary>
    public TenantFilter(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Invoke Async
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (!context.Request.Headers.ContainsKey("X-TenantId") ||
    string.IsNullOrEmpty(context.Request.Headers["X-TenantId"]))
            throw new TenantException((int)ErrorCode.TenantKeyUnavailable, ErrorCode.TenantKeyUnavailable.GetDescription());

        var tenantId = context.Request.Headers["X-TenantId"];
        context.Items["TenantId"] = tenantId;

        try
        {
            var _tenantRepo = _unitOfWork.GetRepository<Tenant>();
            var tenant = await _tenantRepo.GetAsync(new Guid(tenantId.ToString()));
            if (tenant == null)
                throw new TenantException((int)ErrorCode.InvalidTenant, ErrorCode.InvalidTenant.GetDescription());

            await next(context);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
