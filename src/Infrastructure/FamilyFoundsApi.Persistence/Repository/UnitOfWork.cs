﻿using FamilyFoundsApi.Core.Contracts.Persistance;
using FamilyFoundsApi.Core.Contracts.Persistance.Repository;
using FamilyFoundsApi.Domain;
using FamilyFoundsApi.Domain.Models.Base;

namespace FamilyFoundsApi.Persistence.Repository;

public class UnitOfWork : IUnitOfWork
{
    private FamilyFoundsDbContext _dbContext;
    private ITransactionRepository _transaction;
    private ICategoryRepository _category;
    private IImportSourceRepository _importSource;

    public UnitOfWork(FamilyFoundsDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public ITransactionRepository Transaction => 
        _transaction ??= new TransactionRepository(_dbContext);
    public ICategoryRepository Category => 
        _category ??= new CategoryRepository(_dbContext);
    public IImportSourceRepository ImportSource =>
        _importSource ??= new ImportSourceRepository(_dbContext);


    public Task SaveAsync() =>
        _dbContext.SaveChangesAsync();

    public void AddEntity<T>(T instance)
    {
        _dbContext.Add(instance);
        _dbContext.SaveChanges();
    }

    public async Task<int> AddEntitiesAsync<T>(IEnumerable<T> entities) 
        where T : class
    {
        await _dbContext.Set<T>().AddRangeAsync(entities);
        return await _dbContext.SaveChangesAsync();
    }

    public void AttachEntity<T>(T instance, List<string> modifiedProperties)
    {
        _dbContext.Attach(instance);
        foreach (var property in modifiedProperties)
        {
            _dbContext.Entry(instance).Property(property).IsModified = true;
        }
        _dbContext.SaveChanges();
    }

    public void RemoveEntity<T>(T instance) where T : IRemovable
    {
        instance.IsActive = false;
        _dbContext.SaveChanges();
    }
}
