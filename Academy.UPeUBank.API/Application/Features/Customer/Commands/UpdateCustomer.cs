using Application.Exceptions;
using Application.Wrappers;
using AutoMapper;
using Contracts.Repositories;
using DTOs;
using MediatR;

namespace Application.Features.Customer.Commands;

public class UpdateCustomerRequest : CustomerDto, IRequest<Response<CustomerDto>>
{
}

public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerRequest, Response<CustomerDto>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public UpdateCustomerHandler(
        ICustomerRepository customerRepository,
        IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<Response<CustomerDto>> Handle(
        UpdateCustomerRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Name))
        {
            throw new PropertyCannotBeNullException(nameof(UpdateCustomerRequest.Name));
        }

        var customer = await _customerRepository.GetByIdAsync(request.Id);

        if (customer == null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.Customer));
        }
        
        customer.Update(request.Name);

        await _customerRepository.UpdateAsync(customer);

        var customerDto = _mapper.Map<CustomerDto>(customer);

        return new Response<CustomerDto>(customerDto);
    }
}