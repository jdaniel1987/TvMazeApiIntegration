using TvMazeApiIntegration.Application.Extensions;
using TvMazeApiIntegration.Application.Queries.GetAllShows;
using TvMazeApiIntegration.Domain.Entities;
using TvMazeApiIntegration.Domain.Interfaces;

namespace TvMazeApiIntegration.Application.UnitTests.Queries.GetAllShows;

public sealed class GetAllShowsHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_ShouldReturnFailureResult_WhenExceptionIsThrown(
        [Frozen] Mock<IShowRepository> showRepositoryMock,
        GetAllShowsQuery query)
    {
        // Arrange
        var handler = new GetAllShowsHandler(showRepositoryMock.Object);

        showRepositoryMock.Setup(repo => repo.GetAllShows(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("DB Error"));

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Error reading from DB: DB Error");
    }

    [Theory, AutoData]
    public async Task Handle_ShouldReturnSuccessResult_WhenShowsAreFetchedSuccessfully(
        [Frozen] Mock<IShowRepository> showRepositoryMock,
        GetAllShowsQuery query,
        Show[] shows)
    {
        // Arrange
        var handler = new GetAllShowsHandler(showRepositoryMock.Object);

        showRepositoryMock.Setup(repo => repo.GetAllShows(It.IsAny<CancellationToken>()))
            .ReturnsAsync([.. shows]);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Shows.Should().BeEquivalentTo(shows.ToGetAllShowsResponse().Shows);
    }
}
