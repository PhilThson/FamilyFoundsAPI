using AutoMapper;
using FamilyFoundsApi.Core.Contracts.API;
using FamilyFoundsApi.Core.Contracts.Persistance;
using FamilyFoundsApi.Domain.Dtos.Read;

namespace FamilyFoundsApi.Core;

public record GetTransactionsListQuery : IRequest<List<ReadTransactionDto>>;

public class GetTransactionsListQueryHandler : IRequestHandler<GetTransactionsListQuery, List<ReadTransactionDto>>
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public GetTransactionsListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<ReadTransactionDto>> Handle(GetTransactionsListQuery request)
    {
        var transactions = await _unitOfWork.Transaction.FindAllAsync();
        return _mapper.Map<List<ReadTransactionDto>>(transactions);
    }
}
