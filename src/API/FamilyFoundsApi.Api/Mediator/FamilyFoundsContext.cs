using System.Reflection;
using FamilyFoundsApi.Core;

namespace FamilyFoundsApi.Api;

public class FamilyFoundsContext : IMediator
{
    private IServiceScopeFactory _serviceScopeFactory;

    public FamilyFoundsContext(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task<TResult> Send<TResult>(IRequest<TResult> request)
    {
        var handlerType = GetHandlerType(request) ?? throw new ArgumentNullException();
        using var scope = _serviceScopeFactory.CreateScope();
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
        return types.First(t => t.GetInterfaces()
                .Any(i => i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) &&
                    i.GetGenericArguments()[0] == request.GetType()));
    }
}
