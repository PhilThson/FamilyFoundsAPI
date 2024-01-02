using FamilyFoundsApi.Core;
using FamilyFoundsApi.Domain;

namespace FamilyFoundsApi.Persistance;

public class ImportSourceRepository : BaseRepository<ImportSource>, IImportSourceRepository
{
    public ImportSourceRepository(FamilyFoundsDbContext dbContext) : base(dbContext)
    {
        
    }
}
