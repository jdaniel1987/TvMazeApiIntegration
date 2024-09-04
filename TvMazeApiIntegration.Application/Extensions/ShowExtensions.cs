using System.Collections.Immutable;
using TvMazeApiIntegration.API.Queries.Contracts.Responses;
using TvMazeApiIntegration.Application.Queries.GetAllShows;
using TvMazeApiIntegration.Domain.Entities;

namespace TvMazeApiIntegration.Application.Extensions;

public static class ShowExtensions
{
    public static GetAllShowsQueryResponse ToGetAllShowsQueryResponse(this IReadOnlyCollection<Show> shows) => 
        new(
            shows.Select(s => new GetAllShowsQueryResponseItem(s.Data))
            .ToImmutableArray());

    public static GetAllShowsResponse ToGetAllShowsResponse(this GetAllShowsQueryResponse showsQueryResponse) =>
    new(
        showsQueryResponse.Shows.Select(s => new GetAllShowsResponseItem(s.Show))
        .ToImmutableArray());
}
