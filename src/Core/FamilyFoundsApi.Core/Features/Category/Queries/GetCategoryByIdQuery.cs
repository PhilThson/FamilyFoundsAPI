using AutoMapper;
using FamilyFoundsApi.Core.Contracts.API;
using FamilyFoundsApi.Core.Contracts.Persistance;
using FamilyFoundsApi.Core.Exceptions;
using FamilyFoundsApi.Domain.Dtos.Read;
using FamilyFoundsApi.Domain.Models;

namespace FamilyFoundsApi.Core;

public record GetCategoryByIdQuery(short Id) : IRequest<ReadCategoryDto>;

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
        var category = _unitOfWork.Category.FindById(request.Id) ??
            throw new NotFoundException(nameof(Category), request.Id);
        
        return Task.FromResult(_mapper.Map<ReadCategoryDto>(category));
    }
}