using FamilyFoundsApi.Domain.Enums;
using FamilyFoundsApi.Domain.Models;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>().Property(t => t.IsActive).HasDefaultValue(true);
        modelBuilder.Entity<Transaction>()
            .HasIndex(p => new { p.Number, p.Account })
            .IsUnique();
        modelBuilder.Entity<Transaction>().HasQueryFilter(filter => filter.IsActive == true);

        modelBuilder.Entity<ImportSource>()
            .HasData(new List<ImportSource>
            {
                new () { Id = (short)BankEnum.ING, Name = BankEnum.ING.ToString() },
                new () { Id = (short)BankEnum.MILLENIUM, Name = BankEnum.MILLENIUM.ToString() },
            });

        base.OnModelCreating(modelBuilder);
    }
}
