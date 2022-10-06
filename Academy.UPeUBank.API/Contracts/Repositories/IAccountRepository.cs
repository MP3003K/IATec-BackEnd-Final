using Contracts.Repositories.Base;
using Domain.Entities;

namespace Contracts.Repositories;

public interface IAccountRepository : IRepository<Account>
{
    Task<Account?> GetFullByIdAsync(int id);
    Task<Account?> GetByCodeAsync(string code);
    Task<IList<Account>> ListFullAllAsync();
    Task<IList<Account>> ListByCustomerIdAsync(int customerId);
}