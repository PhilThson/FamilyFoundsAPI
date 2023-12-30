namespace FamilyFoundsApi.Core;

public interface IMediator
{
    Task<TResult> Send<TResult>(IRequest<TResult> request);
}
