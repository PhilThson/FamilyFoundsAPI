using FamilyFoundsApi.Core.Contracts.Persistance.Repository;
using FamilyFoundsApi.Domain.Models;

namespace FamilyFoundsApi.Persistance;

public class ImportSourceRepository : BaseRepository<ImportSource>, IImportSourceRepository
{
    public ImportSourceRepository(FamilyFoundsDbContext dbContext) : base(dbContext)
    {
        
    }
}
