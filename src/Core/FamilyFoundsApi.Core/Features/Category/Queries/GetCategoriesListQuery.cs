using AutoMapper;
using FamilyFoundsApi.Domain;

namespace FamilyFoundsApi.Core;

public record GetCategoriesListQuery : IRequest<List<ReadCategoryDto>>;

public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, List<ReadCategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCategoriesListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<ReadCategoryDto>> Handle(GetCategoriesListQuery request)
    {
        var categories = await _unitOfWork.Category.FindAllAsync();
        return _mapper.Map<List<ReadCategoryDto>>(categories);
    }
}
