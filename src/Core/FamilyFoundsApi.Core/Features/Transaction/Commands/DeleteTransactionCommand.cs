using FamilyFoundsApi.Core.Contracts.API;
using FamilyFoundsApi.Core.Contracts.Persistance;
using FamilyFoundsApi.Core.Persistance.Exceptions;

namespace FamilyFoundsApi.Core.Features.Transaction.Commands;

public record DeleteTransactionCommand(long Id) : IRequest<long>;

public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, long>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTransactionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<long> Handle(DeleteTransactionCommand request)
    {
        if (request.Id <= 0)
            throw new ValidationException("Nieprawidłowy identyfikator transakcji");

        var transaction = _unitOfWork.Transaction.FindById(request.Id) ??
            throw new NotFoundException(nameof(Transaction), request.Id);

        _unitOfWork.RemoveEntity(transaction);
        return Task.FromResult(transaction.Id);
    }
}
