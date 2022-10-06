using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using Contracts.Repositories;
using DTOs;
using MediatR;


namespace Application.Features.CreditCard.Commands
{
    public class AddCreditCardRequest : IRequest<Response<CreditCardDto>>
    {
        public int? AccountId { get; set; }
        public double? Limit { get; set; }
    }

    public class AddCreditCardHandler : IRequestHandler<AddCreditCardRequest, Response<CreditCardDto>>
    {
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly IAccountRepository _accountRepository;

        private IMapper _mapper;
        
        public AddCreditCardHandler(ICreditCardRepository creditCardRepository, IAccountRepository accountRepository, IMapper mapper)
        {
            _creditCardRepository = creditCardRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<Response<CreditCardDto>> Handle(AddCreditCardRequest request, CancellationToken cancellationToken)
        {
            // Validar que las propiedades no sean null

            if(request.AccountId == null)
            {
                throw new PropertyCannotBeNullException(nameof(AddCreditCardRequest.AccountId));
            }

            if (request.Limit == null)
            {
                throw new PropertyCannotBeNullException(nameof(AddCreditCardRequest.Limit));
            }

            // Validar si existe el Account

            var account = await _accountRepository.GetByIdAsync((int)request.AccountId);

            if (account == null)
            {
                throw new EntityNotFoundException("Account");
            }

            // Validar que el Limit sea mayor que cero

            if (request.Limit <= 0)
            {
                throw new LimitNeedsBeGreaterThanZeroException();
            }

            var creditCard = new Domain.Entities.CreditCard((int)request.AccountId, (double)request.Limit);

            // Validar que el Number (CreditCard) sea unico

            bool numberExist;

            do
            {
                var creditCardTemp = _creditCardRepository.GetByNumberAsync(creditCard.Number);
                if (creditCardTemp.Result is null)
                {
                    numberExist = false;
                }
                else
                {
                    numberExist = true;
                    creditCard.UpdateNumber();
                }
            } while (numberExist == true);

            await _creditCardRepository.InsertAsync(creditCard);

            var creditCardDto = _mapper.Map<CreditCardDto>(creditCard);

            return new Response<CreditCardDto>(creditCardDto);

        }
    }
}
