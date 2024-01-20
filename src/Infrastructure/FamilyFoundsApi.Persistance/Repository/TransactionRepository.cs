using FamilyFoundsApi.Core.Contracts.Persistance.Repository;
using FamilyFoundsApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FamilyFoundsApi.Persistance;

public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(FamilyFoundsDbContext dbContext) : base(dbContext)
    {
        
    }

    public Task<Transaction> GetByIdAsync(long id, bool tracked = false) =>
        FindByConditionAsync(t => t.Id == id, tracked)
        .Include(t => t.Category)
        .FirstOrDefaultAsync();

    public Task<List<Transaction>> GetAllAsync() =>
        _DbSet.Include(t => t.Category).ToListAsync();

    public bool IsNumberAccountUnique(string number, string account) =>
        !_DbSet.Any(t => t.Number == number && t.Account == account);
}
