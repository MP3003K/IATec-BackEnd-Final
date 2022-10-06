using Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Repositories.Base;

namespace Repository.Repositories;

public class AccountRepository : Repository<Account>, IAccountRepository
{
    public AccountRepository(ApplicationDbContext dBContext) : base(dBContext)
    {
    }

    public async Task<Account?> GetFullByIdAsync(int id)
    {
        return await Table.Where(x => x.Id == id)
            .Include(x => x.Customer)
            .FirstOrDefaultAsync();
    }

    public async Task<Account?> GetByCodeAsync(string code)
    {
        return await Table.FirstOrDefaultAsync(x => x.Code == code);
    }

    public async Task<IList<Account>> ListFullAllAsync()
    {
        return await Table
            .Include(x => x.Customer)
            .ToListAsync();
    }

    public async Task<IList<Account>> ListByCustomerIdAsync(int customerId)
    {
        return await Table
            .Where(x => x.CustomerId == customerId)
            .ToListAsync();
    }
}