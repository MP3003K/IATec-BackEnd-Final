namespace Contracts.Services;

public interface IAccountService
{
    Task<double> UpdateCurrentBalanceAsync(int id);
}