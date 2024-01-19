using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using FamilyFoundsApi.Core.Contracts.Infrastructure;
using FamilyFoundsApi.Core.Exceptions;
using FamilyFoundsApi.Domain.Models;

namespace FamilyFoundsApi.Infrastructure.FileImport;

public class CsvImporter : ICsvImporter
{
    private const string ING_HEADER_INDICATOR = "Data transakcji";
    private const string ING_BLOCKAGE_INDICATOR = "Kwota blokady/zwolnienie blokady";
    public List<Transaction> ImportIngTransactionsFromCsv(Stream fileStream)
    {
        using var reader = new StreamReader(fileStream, Encoding.UTF8);
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";"
        };
        using var csv = new CsvReader(reader, config);
        csv.Context.RegisterClassMap<IngTransactionMap>();
        List<Transaction> records = [];
        try 
        {
            while (csv.Read())
            {
                var currentValue = csv.GetField<string>(0) ?? "";
                if (currentValue.StartsWith(ING_HEADER_INDICATOR))
                {
                    csv.ReadHeader();
                    continue;
                }
                if (csv.HeaderRecord is not null)
                {
                    if (!string.IsNullOrEmpty(csv.GetField(ING_BLOCKAGE_INDICATOR))) {
                        continue;
                    }
                    var transaction = csv.GetRecord<Transaction>() ??
                        throw new ImportException($"Błąd odczytu rekordu. Linia: ${csv.CurrentIndex}");

                    records.Add(transaction);
                }
            }
        }
        catch (Exception e)
        {
            throw new ImportException(e);
        }
        return records;
    }

    public List<Transaction> ImportMilleniumTransactionsFromCsv(Stream fileStream)
    {
        using var reader = new StreamReader(fileStream);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
        csv.Read();
        csv.ReadHeader();

        var records = csv.GetRecords<Transaction>().ToList();
        return records;
    }
}
