using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using FamilyFoundsApi.Domain.Models;

namespace FamilyFoundsApi.Infrastructure.Profiles;

public class IngTransactionMap : ClassMap<Transaction>
{
    public IngTransactionMap()
    {
        Map(t => t.Date).Name("Data transakcji");
        Map(t => t.PostingDate).Name("Data księgowania").Optional();
        Map(t => t.Contractor).Name("Dane kontrahenta")
            .TypeConverter<ContractorConverter>();
        Map(t => t.Title).Name("Tytuł").Optional();
        Map(t => t.ContractorAccountNumber).Name("Nr rachunku")
            .TypeConverter<ContractorAccountNumberConverter>()
            .Optional();
        Map(t => t.ContractorBankName).Name("Nazwa banku")
            .TypeConverter<ContractorBankNameConverter>()
            .Optional();
        Map(t => t.Description).Name("Szczegóły").Optional();
        Map(t => t.Amount).Name("Kwota transakcji (waluta rachunku)")
            .TypeConverterOption.CultureInfo(CultureInfo.CurrentCulture);
        Map(t => t.Currency).Name("Waluta").NameIndex(0);
        Map(t => t.Account).Name("Konto");
    }
}

class ContractorConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        return string.IsNullOrEmpty(text) ? "" : text.Trim();
    }
}

class ContractorAccountNumberConverter : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        return string.IsNullOrEmpty(text) ? null : text.Replace("'", "");
    }
}

class ContractorBankNameConverter : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        return string.IsNullOrEmpty(text) ? null : text;
    }
}

