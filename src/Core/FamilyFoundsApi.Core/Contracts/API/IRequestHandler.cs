namespace FamilyFoundsApi.Core;

public interface IRequestHandler<in TRequest, TResonpse>
    where TRequest : IRequest<TResonpse>
{
    Task<TResonpse> Handle(TRequest request);
}