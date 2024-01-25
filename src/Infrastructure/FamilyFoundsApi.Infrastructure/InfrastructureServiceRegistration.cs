using FamilyFoundsApi.Core.Contracts.Infrastructure;
using FamilyFoundsApi.Infrastructure.FileImport;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyFoundsApi.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ICsvImporter, CsvImporter>();

        return services;
    }
}
