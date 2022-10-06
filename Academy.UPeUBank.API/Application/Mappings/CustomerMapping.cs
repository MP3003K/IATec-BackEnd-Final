using AutoMapper;
using Domain.Entities;
using DTOs;

namespace Application.Mappings;

public class CustomerMapping : Profile
{
    public CustomerMapping()
    {
        CreateMap<Customer, CustomerDto>()
            .ReverseMap();
    }
}