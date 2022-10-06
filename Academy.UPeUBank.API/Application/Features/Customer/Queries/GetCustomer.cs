using Application.Wrappers;
using AutoMapper;
using Contracts.Repositories;
using DTOs;
using MediatR;

namespace Application.Features.Customer.Queries;

public class GetCustomerQuery : IRequest<Response<CustomerDto?>>
{
    public int Id { get; set; }
}

public class GetCustomerHandler : IRequestHandler<GetCustomerQuery, Response<CustomerDto?>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetCustomerHandler(
        ICustomerRepository customerRepository,
        IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<Response<CustomerDto?>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id);

        var customerDto = _mapper.Map<CustomerDto>(customer);

        return new Response<CustomerDto?>(customerDto);
    }
}