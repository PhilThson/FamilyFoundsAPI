using FamilyFoundsApi.Core.Contracts.Persistance.Repository;
using FamilyFoundsApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FamilyFoundsApi.Persistence;

public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(FamilyFoundsDbContext dbContext) : base(dbContext)
    {
        
    }

    public Task<Transaction> GetByIdAsync(long id, bool tracked = false) =>
        FindByConditionAsync(t => t.Id == id, tracked)
        .Include(t => t.Category)
        .Include(t => t.ImportSource)
        .FirstOrDefaultAsync();

    public Task<List<Transaction>> GetAllAsync() =>
        _DbSet.Include(t => t.Category).ToListAsync();

    public Task<List<Transaction>> GetByDateRangeAsync(DateTime startDate, DateTime endDate) =>
        FindByConditionAsync(t => t.Date.Date >= startDate.Date && t.Date.Date <= endDate.Date)
        .Include(t => t.Category)
        .ToListAsync();

    public bool IsNumberUnique(string number) =>
        !_DbSet.Any(t => t.Number == number);
}
