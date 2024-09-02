using Microsoft.EntityFrameworkCore;
using TvMazeApiIntegration.Domain.Entities;
using TvMazeApiIntegration.Domain.Interfaces;
using TvMazeApiIntegration.Infrastructure.Data;

namespace TvMazeApiIntegration.Domain.Repositories;

public class ShowRepository(
    IDbContextFactory<ReadOnlyDatabaseContext> readOnlyDatabaseContextFactory,
    IDbContextFactory<WriteReadDatabaseContext> writeReadDatabaseContextFactory) : IShowRepository
{
    private readonly IDbContextFactory<ReadOnlyDatabaseContext> _readOnlyDatabaseContextFactory = readOnlyDatabaseContextFactory;
    private readonly IDbContextFactory<WriteReadDatabaseContext> _writeReadDatabaseContextFactory = writeReadDatabaseContextFactory;

    public async Task<IReadOnlyCollection<Show>> GetAllShows(CancellationToken cancellationToken)
    {
        var readOnlyDbContext = await _readOnlyDatabaseContextFactory.CreateDbContextAsync(cancellationToken);

        return await readOnlyDbContext
            .Shows
            .ToArrayAsync(cancellationToken);
    }

    public async Task SaveShows(IReadOnlyCollection<Show> shows, CancellationToken cancellationToken)
    {
        var writeReadDbContext = await _writeReadDatabaseContextFactory.CreateDbContextAsync(cancellationToken);

        writeReadDbContext.RemoveRange(writeReadDbContext.Shows);
        await writeReadDbContext.AddRangeAsync(shows, cancellationToken);
        await writeReadDbContext.SaveChangesAsync(cancellationToken);
    }
}
