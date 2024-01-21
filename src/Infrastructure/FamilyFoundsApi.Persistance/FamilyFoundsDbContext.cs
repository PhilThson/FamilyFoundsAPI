using FamilyFoundsApi.Core.Extensions;
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
                new () { Id = (short)BankEnum.MILLENNIUM, Name = BankEnum.MILLENNIUM.ToString() },
            });

        modelBuilder.Entity<Category>()
            .HasData(new List<Category>
            {
                new() { Id = (short)CategoriesEnum.FOOD, Name = CategoriesEnum.FOOD.GetDescription()},
                new() { Id = (short)CategoriesEnum.CHEMICAL_AND_HYGIENE, Name = CategoriesEnum.CHEMICAL_AND_HYGIENE.GetDescription()},
                new() { Id = (short)CategoriesEnum.CLOTHES, Name = CategoriesEnum.CLOTHES.GetDescription()},
                new() { Id = (short)CategoriesEnum.ENTERTAINMENT, Name = CategoriesEnum.ENTERTAINMENT.GetDescription()},
                new() { Id = (short)CategoriesEnum.TRANSPORT, Name = CategoriesEnum.TRANSPORT.GetDescription()},
                new() { Id = (short)CategoriesEnum.HOUSEHOLD, Name = CategoriesEnum.HOUSEHOLD.GetDescription()},
                new() { Id = (short)CategoriesEnum.HEALTH, Name = CategoriesEnum.HEALTH.GetDescription()},
                new() { Id = (short)CategoriesEnum.BILLS, Name = CategoriesEnum.BILLS.GetDescription()},
                new() { Id = (short)CategoriesEnum.CHILDREN, Name = CategoriesEnum.CHILDREN.GetDescription()},
                new() { Id = (short)CategoriesEnum.OTHER, Name = CategoriesEnum.OTHER.GetDescription()}
            });

        base.OnModelCreating(modelBuilder);
    }
}
