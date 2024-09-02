using AutoFixture;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Json;
using TvMazeApiIntegration.Application.Commands.FetchAndStoreShows;
using TvMazeApiIntegration.Domain.Entities;

namespace TvMazeApiIntegration.API.Commands.IntegrationTests.Commands;

public class FetchAndStoreShowsTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_fetch_and_store_shows(
        IFixture fixture)
    {
        // Arrange
        var command = fixture.Build<FetchAndStoreShowsCommand>()
            .With(c => c.ApiKey, "YourSecretApiKey")
            .Create();

        var expected = new[]
        {
            new Show(1, "{\"id\": 1, \"name\": \"Show1\"}"),
            new Show(2, "{\"id\": 2, \"name\": \"Show2\"}")
        };

        // Act
        var response = await ApiClient.PostAsJsonAsync("api/FetchAndStoreShows", command);

        // Assert
        var actualReadOnlyDb = await ReadOnlyDbContext.Shows.ToArrayAsync();
        var actualWriteReadDb = await WriteReadDbContext.Shows.ToArrayAsync();
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        actualReadOnlyDb.Should().BeEquivalentTo(expected);
        actualWriteReadDb.Should().BeEquivalentTo(expected);
    }
}
