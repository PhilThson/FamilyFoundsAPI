using AutoMapper;
using FamilyFoundsApi.Core.Contracts.API;
using FamilyFoundsApi.Core.Contracts.Core;
using FamilyFoundsApi.Core.Contracts.Persistance;
using FamilyFoundsApi.Core.Helpers;
using FamilyFoundsApi.Core.Exceptions;
using FamilyFoundsApi.Domain.Dtos.Read;
using FamilyFoundsApi.Domain.Dtos.Update;

namespace FamilyFoundsApi.Core.Features.Transaction.Commands;

public record UpdateTransactionCommand(UpdateTransactionDto UpdateTransactionDto) : IRequest<ReadTransactionDto>;

public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, ReadTransactionDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public UpdateTransactionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ReadTransactionDto> Handle(UpdateTransactionCommand request)
    {
        var validator = new UpdateTransactionCommandValidator();
        await validator.ValidateAsync(request);
        var existingTransaction = await _unitOfWork.Transaction.GetByIdAsync(request.UpdateTransactionDto.Id) ??
            throw new NotFoundException(nameof(Domain.Models.Transaction), request.UpdateTransactionDto.Id);

        var updateTransaction = _mapper.Map<Domain.Models.Transaction>(request.UpdateTransactionDto);
        var category = await _unitOfWork.Category.GetByNameAsync(request.UpdateTransactionDto.Category);
        if (category is not null)
        {
            updateTransaction.CategoryId = category.Id;
            updateTransaction.Category = category;
        }

        if (updateTransaction == existingTransaction) 
        {
            return _mapper.Map<ReadTransactionDto>(existingTransaction);
        }
        var modifiedProperties = EntitiesHelper.GetModifiedProperties(existingTransaction, updateTransaction);
        _unitOfWork.AttachEntity(updateTransaction, modifiedProperties);

        return _mapper.Map<ReadTransactionDto>(updateTransaction);
    }

    public class UpdateTransactionCommandValidator : AbstractValidator<UpdateTransactionCommand>
    {
        public UpdateTransactionCommandValidator()
        {
            AddRule(IsNotEmpty, "Przesłany model jest pusty");
        }

        private static Task<bool> IsNotEmpty(UpdateTransactionCommand command) =>
            Task.FromResult(command.UpdateTransactionDto is not null);
    }
}
