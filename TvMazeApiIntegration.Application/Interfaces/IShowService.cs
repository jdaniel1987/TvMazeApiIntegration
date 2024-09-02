namespace TvMazeApiIntegration.Application.Interfaces;

public interface IShowService
{
    Task FetchAndStoreShowsAsync(CancellationToken cancellationToken);
    Task<IReadOnlyCollection<string>> GetAllShowsAsync(CancellationToken cancellationToken);
}
