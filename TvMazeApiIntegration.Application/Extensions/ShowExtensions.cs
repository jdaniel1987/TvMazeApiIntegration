using System.Collections.Immutable;
using TvMazeApiIntegration.Application.Queries.GetAllShows;
using TvMazeApiIntegration.Domain.Entities;

namespace TvMazeApiIntegration.Application.Extensions;

public static class ShowExtensions
{
    public static GetAllShowsResponse ToGetAllShowsResponse(this IReadOnlyCollection<Show> shows) => 
        new(
            shows.Select(s => new GetAllShowsResponseItem(s.Data))
            .ToImmutableArray());
}
