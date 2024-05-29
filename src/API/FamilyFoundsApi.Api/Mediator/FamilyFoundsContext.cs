using System.Reflection;
using FamilyFoundsApi.Core.Contracts.API;

namespace FamilyFoundsApi.Api.Mediator;

public class FamilyFoundsContext(IServiceScopeFactory serviceScopeFactory) : IMediator
{
    public async Task<TResult> Send<TResult>(IRequest<TResult> request)
    {
        var handlerType = GetHandlerType(request) ??
                          throw new ArgumentNullException(nameof(request));
        using var scope = serviceScopeFactory.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService(handlerType); 
        var methodInfo = handler.GetType().GetMethod("Handle");
        try 
        {
            return await (methodInfo?.Invoke(handler, [request]) as Task<TResult>)!;
        }
        catch (TargetInvocationException e)
        {
            if (e.InnerException is not null)
                throw e.InnerException;
            else 
                throw new ApplicationException("An error occured during invoking handler method", e);
        }
    }

    private static Type? GetHandlerType<TRequest>(TRequest request)
    {
        var types = Assembly.Load("FamilyFoundsApi.Core").GetTypes();
        return types.FirstOrDefault(t => t.GetInterfaces()
                .Any(i => i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) &&
                    i.GetGenericArguments()[0] == request!.GetType()));
    }
}
