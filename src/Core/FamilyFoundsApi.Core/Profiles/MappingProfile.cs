
using AutoMapper;
using FamilyFoundsApi.Domain.Dtos.Create;
using FamilyFoundsApi.Domain.Dtos.Read;
using FamilyFoundsApi.Domain.Models;

namespace FamilyFoundsApi.Core.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Transaction, ReadTransactionDto>();
        CreateMap<CreateTransactionDto, Transaction>()
            .ForMember(d => d.Category, o => o.Ignore());

        CreateMap<Category, ReadCategoryDto>();

        CreateMap<ImportSource, ReadImportSourceDto>();
    }
}
