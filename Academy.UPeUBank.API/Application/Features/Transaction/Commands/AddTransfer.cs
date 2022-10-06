using Application.Exceptions;
using Application.Wrappers;
using Contracts.Repositories;
using Contracts.Services;
using MediatR;

namespace Application.Features.Transaction.Commands;

public class AddTransferRequest : IRequest<Response<bool>>
{
    public string? From { get; set; }
    public string? To { get; set; }
    public double? Value { get; set; }

    public class AddTransferHandler : IRequestHandler<AddTransferRequest, Response<bool>>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountService _accountService;

        public AddTransferHandler(
            ITransactionRepository transactionRepository,
            IAccountRepository accountRepository,
            IAccountService accountService)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _accountService = accountService;
        }

        public async Task<Response<bool>> Handle(AddTransferRequest request, CancellationToken cancellationToken)
        {
            if (request.From == null)
            {
                throw new PropertyCannotBeNullException(nameof(AddTransferRequest.From));
            }

            if (request.To == null)
            {
                throw new PropertyCannotBeNullException(nameof(AddTransferRequest.To));
            }

            if (request.Value is null or <= 0)
            {
                throw new ValueNeedsBeGreaterThanZeroException();
            }

            var from = await _accountRepository.GetByCodeAsync(request.From);

            if (from == null)
            {
                throw new EntityNotFoundException(nameof(Domain.Entities.Account));
            }

            var to = await _accountRepository.GetByCodeAsync(request.To);

            if (to == null)
            {
                throw new EntityNotFoundException(nameof(Domain.Entities.Account));
            }

            if (from.Balance < request.Value)
            {
                throw new ThereIsNotEnoughBalanceForThisAccountException();
            }

            var transactionFrom = new Domain.Entities.Transaction(from.Id, -(double) request.Value);

            await _transactionRepository.InsertAsync(transactionFrom);

            await _accountService.UpdateCurrentBalanceAsync(from.Id);

            var transactionTo = new Domain.Entities.Transaction(to.Id, (double) request.Value);

            await _transactionRepository.InsertAsync(transactionTo);

            await _accountService.UpdateCurrentBalanceAsync(to.Id);

            return new Response<bool>(true);
        }
    }
}