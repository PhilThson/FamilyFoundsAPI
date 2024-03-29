﻿using FamilyFoundsApi.Domain.Models;

namespace FamilyFoundsApi.Core.Contracts.Persistance.Repository;

public interface ITransactionRepository : IBaseRepository<Transaction>
{
    Task<Transaction> GetByIdAsync(long id, bool tracked = false);
    Task<List<Transaction>> GetAllAsync();
    Task<List<Transaction>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    bool IsNumberUnique(string number);
}
