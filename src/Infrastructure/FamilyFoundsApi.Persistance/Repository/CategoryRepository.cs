using FamilyFoundsApi.Core;
using FamilyFoundsApi.Domain;

namespace FamilyFoundsApi.Persistance;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(FamilyFoundsDbContext dbContext) : base(dbContext)
    {
        
    }
}
