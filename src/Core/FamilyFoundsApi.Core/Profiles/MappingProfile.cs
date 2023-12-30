
using AutoMapper;
using FamilyFoundsApi.Domain;

namespace FamilyFoundsApi.Core;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Transaction, ReadTransactionDto>();
        CreateMap<CreateTransactionDto, Transaction>();

        CreateMap<Category, ReadCategoryDto>();
    }
}
