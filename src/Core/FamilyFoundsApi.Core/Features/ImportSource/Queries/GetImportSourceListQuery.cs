using AutoMapper;
using FamilyFoundsApi.Core.Contracts.API;
using FamilyFoundsApi.Core.Contracts.Persistance;
using FamilyFoundsApi.Domain.Dtos.Read;

namespace FamilyFoundsApi.Core;

public record GetImportSourceListQuery : IRequest<List<ReadImportSourceDto>>;

public class GetImportSourceListQueryHandler : IRequestHandler<GetImportSourceListQuery, List<ReadImportSourceDto>>
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    public GetImportSourceListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<ReadImportSourceDto>> Handle(GetImportSourceListQuery request)
    {
        var importSources = await _unitOfWork.ImportSource.FindAllAsync();
        return _mapper.Map<List<ReadImportSourceDto>>(importSources);
    }
}
