using Application.Wrappers;
using AutoMapper;
using Contracts.Repositories;
using DTOs;
using MediatR;

namespace Application.Features.Account.Queries;

public class GetAccountQuery : IRequest<Response<AccountDto?>>
{
    public int Id { get; set; }
}

public class GetAccountHandler : IRequestHandler<GetAccountQuery, Response<AccountDto?>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public GetAccountHandler(
        IAccountRepository accountRepository,
        IMapper mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    public async Task<Response<AccountDto?>> Handle(GetAccountQuery request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetFullByIdAsync(request.Id);

        var accountDto = _mapper.Map<AccountDto>(account);

        return new Response<AccountDto?>(accountDto);
    }
}