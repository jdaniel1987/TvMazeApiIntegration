using Microsoft.EntityFrameworkCore;

namespace TvMazeApiIntegration.Infrastructure.Data;

public class ReadOnlyDatabaseContext(DbContextOptions<ReadOnlyDatabaseContext> options) : BaseDatabaseContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // No need to track entities, as we are not modifying them in this DbContext
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        base.OnConfiguring(optionsBuilder);
    }
}
