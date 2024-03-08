using FamilyFoundsApi.Core.Contracts.Persistance;
using FamilyFoundsApi.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyFoundsApi.Persistence;

public static class PersistenceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<FamilyFoundsDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("FamilyFounds"));
            options.EnableSensitiveDataLogging();
        });

        // Register wrapper repository
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
