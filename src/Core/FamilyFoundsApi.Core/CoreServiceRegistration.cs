using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyFoundsApi.Core;

public static class CoreServiceRegistration
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        foreach (var handlerType in Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.Name.EndsWith("Handler") && !t.IsAbstract && !t.IsInterface))
        {
            services.AddTransient(handlerType);
        }

        return services;
    }
}
