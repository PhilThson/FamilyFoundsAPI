using AutoMapper;
using FamilyFoundsApi.Domain;

namespace FamilyFoundsApi.Core;

public record GetTransactionByIdQuery(long id) : IRequest<ReadTransactionDto>;

public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, ReadTransactionDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTransactionByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<ReadTransactionDto> Handle(GetTransactionByIdQuery request)
    {
        var transaction = _unitOfWork.Transaction.FindById(request.id) ??
            throw new NotFoundException(nameof(Transaction), request.id);
            
        return Task.FromResult(_mapper.Map<ReadTransactionDto>(transaction));
    }
}
