using AdminCenter.Domain.DomainServices;
using Microsoft.Extensions.DependencyInjection;

namespace AdminCenter.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomainService(this IServiceCollection services)
    {
        services.AddTransient<UserManager>();
        services.AddTransient<RoleManager>();
        services.AddTransient<MenuManager>();
        services.AddTransient<PositionManager>();
        services.AddTransient<OrganizationManager>();

        return services;
    }
}
