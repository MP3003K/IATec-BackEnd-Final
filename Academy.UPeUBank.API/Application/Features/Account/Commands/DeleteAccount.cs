using Application.Exceptions;
using Application.Wrappers;
using Contracts.Repositories;
using DTOs;
using MediatR;

namespace Application.Features.Account.Commands;

public class DeleteAccountRequest : AccountDto, IRequest<Response<bool>>
{
}

public class DeleteAccountHandler : IRequestHandler<DeleteAccountRequest, Response<bool>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IPixRepository _pixRepository;

    public DeleteAccountHandler(
        IAccountRepository customerRepository,
        ITransactionRepository transactionRepository, IPixRepository pixRepository)
    {
        _accountRepository = customerRepository;
        _transactionRepository = transactionRepository;
        _pixRepository = pixRepository;
    }

    public async Task<Response<bool>> Handle(
        DeleteAccountRequest request,
        CancellationToken cancellationToken)
    {
        var customer = await _accountRepository.GetByIdAsync(request.Id);

        if (customer == null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.Account));
        }

        var transactions = await _transactionRepository.ListByAccountIdAsync(request.Id);

        if (transactions.Any())
        {
            throw new EntityCannotBeRemovedByDependencyException("Account");
        }

        var pixes = await _pixRepository.ListByAccountIdAsync(request.Id);

        if (pixes.Any())
        {
            throw new EntityCannotBeRemovedByDependencyException("Account");
        }

        await _accountRepository.DeleteAsync(customer);

        return new Response<bool>(true);
    }
}