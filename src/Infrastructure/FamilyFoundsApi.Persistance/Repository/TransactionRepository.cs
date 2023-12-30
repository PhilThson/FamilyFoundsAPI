using FamilyFoundsApi.Core;
using FamilyFoundsApi.Domain;

namespace FamilyFoundsApi.Persistance;

public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(FamilyFoundsDbContext dbContext) : base(dbContext)
    {
        
    }
}
