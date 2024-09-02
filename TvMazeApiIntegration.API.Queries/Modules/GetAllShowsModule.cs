using Carter;
using MediatR;
using TvMazeApiIntegration.Application.Queries.GetAllShows;
using TvMazeApiIntegration.Domain.Entities;

namespace TvMazeApiIntegration.API.Queries.Modules;

public class GetAllShowsShowsModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/Shows", async (IMediator mediator, CancellationToken cancellationToken) =>
        {
            var query = new GetAllShowsQuery(); 

            var result = await mediator.Send(query, cancellationToken);

            return result.IsSuccess ?
                Results.Ok(result.Value) :
                Results.StatusCode(500);
        })
        .WithOpenApi(operation =>
        {
            operation.Summary = "Get all shows";
            operation.Description = "Get all shows from system.";
            return operation;
        })
        .WithName(nameof(GetAllShowsShowsModule))
        .WithTags(nameof(Show))
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}
