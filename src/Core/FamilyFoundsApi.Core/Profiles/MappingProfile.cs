
using AutoMapper;
using FamilyFoundsApi.Domain.Dtos.Create;
using FamilyFoundsApi.Domain.Dtos.Read;
using FamilyFoundsApi.Domain.Dtos.Update;
using FamilyFoundsApi.Domain.Models;

namespace FamilyFoundsApi.Core.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Transaction, ReadTransactionDto>();
        CreateMap<CreateTransactionDto, Transaction>()
            .ForMember(d => d.Category, o => o.Ignore())
            .ForMember(d => d.ImportSource, o => o.Ignore());
        CreateMap<UpdateTransactionDto, Transaction>()
            .ForMember(d => d.Category, o => o.Ignore())
            .ForMember(d => d.ImportSource, o => o.Ignore());

        CreateMap<Category, ReadCategoryDto>();

        CreateMap<ImportSource, ReadImportSourceDto>();
    }
}
