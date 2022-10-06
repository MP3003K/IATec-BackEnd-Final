using AutoMapper;
using Domain.Exceptions.Base;
using DTOs.Exceptions;

namespace Application.Mappings;

public class ExceptionMapping : Profile
{
    public ExceptionMapping()
    {
        CreateMap<ExceptionDto, BaseException>()
            .ReverseMap();
    }
}