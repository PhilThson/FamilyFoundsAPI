using FamilyFoundsApi.Core;
using Microsoft.EntityFrameworkCore;

namespace FamilyFoundsApi.Persistance;

public class BaseRepository<T> : IBaseRepository<T>
    where T : class
{
    protected DbSet<T> DbSet { get; private set; }

    public BaseRepository(FamilyFoundsDbContext dbContext)
    {
        DbSet = dbContext.Set<T>();
    }

    public Task<List<T>> FindAllAsync() =>
        DbSet.AsNoTracking().ToListAsync();

    public T FindById(object id) => 
        DbSet.Find(id);

    public void AddEntity(T entity) =>
        DbSet.Add(entity);
}
