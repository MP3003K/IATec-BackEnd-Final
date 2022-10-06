using Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Repositories.Base;

namespace Repository.Repositories;

public class TransactionRepository : Repository<Domain.Entities.Transaction>, ITransactionRepository
{
    public TransactionRepository(ApplicationDbContext dBContext) : base(dBContext)
    {
    }

    public async Task<double> GetCurrentBalanceByAccountId(int accountId)
    {
        return await Table
            .Where(x => x.AccountId == accountId)
            .SumAsync(x => x.Value);
    }

    public async Task<IList<Domain.Entities.Transaction>> ListByAccountIdAsync(int accountId)
    {
        return await Table
            .Where(x => x.AccountId == accountId)
            .ToListAsync();
    }
}