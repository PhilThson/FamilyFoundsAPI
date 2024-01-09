using System.Linq.Expressions;

namespace FamilyFoundsApi.Core.Contracts.Persistance.Repository;

public interface IBaseRepository<T>
{
    Task<List<T>> FindAllAsync();
    IQueryable<T> FindByConditionAsync(
        Expression<Func<T, bool>> predicate, bool tracked = false);
    T FindById(object id);
    void AddEntity(T entity);
}
