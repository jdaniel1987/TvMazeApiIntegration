using System.Collections.Immutable;
using TvMazeApiIntegration.Application.Queries.GetAllShows;
using TvMazeApiIntegration.Domain.Entities;

namespace TvMazeApiIntegration.Application.Extensions;

public sealed class ShowExtensionsTests
{
    private readonly IFixture _fixture;

    public ShowExtensionsTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void ToGetAllShowsResponse_ShouldConvertShowsToResponse()
    {
        // Arrange
        var shows = _fixture.CreateMany<Show>(3).ToImmutableArray();

        var expected = shows.Select(s => new GetAllShowsResponseItem(s.Data));

        // Act
        var result = shows.ToGetAllShowsResponse();

        // Assert
        result.Shows.Should().BeEquivalentTo(expected);
    }
}
