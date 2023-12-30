namespace FamilyFoundsApi.Core;

public interface IBaseRepository<T>
{
    Task<List<T>> FindAllAsync();
    T FindById(object id);
    void AddEntity(T entity);
}
