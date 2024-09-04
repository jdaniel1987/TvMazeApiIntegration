using TvMazeApiIntegration.API.Queries.Contracts.Responses;
using TvMazeApiIntegration.Application.Queries.GetAllShows;
using TvMazeApiIntegration.Domain.Entities;

namespace TvMazeApiIntegration.Application.Extensions;

public sealed class ShowExtensionsTests
{
    public sealed class ToGetAllShowsQueryResponse
    {
        [Theory, AutoData]
        public void ToGetAllShowsQueryResponse_ShouldConvertShowsToQueryResponse(IReadOnlyCollection<Show> shows)
        {
            // Arrange
            var expected = shows.Select(s => new GetAllShowsResponseItem(s.Data));

            // Act
            var result = shows.ToGetAllShowsQueryResponse();

            // Assert
            result.Shows.Should().BeEquivalentTo(expected);
        }
    }

    public sealed class ToGetAllShowsResponse
    {
        [Theory, AutoData]
        public void ToGetAllShowsResponse_ShouldConvertGetAllShowsQueryResponseToResponse(GetAllShowsQueryResponse showsQueryResponse)
        {
            // Arrange
            var expected = showsQueryResponse.Shows.Select(s => new GetAllShowsResponseItem(s.Show));

            // Act
            var result = showsQueryResponse.ToGetAllShowsResponse();

            // Assert
            result.Shows.Should().BeEquivalentTo(expected);
        }
    }
}
