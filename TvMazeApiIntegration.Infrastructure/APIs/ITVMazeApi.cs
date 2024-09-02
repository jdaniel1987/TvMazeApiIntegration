using Refit;

namespace TvMazeApiIntegration.Infrastructure.APIs;

public interface ITVMazeApi
{
    [Get("/shows")]
    Task<string> GetShowsAsync(CancellationToken cancellationToken = default);
}
