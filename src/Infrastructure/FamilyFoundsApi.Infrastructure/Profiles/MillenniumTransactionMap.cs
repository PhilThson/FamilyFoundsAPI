using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using FamilyFoundsApi.Domain.Models;
using System.Security.Cryptography;
using System.Text;

namespace FamilyFoundsApi.Infrastructure.Profiles;

public class MillenniumTransactionMap : ClassMap<Transaction>
{
    public MillenniumTransactionMap()
    {
        Map(t => t.Date).Name("Data transakcji");
        Map(t => t.PostingDate).Name("Data rozliczenia").Optional();
        Map(t => t.Contractor).Name("Odbiorca/Zleceniodawca")
            .TypeConverter<ContractorConverter>();
        Map(t => t.Title).Name("Opis").Optional();
        Map(t => t.ContractorAccountNumber).Name("Na konto/Z konta")
            .TypeConverter<ContractorAccountNumberConverter>()
            .Optional();
        Map(t => t.Description).Name("Rodzaj transakcji").Optional();
        Map(t => t.Amount).Convert(_AmountConverter);
        Map(t => t.Currency).Name("Waluta");
        Map(t => t.Account).Name("Numer rachunku/karty");
        Map(t => t.Number).Convert(_NumberConverter);
    }

    private static readonly ConvertFromString<decimal> _AmountConverter = (args) => 
    {
        var debit = args.Row.GetField("Obciążenia");
        var credit = args.Row.GetField("Uznania");
        return string.IsNullOrEmpty(debit) ? 
            Convert.ToDecimal(credit, CultureInfo.InvariantCulture)
            : Convert.ToDecimal(debit, CultureInfo.InvariantCulture);
    };

    private static readonly ConvertFromString<string> _NumberConverter = (args) =>
    {
        var date = args.Row.GetField("Data transakcji") ?? "";
        var transactionType = args.Row.GetField("Rodzaj transakcji") ?? "";
        var debit = args.Row.GetField("Obciążenia") ?? "";
        var credit = args.Row.GetField("Uznania") ?? "";

        var concat = $"{date}-{transactionType}-{debit}-{credit}";
        var inputBytes = Encoding.UTF8.GetBytes(concat);
        byte[] hashBytes = MD5.HashData(inputBytes);
        return Convert.ToHexString(hashBytes);
    };
}
