using FamilyFoundsApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace FamilyFoundsApi.Persistance;

public class FamilyFoundsDbContext : DbContext
{
    public FamilyFoundsDbContext(DbContextOptions<FamilyFoundsDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<Transaction> Transaction { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<ImportSource> ImportSource { get; set; }
}
