using Contracts.Repositories.Base;
using Domain.Entities;

namespace Contracts.Repositories;

public interface IPixRepository : IRepository<Pix>
{
    Task<Pix?> GetPixByKeyAsync(string key);
    Task<IList<Pix>> ListByAccountIdAsync(int accountId);
}