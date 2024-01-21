using FamilyFoundsApi.Domain.Dtos.Create;
using FamilyFoundsApi.Domain.Models;

namespace FamilyFoundsApi.Core.Contracts.Infrastructure;

public interface ICsvImporter
{
    List<Transaction> ImportIngTransactionsFromCsv(Stream fileStream);
    List<Transaction> ImportMillenniumTransactionsFromCsv(Stream fileStream);
}
