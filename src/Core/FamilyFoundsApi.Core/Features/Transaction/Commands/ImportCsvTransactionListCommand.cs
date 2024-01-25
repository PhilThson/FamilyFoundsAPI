using FamilyFoundsApi.Core.Contracts.API;
using FamilyFoundsApi.Core.Contracts.Infrastructure;
using FamilyFoundsApi.Core.Contracts.Persistance;
using FamilyFoundsApi.Core.Exceptions;
using FamilyFoundsApi.Domain.Enums;

namespace FamilyFoundsApi.Core.Features.Transaction.Commands;

public record ImportCsvTransactionListCommand(Stream FileStream, BankEnum Bank) : IRequest<int>;

public class ImportCsvTransactionListCommandHandler : IRequestHandler<ImportCsvTransactionListCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICsvImporter _csvImporter;

    public ImportCsvTransactionListCommandHandler(IUnitOfWork unitOfWork, ICsvImporter csvImporter)
    {
        _unitOfWork = unitOfWork;
        _csvImporter = csvImporter;
    }

    public async Task<int> Handle(ImportCsvTransactionListCommand request)
    {
        if (request.FileStream is null)
            throw new ValidationException("Brak pliku źródłowego");

        var importedTransactions = request.Bank switch
        {
            BankEnum.ING => _csvImporter.ImportIngTransactionsFromCsv(request.FileStream),
            BankEnum.MILLENNIUM => _csvImporter.ImportMillenniumTransactionsFromCsv(request.FileStream),
            _ => throw new ValidationException("Nieobsługiwane źródło importu")
        };

        var importedTransactionsCount = await AddNewTransactions(importedTransactions);
        return importedTransactionsCount;
    }

    private Task<int> AddNewTransactions(List<Domain.Models.Transaction> importedTransactions)
    {
        var newTransactions = importedTransactions
            .Where(t => _unitOfWork.Transaction.IsNumberUnique(t.Number));

        if (newTransactions.Any())
        {
            return _unitOfWork.AddEntitiesAsync(newTransactions);
        }
        return Task.FromResult(0);
    }
}
