using Application.Exceptions;
using Contracts.Repositories;
using Contracts.Services;
using Domain.Entities;

namespace Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionRepository _transactionRepository;

    public AccountService(
        IAccountRepository accountRepository,
        ITransactionRepository transactionRepository)
    {
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task<double> UpdateCurrentBalanceAsync(int id)
    {
        var account = await _accountRepository.GetByIdAsync(id);

        if (account == null)
        {
            throw new EntityNotFoundException(nameof(Account));
        }

        var currentBalance = await _transactionRepository.GetCurrentBalanceByAccountId(id);

        account.UpdateBalance(currentBalance);

        await _accountRepository.UpdateAsync(account);

        return currentBalance;
    }
}