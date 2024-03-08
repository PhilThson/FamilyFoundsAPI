using FamilyFoundsApi.Core;
using FamilyFoundsApi.Core.Contracts.API;
using FamilyFoundsApi.Domain.Dtos.Read;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FamilyFoundsApi.Api.Extensions;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this RouteGroupBuilder group)
    {
        var categories = group.MapGroup("categories");

        categories.MapGet("", GetAll).WithOpenApi();
        categories.MapGet("{id}", GetById).WithOpenApi();
    }

    private static async Task<Ok<List<ReadCategoryDto>>> GetAll(IMediator mediator)
    {
        return TypedResults.Ok(await mediator.Send(new GetCategoriesListQuery()));
    }

    private static async Task<Results<Ok<ReadCategoryDto>, NotFound>> GetById(short id, IMediator mediator)
    {
        return TypedResults.Ok(await mediator.Send(new GetCategoryByIdQuery(id)));
    }
}
