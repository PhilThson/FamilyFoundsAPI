using AutoMapper;
using FamilyFoundsApi.Core.Contracts.API;
using FamilyFoundsApi.Core.Contracts.Core;
using FamilyFoundsApi.Core.Contracts.Persistance;
using FamilyFoundsApi.Core.Helpers;
using FamilyFoundsApi.Core.Exceptions;
using FamilyFoundsApi.Domain.Dtos.Read;
using FamilyFoundsApi.Domain.Dtos.Update;
using FamilyFoundsApi.Domain.Models;

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
        if (updateTransaction.CategoryId is not null) 
        {
            _ = _unitOfWork.Category.FindById(updateTransaction.CategoryId) ??
                throw new NotFoundException(nameof(Category), updateTransaction.CategoryId);
        }

        if (updateTransaction == existingTransaction) 
        {
            return _mapper.Map<ReadTransactionDto>(existingTransaction);
        }
        var modifiedProperties = EntitiesHelper.GetModifiedProperties(existingTransaction, updateTransaction);
        _unitOfWork.AttachEntity(updateTransaction, modifiedProperties);
        var transactionFromDb = await _unitOfWork.Transaction.GetByIdAsync(updateTransaction.Id);
        return _mapper.Map<ReadTransactionDto>(transactionFromDb);
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
