using Application.Services;
using Contracts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IoC.Containers;

public static class ServiceDependencyInjections
{
    public static IServiceCollection AddServiceInjections(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();

        return services;
    }
}