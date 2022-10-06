using Contracts.Repositories.Base;
using Domain.Entities;

namespace Contracts.Repositories;

public interface ITransactionRepository : IRepository<Transaction>
{
    Task<double> GetCurrentBalanceByAccountId(int accountId);
    Task<IList<Transaction>> ListByAccountIdAsync(int accountId);
}