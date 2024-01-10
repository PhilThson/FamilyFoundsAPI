﻿using FamilyFoundsApi.Domain.Models;
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
        modelBuilder.Entity<Transaction>().HasQueryFilter(filter => filter.IsActive == true);

        base.OnModelCreating(modelBuilder);
    }
}
