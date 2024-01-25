using FamilyFoundsApi.Domain.Enums;
using FamilyFoundsApi.Domain.Models;

namespace FamilyFoundsApi.Infrastructure;

public static class TransactionsExtensions
{
    public static short? GetCategoryFromIng(this Transaction transaction)
    {
        if (transaction.Amount >= 0m || string.IsNullOrEmpty(transaction.Contractor)) return null;

        string contractor = transaction.Contractor;
        if (contractor.Contains("biedronka", StringComparison.InvariantCultureIgnoreCase)
            || contractor.Contains("lidl", StringComparison.InvariantCultureIgnoreCase)
            || contractor.Contains("kaufland", StringComparison.InvariantCultureIgnoreCase)
            || contractor.Contains("carrefour", StringComparison.InvariantCultureIgnoreCase)
            || contractor.Contains("piekarnia", StringComparison.InvariantCultureIgnoreCase)
            || contractor.Contains("putka", StringComparison.InvariantCultureIgnoreCase)
            || contractor.Contains("cukiernia", StringComparison.InvariantCultureIgnoreCase)
            || contractor.Contains("zabka", StringComparison.InvariantCultureIgnoreCase)
            || contractor.Contains("stokrotka", StringComparison.InvariantCultureIgnoreCase)
            || contractor.Contains("pizza", StringComparison.InvariantCultureIgnoreCase)
            || contractor.Contains("sklep", StringComparison.InvariantCultureIgnoreCase))
        {
            return (short)CategoriesEnum.FOOD;
        }
        else if (contractor.Contains("apteka", StringComparison.InvariantCultureIgnoreCase)
            || contractor.Contains("cefarm", StringComparison.InvariantCultureIgnoreCase)
            || contractor.Contains("izabela", StringComparison.InvariantCultureIgnoreCase))
        {
            return (short)CategoriesEnum.HEALTH;
        }
        else if (contractor.Contains("rossmann", StringComparison.InvariantCultureIgnoreCase))
        {
            return (short)CategoriesEnum.CHEMICAL_AND_HYGIENE;
        }
        else if (contractor.Contains("smyk", StringComparison.InvariantCultureIgnoreCase)
            || contractor.Contains("pepco", StringComparison.InvariantCultureIgnoreCase))
        {
            return (short)CategoriesEnum.CHILDREN;
        }
        else if (contractor.Contains("traffic", StringComparison.InvariantCultureIgnoreCase)
            || contractor.Contains("paliw", StringComparison.InvariantCultureIgnoreCase))
        {
            return (short)CategoriesEnum.TRANSPORT;
        }
        else if (contractor.Contains("psb", StringComparison.InvariantCultureIgnoreCase)
            || contractor.Contains("leroy", StringComparison.InvariantCultureIgnoreCase))
        {
            return (short)CategoriesEnum.HOUSEHOLD;
        }
        else if (contractor.Contains("hbo", StringComparison.InvariantCultureIgnoreCase)
            || contractor.Contains("cofidis", StringComparison.InvariantCultureIgnoreCase))
        {
            return (short)CategoriesEnum.BILLS;
        }
        else
        {
            return (short)CategoriesEnum.OTHER;
        }
    }

    public static List<Transaction> AssignCategoriesToList(this IEnumerable<Transaction> records)
    {
        List<Transaction> result = [];
        foreach (var transaction in records)
        {
            transaction.CategoryId = GetCategoryFromMillennium(transaction.Title, transaction.Amount);
            result.Add(transaction);
        }
        return result;
    }
    
    private static short? GetCategoryFromMillennium(string title, decimal amount)
    {
        if (amount >= 0m || string.IsNullOrEmpty(title)) return null;

        if (title.Contains("biedronka", StringComparison.InvariantCultureIgnoreCase)
            || title.Contains("lidl", StringComparison.InvariantCultureIgnoreCase)
            || title.Contains("kaufland", StringComparison.InvariantCultureIgnoreCase)
            || title.Contains("carrefour", StringComparison.InvariantCultureIgnoreCase)
            || title.Contains("piekarnia", StringComparison.InvariantCultureIgnoreCase)
            || title.Contains("putka", StringComparison.InvariantCultureIgnoreCase)
            || title.Contains("cukiernia", StringComparison.InvariantCultureIgnoreCase)
            || title.Contains("zabka", StringComparison.InvariantCultureIgnoreCase)
            || title.Contains("stokrotka", StringComparison.InvariantCultureIgnoreCase)
            || title.Contains("pizza", StringComparison.InvariantCultureIgnoreCase)
            || title.Contains("turczynski", StringComparison.InvariantCultureIgnoreCase)
            || title.Contains("sklep", StringComparison.InvariantCultureIgnoreCase))
        {
            return (short)CategoriesEnum.FOOD;
        }
        else if (title.Contains("apteka", StringComparison.InvariantCultureIgnoreCase)
            || title.Contains("cefarm", StringComparison.InvariantCultureIgnoreCase)
            || title.Contains("izabela", StringComparison.InvariantCultureIgnoreCase))
        {
            return (short)CategoriesEnum.HEALTH;
        }
        else if (title.Contains("rossmann", StringComparison.InvariantCultureIgnoreCase))
        {
            return (short)CategoriesEnum.CHEMICAL_AND_HYGIENE;
        }
        else if (title.Contains("smyk", StringComparison.InvariantCultureIgnoreCase)
            || title.Contains("pepco", StringComparison.InvariantCultureIgnoreCase))
        {
            return (short)CategoriesEnum.CHILDREN;
        }
        else if (title.Contains("traffic", StringComparison.InvariantCultureIgnoreCase)
            || title.Contains("paliw", StringComparison.InvariantCultureIgnoreCase))
        {
            return (short)CategoriesEnum.TRANSPORT;
        }
        else if (title.Contains("psb", StringComparison.InvariantCultureIgnoreCase)
            || title.Contains("leroy", StringComparison.InvariantCultureIgnoreCase))
        {
            return (short)CategoriesEnum.HOUSEHOLD;
        }
        else if (title.Contains("faktura", StringComparison.InvariantCultureIgnoreCase)
            || title.Contains("p/", StringComparison.InvariantCultureIgnoreCase)
            || title.Contains("/ktr/", StringComparison.InvariantCultureIgnoreCase)
            || title.Contains("fvs/", StringComparison.InvariantCultureIgnoreCase)
            || title.Contains("odpady", StringComparison.InvariantCultureIgnoreCase)
            || title.Contains("autopay", StringComparison.InvariantCultureIgnoreCase)
            || title.Contains("player", StringComparison.InvariantCultureIgnoreCase))
        {
            return (short)CategoriesEnum.BILLS;
        }
        else if (title.Contains("mohito", StringComparison.InvariantCultureIgnoreCase)
            || title.Contains("vinted", StringComparison.InvariantCultureIgnoreCase)
            || title.Contains("sinsay", StringComparison.InvariantCultureIgnoreCase)
            || title.Contains("reserved", StringComparison.InvariantCultureIgnoreCase))
        {
            return (short)CategoriesEnum.CLOTHES;
        }
        else
        {
            return (short)CategoriesEnum.OTHER;
        }
    }
}
