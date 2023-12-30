﻿using FamilyFoundsApi.Core;

namespace FamilyFoundsApi.Persistance;

public class UnitOfWork : IUnitOfWork
{
    private FamilyFoundsDbContext _dbContext;
    private ITransactionRepository _transaction;
    private ICategoryRepository _category;

    public UnitOfWork(FamilyFoundsDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public ITransactionRepository Transaction => 
        _transaction ??= new TransactionRepository(_dbContext);
    public ICategoryRepository Category => 
        _category ??= new CategoryRepository(_dbContext);


    public Task SaveAsync() =>
        _dbContext.SaveChangesAsync();
}
