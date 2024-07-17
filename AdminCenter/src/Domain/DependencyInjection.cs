using Microsoft.Extensions.DependencyInjection;

namespace AdminCenter.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomainService(this IServiceCollection services)
    {
        services.AddTransient<UserManager>();
        services.AddTransient<OrganizationManager>();

        return services;
    }
}
