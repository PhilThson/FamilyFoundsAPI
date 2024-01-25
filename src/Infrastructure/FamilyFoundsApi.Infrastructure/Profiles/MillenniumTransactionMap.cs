using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using FamilyFoundsApi.Domain.Models;

namespace FamilyFoundsApi.Infrastructure.Profiles;

public class MillenniumTransactionMap : ClassMap<Transaction>
{
    public MillenniumTransactionMap()
    {
        Map(t => t.Date).Name("Data transakcji");
        Map(t => t.PostingDate).Name("Data rozliczenia")
            .Optional();
        Map(t => t.Contractor).Name("Odbiorca/Zleceniodawca")
            .TypeConverter<ContractorConverter>();
        Map(t => t.Title).Name("Opis")
            .Optional();
        Map(t => t.ContractorAccountNumber).Name("Na konto/Z konta")
            .TypeConverter<ContractorAccountNumberConverter>()
            .Optional();
        Map(t => t.Description).Name("Rodzaj transakcji")
            .Optional();
        Map(t => t.Amount).Convert(_AmountConverter);
        Map(t => t.Currency).Name("Waluta");
        Map(t => t.Account).Name("Numer rachunku/karty")
            .TypeConverter<AccountConverter>();
    }

    private static readonly ConvertFromString<decimal> _AmountConverter = (args) => 
    {
        var debit = args.Row.GetField("Obciążenia");
        var credit = args.Row.GetField("Uznania");
        return string.IsNullOrEmpty(debit) ? 
            Convert.ToDecimal(credit, CultureInfo.InvariantCulture)
            : Convert.ToDecimal(debit, CultureInfo.InvariantCulture);
    };
}

class AccountConverter : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrEmpty(text))
        {
            return "Nieznane konto Millennium";
        }
        return text.Substring(0, 4) switch 
        {
            "PL16" => "Konto 360",
            "PL12" => "Konto Oszczędnościowe Profit",
            _ => "Nieznane konto Millennium"
        };
    }
}
