using AutoMapper;
using FamilyFoundsApi.Core.Contracts.API;
using FamilyFoundsApi.Core.Contracts.Persistance;
using FamilyFoundsApi.Domain.Dtos.Read;

namespace FamilyFoundsApi.Core.Features.Transaction.Queries;

public record GetTransactionsListQuery : IRequest<List<ReadTransactionDto>>;

public class GetTransactionsListQueryHandler : IRequestHandler<GetTransactionsListQuery, List<ReadTransactionDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTransactionsListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<ReadTransactionDto>> Handle(GetTransactionsListQuery request)
    {
        var transactions = await _unitOfWork.Transaction.GetAllAsync();
        return _mapper.Map<List<ReadTransactionDto>>(transactions);
    }
}
