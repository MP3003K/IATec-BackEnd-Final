using Application.Exceptions;
using Application.Wrappers;
using Contracts.Repositories;
using Contracts.Services;
using MediatR;

namespace Application.Features.CreditCardTransaction.Commands
{
    public class AddDebitToCreditCardRequest : IRequest<Response<bool>>
    {
        public string? Number { get; set; }
        public double? Value { get; set; }
    }

    public class AddDebitToCreditCardHandler : IRequestHandler<AddDebitToCreditCardRequest, Response<bool>>
    {
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly ICreditCardTransactionRepository _creditCardTransactionRepository;
        private readonly ITransactionRepository _transactionRepository;

        public AddDebitToCreditCardHandler(
        ICreditCardTransactionRepository creditCardTransactionRepository,
        ICreditCardRepository creditCardRepository,
        ITransactionRepository transactionRepository)
        {
            _creditCardTransactionRepository = creditCardTransactionRepository;
            _creditCardRepository = creditCardRepository;
            _transactionRepository = transactionRepository;
            
        }

        public async Task<Response<bool>> Handle(AddDebitToCreditCardRequest request, CancellationToken cancellationToken)
        {
            // Validar que los valores no sean null
            
            if (request.Number == null)
            {
                throw new PropertyCannotBeNullException(nameof(AddDebitToCreditCardRequest.Number));
            }
            
            if (request.Value == null)
            {
                throw new PropertyCannotBeNullException(nameof(AddDebitToCreditCardRequest.Value));
            }

            // Validar que el credito solicitado sea mayor a cero

            if (request.Value <= 0)
            {
                throw new ValueNeedsBeGreaterThanZeroException();
            }

            // Validar que exista la tarjeta de credito

            var creditCard = await _creditCardRepository.GetByNumberAsync(request.Number);

            if (creditCard == null)
            {
                throw new EntityNotFoundException(nameof(Domain.Entities.CreditCard));
            }

            // Validar que la tarjeta de credito cuente con fondos suficientes
            
            var balance = await _creditCardTransactionRepository.GetCurrentBalanceByCreditCardId(creditCard.Id,DateTime.Now);

            var debitAllowed = creditCard.Limit + balance;

            if (request.Value > debitAllowed )
            {
                throw new CreditCardWithInsufficientBalanceException();
            }


            var CreditCardtransaction = new Domain.Entities.CreditCardTransaction(creditCard.Id, -(double)request.Value);

            await _creditCardTransactionRepository.InsertAsync(CreditCardtransaction);

            return new Response<bool>(true);
        }
    }
}
