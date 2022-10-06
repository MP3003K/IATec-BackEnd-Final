using Contracts.Repositories;
using Domain.Entities;
using Repository.Context;
using Repository.Repositories.Base;

namespace Repository.Repositories;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext dBContext) : base(dBContext)
    {
    }
}