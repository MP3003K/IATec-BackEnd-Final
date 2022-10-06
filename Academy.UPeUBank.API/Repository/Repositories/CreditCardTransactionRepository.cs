using Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Repositories.Base;

namespace Repository.Repositories
{
    public class CreditCardTransactionRepository : Repository<CreditCardTransaction>, ICreditCardTransactionRepository
    {
        public CreditCardTransactionRepository(ApplicationDbContext dBContext) : base(dBContext)
        {
        }

        public async Task<double> GetCurrentBalanceByCreditCardId(int creditCardId, DateTime date)
        {
            return await Table
                .Where(x => x.CreditCardId == creditCardId && x.Date.Month == date.Month && x.Date.Year == date.Year) 
                .SumAsync(x => x.Value);
        }
    }
}
