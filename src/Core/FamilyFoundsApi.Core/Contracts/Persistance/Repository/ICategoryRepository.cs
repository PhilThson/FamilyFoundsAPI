using FamilyFoundsApi.Domain.Models;

namespace FamilyFoundsApi.Core.Contracts.Persistance.Repository;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<Category> GetByNameAsync(string name, bool tracked = false);
    bool ExistByName(string name);
}
