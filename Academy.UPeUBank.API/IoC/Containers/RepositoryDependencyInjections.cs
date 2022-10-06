using Contracts.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories;

namespace IoC.Containers;

public static class RepositoryDependencyInjections
{
    public static IServiceCollection AddRepositoryInjections(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>()
            .AddScoped<IAccountRepository, AccountRepository>()
            .AddScoped<ITransactionRepository, TransactionRepository>()
            .AddScoped<IPixRepository, PixRepository>()
            .AddScoped<ICreditCardRepository, CreditCardRepository>()
            .AddScoped<ICreditCardTransactionRepository, CreditCardTransactionRepository>();
        
        return services;
    }
}