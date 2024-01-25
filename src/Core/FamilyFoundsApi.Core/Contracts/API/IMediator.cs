namespace FamilyFoundsApi.Core.Contracts.API;

public interface IMediator
{
    Task<TResult> Send<TResult>(IRequest<TResult> request);
}
