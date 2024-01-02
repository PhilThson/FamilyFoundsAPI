namespace FamilyFoundsApi.Core;

public interface IUnitOfWork
{
    ITransactionRepository Transaction { get; }
    ICategoryRepository Category { get; }
    IImportSourceRepository ImportSource { get; }

    Task SaveAsync();
}
