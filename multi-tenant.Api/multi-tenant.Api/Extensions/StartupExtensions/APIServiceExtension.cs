using FluentValidation;
using MediatR;
using multi_tenant.Managers.Login;
using multi_tenant.Models.Login;
using multi_tenant.Repositories.User;

namespace multi_tenant.Api.Extensions.StartupExtensions;

/// <summary>
/// APIServiceExtension
/// </summary>
public static class APIServiceExtension
{
    /// <summary>
    /// Register API Services
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterAPIServices(this IServiceCollection services)
    {
        RegisterLoginServices(services);

        return services;
    }

    private static IServiceCollection RegisterLoginServices(IServiceCollection services)
    {
        services.AddMediatR(typeof(LoginRequest).Assembly);
        services.AddTransient<IValidator<LoginRequest>, LoginValidator>();
        services.AddTransient<IRequestHandler<LoginRequest, LoginResponse>, LoginHandler>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
