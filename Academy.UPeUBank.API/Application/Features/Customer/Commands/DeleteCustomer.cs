using Application.Exceptions;
using Application.Wrappers;
using Contracts.Repositories;
using MediatR;

namespace Application.Features.Customer.Commands;

public class DeleteCustomerRequest : IRequest<Response<bool>>
{
    public int Id { get; set; }
}

public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerRequest, Response<bool>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IAccountRepository _accountRepository;

    public DeleteCustomerHandler(
        ICustomerRepository customerRepository,
        IAccountRepository accountRepository)
    {
        _customerRepository = customerRepository;
        _accountRepository = accountRepository;
    }

    public async Task<Response<bool>> Handle(
        DeleteCustomerRequest request,
        CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id);

        if (customer == null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.Customer));
        }

        var accounts = await _accountRepository.ListByCustomerIdAsync(request.Id);

        if (accounts.Any())
        {
            throw new EntityCannotBeRemovedByDependencyException("Customer");
        }

        await _customerRepository.DeleteAsync(customer);

        return new Response<bool>(true);
    }
}