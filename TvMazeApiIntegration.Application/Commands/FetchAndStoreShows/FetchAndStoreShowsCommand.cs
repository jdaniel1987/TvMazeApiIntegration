using CSharpFunctionalExtensions;
using MediatR;

namespace TvMazeApiIntegration.Application.Commands.FetchAndStoreShows;

public record FetchAndStoreShowsCommand() : IRequest<IResult<FetchAndStoreShowsResponse>>;
