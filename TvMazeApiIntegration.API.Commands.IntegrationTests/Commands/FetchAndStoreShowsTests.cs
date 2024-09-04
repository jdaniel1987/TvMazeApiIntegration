using Microsoft.EntityFrameworkCore;
using System.Net;
using TvMazeApiIntegration.Domain.Entities;

namespace TvMazeApiIntegration.API.Commands.IntegrationTests.Commands;

public sealed class FetchAndStoreShowsTests : ApiBaseTests
{
    [Fact]
    public async Task Should_fetch_and_store_shows()
    {
        // Arrange
        var apiKey = "YourSecretApiKey";

        var expected = new[]
        {
            new Show(1, "{\"id\": 1, \"name\": \"Show1\"}"),
            new Show(2, "{\"id\": 2, \"name\": \"Show2\"}")
        };

        // Act
        ApiClient.DefaultRequestHeaders.Add("ApiKey", apiKey);
        var response = await ApiClient.PostAsync("api/FetchAndStoreShows", null);

        // Assert
        var actualReadOnlyDb = await ReadOnlyDbContext.Shows.ToArrayAsync();
        var actualWriteReadDb = await WriteReadDbContext.Shows.ToArrayAsync();
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        actualReadOnlyDb.Should().BeEquivalentTo(expected);
        actualWriteReadDb.Should().BeEquivalentTo(expected);
    }
}
