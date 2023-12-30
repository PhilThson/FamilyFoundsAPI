namespace FamilyFoundsApi.Core;

public interface IUnitOfWork
{
    ITransactionRepository Transaction { get; }
    ICategoryRepository Category { get; }

    Task SaveAsync();
}
