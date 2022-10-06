using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using Contracts.Repositories;
using DTOs;
using MediatR;

namespace Application.Features.Customer.Commands;

public class AddCustomerRequest : IRequest<Response<CustomerDto>>
{
    public string? Name { get; set; }
}

public class AddCustomerHandler : IRequestHandler<AddCustomerRequest, Response<CustomerDto>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public AddCustomerHandler(
        ICustomerRepository customerRepository,
        IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<Response<CustomerDto>> Handle(
        AddCustomerRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Name))
        {
            throw new PropertyCannotBeNullException(nameof(AddCustomerRequest.Name));
        }

        var customer = new Domain.Entities.Customer(request.Name);

        await _customerRepository.InsertAsync(customer);

        var customerDto = _mapper.Map<CustomerDto>(customer);

        return new Response<CustomerDto>(customerDto);
    }
}