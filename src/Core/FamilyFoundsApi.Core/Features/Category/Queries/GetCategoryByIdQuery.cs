using AutoMapper;
using FamilyFoundsApi.Domain;

namespace FamilyFoundsApi.Core;

public record GetCategoryByIdQuery(short id) : IRequest<ReadCategoryDto>;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, ReadCategoryDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<ReadCategoryDto> Handle(GetCategoryByIdQuery request)
    {
        var category = _unitOfWork.Category.FindById(request.id) ??
            throw new NotFoundException(nameof(Category), request.id);
        
        return Task.FromResult(_mapper.Map<ReadCategoryDto>(category));
    }
}