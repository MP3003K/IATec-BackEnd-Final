using Application.Exceptions;
using Application.Wrappers;
using Contracts.Repositories;
using Contracts.Services;
using MediatR;

namespace Application.Features.Transaction.Commands;

public class AddDebitRequest : IRequest<Response<bool>>
{
    public string? Account { get; set; }
    public double? Value { get; set; }
}

public class AddDebitHandler : IRequestHandler<AddDebitRequest, Response<bool>>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IAccountService _accountService;

    public AddDebitHandler(
        ITransactionRepository transactionRepository,
        IAccountRepository accountRepository,
        IAccountService accountService)
    {
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;
        _accountService = accountService;
    }

    public async Task<Response<bool>> Handle(
        AddDebitRequest request,
        CancellationToken cancellationToken)
    {
        if (request.Account == null)
        {
            throw new PropertyCannotBeNullException(nameof(AddDebitRequest.Account));
        }

        var account = await _accountRepository.GetByCodeAsync(request.Account);

        if (account == null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.Account));
        }

        if (request.Value is null or <= 0)
        {
            throw new ValueNeedsBeGreaterThanZeroException();
        }

        if (account.Balance < request.Value)
        {
            throw new ThereIsNotEnoughBalanceForThisAccountException();
        }

        var transaction = new Domain.Entities.Transaction(account.Id, -(double) request.Value);

        await _transactionRepository.InsertAsync(transaction);

        await _accountService.UpdateCurrentBalanceAsync(account.Id);

        return new Response<bool>(true);
    }
}