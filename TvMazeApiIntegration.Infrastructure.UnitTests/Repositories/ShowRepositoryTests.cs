using Microsoft.EntityFrameworkCore;
using TvMazeApiIntegration.Domain.Entities;
using TvMazeApiIntegration.Domain.Repositories;
using TvMazeApiIntegration.Infrastructure.Data;

namespace TvMazeApiIntegration.Infrastructure.UnitTests.Repositories;

public sealed class ShowRepositoryTests
{
    public static async Task<IReadOnlyCollection<Show>>
        CreateExistingShows(
        IFixture fixture,
        WriteReadDatabaseContext writeReadDbContext)
    {
        var existingShows = fixture.Build<Show>()
            .CreateMany()
            .ToArray();

        await writeReadDbContext.AddRangeAsync(existingShows);
        await writeReadDbContext.SaveChangesAsync();

        writeReadDbContext.ChangeTracker.Clear();

        return existingShows;
    }

    public sealed class GetAllShows : RepositoryTestsBase<ShowRepository>
    {
        [Theory, AutoData]
        public async Task Should_get_all_shows(
            IReadOnlyCollection<Show> existingShows)
        {
            // Arrange
            await WriteReadDbContext.AddRangeAsync(existingShows);
            await WriteReadDbContext.SaveChangesAsync();

            WriteReadDbContext.ChangeTracker.Clear();

            var expected = existingShows;

            // Act
            var actual = await RepositoryUnderTesting.GetAllShows(default);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }

    public sealed class SaveShows : RepositoryTestsBase<ShowRepository>
    {
        [Theory, AutoData]
        public async Task Should_add_shows(
            IReadOnlyCollection<Show> existingShows)
        {
            // Arrange
            var expected = existingShows;

            // Act
            await RepositoryUnderTesting.SaveShows(existingShows, default);

            // Assert
            var actualReadOnlyDbContext = await ReadOnlyDbContext.Shows.ToArrayAsync();
            var actualWriteReadDbContext = await WriteReadDbContext.Shows.ToArrayAsync();
            actualReadOnlyDbContext.Should().BeEquivalentTo(expected);
            actualWriteReadDbContext.Should().BeEquivalentTo(expected);
        }
    }
}
