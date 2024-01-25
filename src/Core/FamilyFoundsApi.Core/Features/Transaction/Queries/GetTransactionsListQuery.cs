using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FamilyFoundsApi.Core.Contracts.API;
using FamilyFoundsApi.Core.Contracts.Persistance;
using FamilyFoundsApi.Domain.Dtos.Read;

namespace FamilyFoundsApi.Core.Features.Transaction.Queries;

public record GetTransactionsListQuery(DateTime StartDate, DateTime EndDate) : IRequest<List<ReadTransactionDto>>;

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
        if (request.StartDate.Date > request.EndDate.Date)
        {
            throw new ValidationException("Data początkowa musi być mniejsza lub równa dacie końcowej");
        }
        var transactions = await _unitOfWork.Transaction.GetByDateRangeAsync(
            request.StartDate.Date, request.EndDate.Date);
            
        return _mapper.Map<List<ReadTransactionDto>>(transactions);
    }
}
