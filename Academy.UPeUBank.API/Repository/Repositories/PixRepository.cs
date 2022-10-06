using Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Repositories.Base;

namespace Repository.Repositories;

public class PixRepository : Repository<Pix>, IPixRepository
{
    public PixRepository(ApplicationDbContext dBContext) : base(dBContext)
    {
    }

    public async Task<Pix?> GetPixByKeyAsync(string key)
    {
        return await Table
            .Include(x => x.Account)
            .FirstOrDefaultAsync(x => x.Key == key);
    }

    public async Task<IList<Pix>> ListByAccountIdAsync(int accountId)
    {
        return await Table
            .Where(x => x.AccountId == accountId).ToListAsync();
    }
}