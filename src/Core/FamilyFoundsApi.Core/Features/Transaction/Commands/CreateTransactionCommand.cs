using AutoMapper;
using FamilyFoundsApi.Core.Contracts.API;
using FamilyFoundsApi.Core.Contracts.Core;
using FamilyFoundsApi.Core.Contracts.Persistance;
using FamilyFoundsApi.Core.Exceptions;
using FamilyFoundsApi.Domain.Dtos.Create;
using FamilyFoundsApi.Domain.Dtos.Read;
using FamilyFoundsApi.Domain.Models;

namespace FamilyFoundsApi.Core.Features.Transaction.Commands;

public record CreateTransactionCommand(CreateTransactionDto TransactionDto) : IRequest<ReadTransactionDto>;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, ReadTransactionDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateTransactionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ReadTransactionDto> Handle(CreateTransactionCommand request)
    {
        var validator = new CreateTransactionCommandValidator();
        await validator.ValidateAsync(request);
        if (request.TransactionDto.CategoryId is not null)
        {
            _ = _unitOfWork.Category.FindById(request.TransactionDto.CategoryId) ??
                throw new NotFoundException(nameof(Category), request.TransactionDto.CategoryId);
        }
        var transactionEntity = _mapper.Map<Domain.Models.Transaction>(request.TransactionDto);
        _unitOfWork.AddEntity(transactionEntity);
        var transactionFromDb = await _unitOfWork.Transaction.GetByIdAsync(transactionEntity.Id);
        return _mapper.Map<ReadTransactionDto>(transactionFromDb);
    }
}

public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionCommandValidator()
    {
        AddRule(IsNotEmpty, "Przesłany model jest pusty");
        AddRule(HasDate, "Brak daty transakcji");
        AddRule(HasTitle, "Tytuł transakcji jest wymagany");
        AddRule(HasContractor, "Brak danych kontrahenta");
        AddRule(HasAmount, "Kwota musi być różna od 0");
        AddRule(HasCurrency, "Należ podać walutę (np. PLN)");
    }

    private Task<bool> IsNotEmpty(CreateTransactionCommand command) =>
        Task.FromResult(command.TransactionDto is not null);

    private Task<bool> HasDate(CreateTransactionCommand command) =>
        Task.FromResult(command.TransactionDto.Date.HasValue);

    private Task<bool> HasContractor(CreateTransactionCommand command) =>
        Task.FromResult(!string.IsNullOrEmpty(command.TransactionDto.Contractor));

    private Task<bool> HasTitle(CreateTransactionCommand command) =>
        Task.FromResult(!string.IsNullOrEmpty(command.TransactionDto.Title));

    private Task<bool> HasAmount(CreateTransactionCommand command) =>
        Task.FromResult(command.TransactionDto.Amount != 0.0);

    private Task<bool> HasCurrency(CreateTransactionCommand command) =>
        Task.FromResult(!string.IsNullOrEmpty(command.TransactionDto.Currency));
}
