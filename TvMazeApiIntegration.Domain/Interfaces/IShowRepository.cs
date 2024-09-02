using TvMazeApiIntegration.Domain.Entities;

namespace TvMazeApiIntegration.Domain.Interfaces;

public interface IShowRepository
{
    Task SaveShows(IReadOnlyCollection<Show> shows, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<Show>> GetAllShows(CancellationToken cancellationToken);
}
