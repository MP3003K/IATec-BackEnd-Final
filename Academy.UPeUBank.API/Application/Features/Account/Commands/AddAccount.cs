using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using Contracts.Repositories;
using DTOs;
using MediatR;

namespace Application.Features.Account.Commands;

public class AddAccountRequest : IRequest<Response<AccountDto>>
{
    public int? CustomerId { get; set; }
    public string? Code { get; set; }
}

public class AddAccountHandler : IRequestHandler<AddAccountRequest, Response<AccountDto>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public AddAccountHandler(
        IAccountRepository accountRepository,
        ICustomerRepository customerRepository,
        IMapper mapper)
    {
        _accountRepository = accountRepository;
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<Response<AccountDto>> Handle(
        AddAccountRequest request,
        CancellationToken cancellationToken)
    {
        if (request.CustomerId == null)
        {
            throw new PropertyCannotBeNullException(nameof(AddAccountRequest.CustomerId));
        }

        var customer = await _customerRepository.GetByIdAsync((int) request.CustomerId);

        if (customer == null)
        {
            throw new EntityNotFoundException("Customer");
        }

        if (string.IsNullOrEmpty(request.Code))
        {
            throw new PropertyCannotBeNullException(nameof(AddAccountRequest.Code));
        }

        var account = new Domain.Entities.Account((int) request.CustomerId, request.Code, 0);

        await _accountRepository.InsertAsync(account);

        var accountDto = _mapper.Map<AccountDto>(account);

        return new Response<AccountDto>(accountDto);
    }
}