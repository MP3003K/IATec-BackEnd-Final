using Application.Wrappers;
using AutoMapper;
using Contracts.Repositories;
using DTOs;
using MediatR;

namespace Application.Features.Account.Queries;

public class ListAccountQuery : IRequest<Response<IList<AccountDto>>>
{
}

public class ListAccountHandler : IRequestHandler<ListAccountQuery, Response<IList<AccountDto>>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public ListAccountHandler(
        IAccountRepository accountRepository,
        IMapper mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    public async Task<Response<IList<AccountDto>>> Handle(ListAccountQuery request, CancellationToken cancellationToken)
    {
        var accounts = await _accountRepository.ListFullAllAsync();

        var accountsDto = _mapper.Map<IList<AccountDto>>(accounts);

        return new Response<IList<AccountDto>>(accountsDto);
    }
}