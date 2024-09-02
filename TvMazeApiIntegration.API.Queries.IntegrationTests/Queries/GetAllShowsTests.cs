﻿using System.Collections.Immutable;
using System.Net.Http.Json;
using TvMazeApiIntegration.Application.Queries.GetAllShows;
using TvMazeApiIntegration.Domain.Entities;

namespace TvMazeApiIntegration.API.Queries.IntegrationTests.Modules;

public class GetAllShowsTests : ApiBaseTests
{
    [Theory, AutoData]
    public async Task Should_get_all_shows(
        IReadOnlyCollection<Show> existingShows)
    {
        // Arrange
        await ReadOnlyDbContext.AddRangeAsync(existingShows);
        await ReadOnlyDbContext.SaveChangesAsync();

        var expected = new GetAllShowsResponse(existingShows.Select(gc =>
            new GetAllShowsResponseItem(
                gc.Data
            ))
            .ToImmutableArray());

        // Act
        var actual = await ApiClient.GetFromJsonAsync<GetAllShowsResponse>("api/Shows");

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}
