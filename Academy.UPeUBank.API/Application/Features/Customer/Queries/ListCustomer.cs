using Application.Wrappers;
using AutoMapper;
using Contracts.Repositories;
using DTOs;
using MediatR;

namespace Application.Features.Customer.Queries;

public class ListCustomerQuery : IRequest<Response<IList<CustomerDto>>>
{
}

public class ListCustomerHandler : IRequestHandler<ListCustomerQuery, Response<IList<CustomerDto>>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public ListCustomerHandler(
        ICustomerRepository customerRepository,
        IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<Response<IList<CustomerDto>>> Handle(ListCustomerQuery request, CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.ListAllAsync();

        var customersDto = _mapper.Map<IList<CustomerDto>>(customers);

        return new Response<IList<CustomerDto>>(customersDto);
    }
}