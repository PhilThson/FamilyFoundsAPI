using FamilyFoundsApi.Core.Contracts.Persistance.Repository;
using FamilyFoundsApi.Domain;

namespace FamilyFoundsApi.Core.Contracts.Persistance;

public interface IUnitOfWork
{
    ITransactionRepository Transaction { get; }
    ICategoryRepository Category { get; }
    IImportSourceRepository ImportSource { get; }

    Task SaveAsync();
    void AddEntity<T>(T instance) where T : class;
    void RemoveEntity<T>(T instance) where T : IRemoveable;
}
