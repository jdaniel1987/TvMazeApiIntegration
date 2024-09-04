using Refit;

namespace TvMazeApiIntegration.API.Commands.Contracts;

public interface ICommandEndpoints
{
    [Post("/api/FetchAndStoreShows")]
    Task FetchAndStoreShowsAsync([Header("ApiKey")] string apiKey);
}
