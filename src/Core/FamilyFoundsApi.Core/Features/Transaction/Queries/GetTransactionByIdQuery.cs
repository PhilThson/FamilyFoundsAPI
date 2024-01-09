using AutoMapper;
using FamilyFoundsApi.Core.Contracts.API;
using FamilyFoundsApi.Core.Contracts.Persistance;
using FamilyFoundsApi.Core.Persistance.Exceptions;
using FamilyFoundsApi.Domain.Dtos.Read;
using FamilyFoundsApi.Domain.Models;

namespace FamilyFoundsApi.Core;

public record GetTransactionByIdQuery(long Id) : IRequest<ReadTransactionDto>;

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
        var transaction = _unitOfWork.Transaction.FindById(request.Id) ??
            throw new NotFoundException(nameof(Transaction), request.Id);
            
        return Task.FromResult(_mapper.Map<ReadTransactionDto>(transaction));
    }
}
