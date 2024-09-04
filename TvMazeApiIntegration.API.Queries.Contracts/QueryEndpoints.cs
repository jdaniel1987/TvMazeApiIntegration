using Refit;
using TvMazeApiIntegration.API.Queries.Contracts.Responses;

namespace TvMazeApiIntegration.API.Queries.Contracts;

public interface IQueryEndpoints
{
    [Get("/api/Shows")]
    Task<GetAllShowsResponse> GetAllShows();
}
