
using AutoMapper;
using FamilyFoundsApi.Core.Contracts.API;
using FamilyFoundsApi.Core.Contracts.Persistance;
using FamilyFoundsApi.Domain.Dtos.Read;
using FamilyFoundsApi.Domain.Models;

namespace FamilyFoundsApi.Core;

public record CreateCategoryCommand(string CategoryName) : IRequest<ReadCategoryDto>;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ReadCategoryDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ReadCategoryDto> Handle(CreateCategoryCommand request)
    {
        if (string.IsNullOrEmpty(request.CategoryName))
            throw new ArgumentException(nameof(request.CategoryName));

        if (!_unitOfWork.Category.ExistByName(request.CategoryName))
        {
            var newCategory = new Category { Name = request.CategoryName };
            _unitOfWork.AddEntity(newCategory);
        }
        var existingCategoty = await _unitOfWork.Category.GetByNameAsync(request.CategoryName);
        return _mapper.Map<ReadCategoryDto>(existingCategoty);
    }
}
