using System.Linq.Expressions;
using FamilyFoundsApi.Core.Contracts.Persistance.Repository;
using Microsoft.EntityFrameworkCore;

namespace FamilyFoundsApi.Persistance;

public abstract class BaseRepository<T> : IBaseRepository<T>
    where T : class
{
    protected DbSet<T> _DbSet { get; private set; }

    public BaseRepository(FamilyFoundsDbContext dbContext)
    {
        _DbSet = dbContext.Set<T>();
    }

    public Task<List<T>> FindAllAsync() =>
        _DbSet.AsNoTracking().ToListAsync();

    public IQueryable<T> FindByConditionAsync(
        Expression<Func<T, bool>> predicate, bool tracked = false)
    {
        var query = _DbSet.AsQueryable();
        if (!tracked) 
        {
            query = query.AsNoTracking();
        }
        return query.Where(predicate);
    }

    public T FindById(object id) => 
        _DbSet.Find(id);
}
