using System.Net;
using System.Net.Http.Json;
using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using Contracts.Repositories;
using Domain.Exceptions.Base;
using DTOs;
using MediatR;
using Newtonsoft.Json;

namespace Application.Features.Pix.Commands;

public class AddPixRequest : IRequest<Response<PixDto>>
{
    public int? AccountId { get; set; }
    public string? Key { get; set; }
}

public class AddPixHandler : IRequestHandler<AddPixRequest, Response<PixDto>>
{
    private readonly IPixRepository _pixRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    private readonly HttpClient _httpClient;

    public AddPixHandler(
        IPixRepository pixRepository,
        IAccountRepository accountRepository,
        IHttpClientFactory httpClientFactory,
        IMapper mapper)
    {
        _pixRepository = pixRepository;
        _accountRepository = accountRepository;
        _mapper = mapper;
        _httpClient = httpClientFactory.CreateClient("CentralBankApi");
    }

    public async Task<Response<PixDto>> Handle(
        AddPixRequest request,
        CancellationToken cancellationToken)
    {
        if (request.AccountId == null)
        {
            throw new PropertyCannotBeNullException(nameof(AddPixRequest.AccountId));
        }

        var account = await _accountRepository.GetByIdAsync((int) request.AccountId);

        if (account == null)
        {
            throw new EntityNotFoundException("Account");
        }

        if (string.IsNullOrEmpty(request.Key))
        {
            throw new PropertyCannotBeNullException(nameof(AddPixRequest.Key));
        }

        var pix = new Domain.Entities.Pix((int) request.AccountId, request.Key);

        await _pixRepository.InsertAsync(pix);

        var pixContent = new
        {
            bankCode = "002",
            key = pix.Key
        };

        var response = await _httpClient.PostAsJsonAsync("Pix", pixContent, cancellationToken);

        if (response.StatusCode != HttpStatusCode.Created)
        {
            throw new BaseException(response.Content.ToString() ?? "Something was wrong");
        }

        var pixDto = _mapper.Map<PixDto>(pix);

        return new Response<PixDto>(pixDto);
    }
}