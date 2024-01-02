using FamilyFoundsApi.Core;
using FamilyFoundsApi.Domain;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FamilyFoundsApi.Api;

public static class ImportSourceEndpoints
{
    public static void MapImportSourceEndpoints(this RouteGroupBuilder group)
    {
        var importSource = group.MapGroup("importSource");

        importSource.MapGet("", GetAll).WithOpenApi();
    }

    private static async Task<Ok<List<ReadImportSourceDto>>> GetAll(IMediator mediator)
    {
        return TypedResults.Ok(await mediator.Send(new GetImportSourceListQuery()));
    }
}
