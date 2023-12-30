using System.Reflection;
using FamilyFoundsApi.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyFoundsApi.Core;

public static class CoreServiceRegistration
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        //Queries
        services.AddScoped<GetTransactionsListQueryHandler>();
        services.AddScoped<GetTransactionByIdQueryHandler>();
        services.AddScoped<GetTransactionByIdQueryHandler>();
        services.AddScoped<GetCategoriesListQueryHandler>();
        services.AddScoped<GetCategoryByIdQueryHandler>();

        //Commands
        services.AddScoped<CreateTransactionCommandHandler>();

        return services;
    }
}
