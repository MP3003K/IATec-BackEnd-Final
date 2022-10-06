using AutoMapper;
using Domain.Entities;
using DTOs;

namespace Application.Mappings;

public class AccountMapping : Profile
{
    public AccountMapping()
    {
        CreateMap<Account, AccountDto>()
            .ReverseMap();
    }
}