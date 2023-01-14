using multi_tenant.Api.Filters.ActionFilters;

namespace multi_tenant.Api.Extensions;

/// <summary>
/// Filter Extension
/// </summary>
public static class FilterExtension
{
    /// <summary>
    /// Configure Filter Extensions
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureFilterExtensions(this IServiceCollection services)
    {
        ConfigureActionFilterExtensions(services);

        return services;
    }

    /// <summary>
    /// Configure Action Filter Extensions
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureActionFilterExtensions(IServiceCollection services)
    {
        services.AddScoped<TenantFilter>();

        return services;
    }
}
