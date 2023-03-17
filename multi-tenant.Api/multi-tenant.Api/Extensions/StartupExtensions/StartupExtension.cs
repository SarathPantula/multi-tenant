using core.Extensions.StartUpExtensions;

namespace multi_tenant.Api.Extensions.StartupExtensions;

/// <summary>
/// Startup Extensions
/// </summary>
public static class StartupExtension
{
    /// <summary>
    /// Register Startup Extensions
    /// </summary>
    /// <param name="services">IServices Collection <see cref="IServiceCollection"/></param>
    /// <param name="configuration">ICOnfiguration <see cref="IConfiguration"/></param>
    /// <returns></returns>
    public static IServiceCollection RegisterStartupExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureBaseExtensions(configuration);
        services.RegisterUnitOfWorkServices();
        services.RegisterRedisCacheServices();

        return services;
    }
}
