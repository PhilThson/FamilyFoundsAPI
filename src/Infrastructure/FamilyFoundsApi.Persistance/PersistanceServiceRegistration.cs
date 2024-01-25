using FamilyFoundsApi.Core.Contracts.Persistance;
using FamilyFoundsApi.Persistance.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyFoundsApi.Persistance;

public static class PersistanceRegistration
{
    public static IServiceCollection AddPersistanceServices(this IServiceCollection services,
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
