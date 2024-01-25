using FamilyFoundsApi.Core.Contracts.Persistance.Repository;
using FamilyFoundsApi.Domain;

namespace FamilyFoundsApi.Core.Contracts.Persistance;

public interface IUnitOfWork
{
    ITransactionRepository Transaction { get; }
    ICategoryRepository Category { get; }
    IImportSourceRepository ImportSource { get; }

    Task SaveAsync();
    void AddEntity<T>(T instance);
    Task<int> AddEntitiesAsync<T>(IEnumerable<T> entities) where T : class;
    void AttachEntity<T>(T instance, List<string> modifiedProperties);
    void RemoveEntity<T>(T instance) where T : IRemoveable;
}
