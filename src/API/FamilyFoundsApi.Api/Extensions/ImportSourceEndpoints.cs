using FamilyFoundsApi.Core;
using FamilyFoundsApi.Core.Contracts.API;
using FamilyFoundsApi.Domain.Dtos.Read;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FamilyFoundsApi.Api.Extensions;

public static class ImportSourceEndpoints
{
    public static void MapImportSourceEndpoints(this RouteGroupBuilder group)
    {
        var importSource = group.MapGroup("importSources");

        importSource.MapGet("", GetAll).WithOpenApi();
    }

    private static async Task<Ok<List<ReadImportSourceDto>>> GetAll(IMediator mediator)
    {
        return TypedResults.Ok(await mediator.Send(new GetImportSourceListQuery()));
    }
}
