using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IoC.Configurations;

public static class WebClientsConfiguration
{
    public static IServiceCollection AddWebClientsConfig(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpClient("CentralBankApi",
            options => { options.BaseAddress = new Uri(configuration.GetSection("ExternalHosts:CentralBakApi").Value); });
        
        return services;
    }
}
