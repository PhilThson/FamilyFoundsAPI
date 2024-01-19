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
            BankEnum.MILLENIUM => _csvImporter.ImportMilleniumTransactionsFromCsv(request.FileStream),
            _ => throw new ValidationException("Nieobsługiwany bank")
        };
        //TODO:
        //logika sprawdzania czy transakcja juz nie istnieje i ew. update
        await _unitOfWork.AddEntitiesAsync(importedTransactions);

        return importedTransactions.Count;
    }
}
