using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TvMazeApiIntegration.Application.Commands.FetchAndStoreShows;
using TvMazeApiIntegration.Domain.Entities;

namespace TvMazeApiIntegration.API.Commands.Modules;

public class FetchAndStoreShowsModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/FetchAndStoreShows", async (
            [FromHeader(Name = "ApiKey")] string apiKey,
            FetchAndStoreShowsCommand command, 
            IMediator mediator, 
            IConfiguration configuration, 
            CancellationToken cancellationToken) =>
        {
            if (apiKey != configuration.GetValue<string>("ApiKey"))
            {
                return Results.Unauthorized();
            }

            var result = await mediator.Send(command, cancellationToken);

            return result.IsSuccess ?
                Results.Created() :
                Results.StatusCode(500);
        })
        .WithOpenApi(operation =>
        {
            operation.Summary = "Gets all shows from TvMaze API and stores it";
            operation.Description = "Gets all shows from TvMaze API and stores it in the system.";
            return operation;
        })
        .WithName(nameof(FetchAndStoreShowsModule))
        .WithTags(nameof(Show))
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}
