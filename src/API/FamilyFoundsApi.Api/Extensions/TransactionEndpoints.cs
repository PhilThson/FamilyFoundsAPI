using FamilyFoundsApi.Core;
using FamilyFoundsApi.Core.Contracts.API;
using FamilyFoundsApi.Core.Features.Transaction;
using FamilyFoundsApi.Domain.Dtos.Create;
using FamilyFoundsApi.Domain.Dtos.Read;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FamilyFoundsApi.Api;

public static class TransactionEndpoints
{
    public static void MapTransactionEndpoints(this RouteGroupBuilder group)
    {
        var transactions = group.MapGroup("/transactions");

        transactions.MapGet("", GetAll)
            .WithName("GetAllTransactions")
            .WithOpenApi();

        transactions.MapGet("{id:long}", GetById)
            .WithName("GetTransactionById")
            .WithOpenApi();

        transactions.MapPost("", Create)
            .WithName("CreateTransaction")
            .WithOpenApi();

        transactions.MapDelete("{id}", DeleteById)
            .WithName("DeleteTransaction")
            .WithOpenApi();
    }

    private static async Task<Ok<List<ReadTransactionDto>>> GetAll(IMediator mediator)
    {
        var transactions = await mediator.Send(new GetTransactionsListQuery());
        await Task.Delay(2000);
        return TypedResults.Ok(transactions);
    }

    private static async Task<Results<Ok<ReadTransactionDto>, NotFound>> 
        GetById(long id, IMediator mediator)
    {
        var transaction = await mediator.Send(new GetTransactionByIdQuery(id));
        return TypedResults.Ok(transaction);
    }

    private static async Task<Results<Created<ReadTransactionDto>, BadRequest>>
        Create(CreateTransactionDto transactionDto, IMediator mediator)
    {
        var transaction = await mediator.Send(new CreateTransactionCommand(transactionDto));
        return TypedResults.Created(nameof(GetById), transaction);
    }

    private static async Task<Results<NoContent, NotFound>>
        DeleteById(long id, IMediator mediator)
    {
        _ = await mediator.Send(new DeleteTransactionCommand(id));
        return TypedResults.NoContent();
    }
}
