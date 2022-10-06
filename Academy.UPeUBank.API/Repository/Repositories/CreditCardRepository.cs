using Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Repositories.Base;


namespace Repository.Repositories
{
    public class CreditCardRepository : Repository<CreditCard>, ICreditCardRepository
    {
        public CreditCardRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }

        public async Task<CreditCard?> GetByNumberAsync(string number)
        {
            return await Table.FirstOrDefaultAsync(x => x.Number == number);
        }
    }
}
