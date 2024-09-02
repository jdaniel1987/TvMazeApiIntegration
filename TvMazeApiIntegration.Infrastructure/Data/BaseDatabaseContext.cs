using Microsoft.EntityFrameworkCore;
using TvMazeApiIntegration.Domain.Entities;
using TvMazeApiIntegration.Infrastructure.Data.EntityConfigurations;

namespace TvMazeApiIntegration.Infrastructure.Data;

public class BaseDatabaseContext(DbContextOptions options) : DbContext(options)
{
    public virtual DbSet<Show> Shows { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShowConfiguration).Assembly);
    }
}
