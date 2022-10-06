using System.Net;
using Application.Exceptions;
using Application.Wrappers;
using Contracts.Repositories;
using Domain.Exceptions.Base;
using MediatR;

namespace Application.Features.Pix.Commands;

public class DeletePixRequest : IRequest<Response<bool>>
{
    public int Id { get; set; }
}

public class DeletePixHandler : IRequestHandler<DeletePixRequest, Response<bool>>
{
    private readonly IPixRepository _pixRepository;
    private readonly HttpClient _httpClient;
    
    public DeletePixHandler(
        IPixRepository pixRepository,
        IHttpClientFactory httpClientFactory
    )
    {
        _pixRepository = pixRepository;
        _httpClient = httpClientFactory.CreateClient("CentralBankApi");
    }

    public async Task<Response<bool>> Handle(
        DeletePixRequest request,
        CancellationToken cancellationToken)
    {
        var pix = await _pixRepository.GetByIdAsync(request.Id);

        if (pix == null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.Pix));
        }

        await _pixRepository.DeleteAsync(pix);

        var response = await _httpClient.DeleteAsync($"Pix/{pix.Key}", cancellationToken);
        
        if (response.StatusCode != HttpStatusCode.OK)
        {
            throw new BaseException(response.Content.ToString() ?? "Something was wrong");
        }

        return new Response<bool>(true);
    }
}