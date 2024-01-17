using System.Diagnostics;
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
    public List<Transaction> ImportIngTransactionsFromCsv(Stream fileStream)
    {
        using var reader = new StreamReader(fileStream, Encoding.UTF8);
        var config = new CsvConfiguration(CultureInfo.CurrentCulture)
        {
            IgnoreBlankLines = true, 
            Delimiter = ";",
            ShouldSkipRecord = _omitDocument
        };
        using var csv = new CsvReader(reader, config);
        csv.Context.RegisterClassMap<IngTransactionMap>();
        List<Transaction> records = [];
        //bool isHeaderRead = false;
        while (csv.Read())
        {
            Debug.WriteLine($"Linia: {csv.CurrentIndex}");
            var currentValue = csv.GetField<string>(0);
            if (string.IsNullOrEmpty(currentValue))
                continue;

            if (currentValue.StartsWith("Data transakcji"))
            {
                csv.ReadHeader();
                continue;
            }
            if (csv.HeaderRecord is not null)
            {
                if (!string.IsNullOrEmpty(csv.GetField("Kwota blokady/zwolnienie blokady"))) {
                    continue;
                }
                var transaction = csv.GetRecord<Transaction>() ??
                    throw new ImportException($"Błąd odczytu. Linia: ${csv.CurrentIndex}");

                records.Add(transaction);
            }
        }
        return records;
        //return ParseRecords(records, importDto.StartingLine, importDto.RequiredColumns);
    }

    public List<Transaction> ImportMilleniumTransactionsFromCsv(Stream fileStream)
    {
        using var reader = new StreamReader(fileStream);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
        csv.Read();
        csv.ReadHeader();

        var records = csv.GetRecords<Transaction>().ToList();
        return records;
        //return ParseRecords(records, importDto.StartingLine, importDto.RequiredColumns);
    }

    private static List<Transaction> ParseRecords(List<dynamic> records, int startingLine, string[] requiredColumns)
    {
        // Implement your logic to map dynamic records to your Transaction model
        // Use startingLine and requiredColumns to extract relevant data

        // Example:
        var transactions = records.Select(record => new Transaction
        {
            Amount = Convert.ToDouble(record["Amount"]),
            Contractor = record["Contractor"],
            // Map other properties
        }).ToList();

        return transactions;
    }
    private readonly ShouldSkipRecord _omitDocument = (r) => 
    {
        var field = r.Row.GetField(0);
        if (string.IsNullOrEmpty(field) || field.StartsWith("Dokument")) {
            return true;
        }
        return false;
    };
}
