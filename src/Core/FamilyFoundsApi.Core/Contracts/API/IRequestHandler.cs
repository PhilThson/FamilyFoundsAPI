namespace FamilyFoundsApi.Core.Contracts.API;

public interface IRequestHandler<in TRequest, TResonpse>
    where TRequest : IRequest<TResonpse>
{
    Task<TResonpse> Handle(TRequest request);
}