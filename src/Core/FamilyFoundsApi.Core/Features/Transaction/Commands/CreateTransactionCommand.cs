using AutoMapper;
using FamilyFoundsApi.Domain;

namespace FamilyFoundsApi.Core;

public record CreateTransactionCommand(CreateTransactionDto transactionDto) : IRequest<ReadTransactionDto>;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, ReadTransactionDto>
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public CreateTransactionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ReadTransactionDto> Handle(CreateTransactionCommand request)
    {
        var validator = new CreateTransactionCommandValidator();
        await validator.ValidateAsync(request);
        var transactionEntity = _mapper.Map<Transaction>(request.transactionDto);
        _unitOfWork.Transaction.AddEntity(transactionEntity);
        await _unitOfWork.SaveAsync();
        var transactionFromDb = _unitOfWork.Transaction.FindById(transactionEntity.Id);
        return _mapper.Map<ReadTransactionDto>(transactionFromDb);
    }
}

public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionCommandValidator()
    {
        AddRule(HasDate, "Brak daty transakcji");
        AddRule(HasContractor, "Brak danych kontrahenta");
    }

    private Task<bool> HasDate(CreateTransactionCommand command)
    {
        return Task.FromResult(command.transactionDto.Date.HasValue);
    }

    private Task<bool> HasContractor(CreateTransactionCommand command)
    {
        return Task.FromResult(!string.IsNullOrEmpty(command.transactionDto.Contractor));
    }
}
