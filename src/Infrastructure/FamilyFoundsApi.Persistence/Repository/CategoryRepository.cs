using FamilyFoundsApi.Core.Contracts.Persistance.Repository;
using FamilyFoundsApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FamilyFoundsApi.Persistence;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(FamilyFoundsDbContext dbContext) : base(dbContext)
    {
        
    }

    public Task<Category> GetByNameAsync(string name, bool tracked = false) =>
        FindByConditionAsync(c => c.Name == name).FirstOrDefaultAsync();

    public bool ExistByName(string name) => _DbSet.Any(c => c.Name == name);
}
