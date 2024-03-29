﻿using AutoMapper;
using FamilyFoundsApi.Core.Contracts.API;
using FamilyFoundsApi.Core.Contracts.Persistance;
using FamilyFoundsApi.Core.Exceptions;
using FamilyFoundsApi.Domain.Dtos.Read;

namespace FamilyFoundsApi.Core.Features.Transaction.Queries;

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
