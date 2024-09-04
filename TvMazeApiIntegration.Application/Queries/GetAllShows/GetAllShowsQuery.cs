using CSharpFunctionalExtensions;
using MediatR;

namespace TvMazeApiIntegration.Application.Queries.GetAllShows;

public record GetAllShowsQuery() : IRequest<IResult<GetAllShowsQueryResponse>>;
