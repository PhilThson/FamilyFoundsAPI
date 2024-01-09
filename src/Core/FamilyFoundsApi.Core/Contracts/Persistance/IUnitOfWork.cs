using FamilyFoundsApi.Core.Contracts.Persistance.Repository;

namespace FamilyFoundsApi.Core.Contracts.Persistance;

public interface IUnitOfWork
{
    ITransactionRepository Transaction { get; }
    ICategoryRepository Category { get; }
    IImportSourceRepository ImportSource { get; }

    Task SaveAsync();
}
