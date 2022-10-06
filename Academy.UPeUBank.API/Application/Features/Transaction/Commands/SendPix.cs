using System.Net;
using System.Net.Http.Json;
using Application.Exceptions;
using Application.Wrappers;
using Contracts.Repositories;
using Contracts.Services;
using Domain.Exceptions.Base;
using MediatR;

namespace Application.Features.Transaction.Commands;

public class SendPixRequest : IRequest<Response<bool>>
{
    public string? Account { get; set; }
    public string? Key { get; set; }
    public double? Value { get; set; }
}

public class SendPixHandler : IRequestHandler<SendPixRequest, Response<bool>>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IPixRepository _pixRepository;
    private readonly IAccountService _accountService;

    private readonly HttpClient _httpClient;

    public SendPixHandler(
        ITransactionRepository transactionRepository,
        IAccountService accountService,
        IPixRepository pixRepository,
        IHttpClientFactory httpClientFactory,
        IAccountRepository accountRepository)
    {
        _transactionRepository = transactionRepository;
        _accountService = accountService;
        _pixRepository = pixRepository;
        _accountRepository = accountRepository;
        _httpClient = httpClientFactory.CreateClient("CentralBankApi");
    }

    public async Task<Response<bool>> Handle(
        SendPixRequest request,
        CancellationToken cancellationToken)
    {
        if (request.Account == null)
        {
            throw new PropertyCannotBeNullException(nameof(SendPixRequest.Account));
        }

        if (request.Key == null)
        {
            throw new PropertyCannotBeNullException(nameof(SendPixRequest.Key));
        }

        if (request.Value is null or <= 0)
        {
            throw new ValueNeedsBeGreaterThanZeroException();
        }

        var account = await _accountRepository.GetByCodeAsync(request.Account);

        if (account == null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.Account));
        }


        if (account.Balance < request.Value)
        {
            throw new ThereIsNotEnoughBalanceForThisAccountException();
        }

        var transaction = new Domain.Entities.Transaction(account.Id, -(double) request.Value);

        await _transactionRepository.InsertAsync(transaction);

        await _accountService.UpdateCurrentBalanceAsync(account.Id);

        var pixContent = new
        {
            key = request.Key,
            value = request.Value
        };
        
        var response = await _httpClient.PostAsJsonAsync("Transaction", pixContent, cancellationToken);

        if (response.StatusCode != HttpStatusCode.Created)
        {
            throw new BaseException(response.Content.ToString() ?? "Something was wrong");
        }

        return new Response<bool>(true);
    }
}