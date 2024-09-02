using TvMazeApiIntegration.Application.Commands.FetchAndStoreShows;
using TvMazeApiIntegration.Domain.Entities;
using TvMazeApiIntegration.Domain.Interfaces;
using TvMazeApiIntegration.Infrastructure.APIs;

namespace TvMazeApiIntegration.Application.UnitTests.Commands.FetchAndStoreShows;

public sealed class FetchAndStoreShowsHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_ShouldReturnFailureResult_WhenApiCallFails(
        [Frozen] Mock<ITVMazeApi> tvMazeApiMock,
        [Frozen] Mock<IShowRepository> showRepositoryMock,
        FetchAndStoreShowsCommand command)
    {
        // Arrange
        var handler = new FetchAndStoreShowsHandler(tvMazeApiMock.Object, showRepositoryMock.Object);

        tvMazeApiMock.Setup(api => api.GetShowsAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("API Error"));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Error connecting to TVMaze API: API Error");
    }

    [Theory, AutoData]
    public async Task Handle_ShouldReturnFailureResult_WhenApiResponseIsInvalid(
        [Frozen] Mock<ITVMazeApi> tvMazeApiMock,
        [Frozen] Mock<IShowRepository> showRepositoryMock,
        FetchAndStoreShowsCommand command)
    {
        // Arrange
        var handler = new FetchAndStoreShowsHandler(tvMazeApiMock.Object, showRepositoryMock.Object);

        tvMazeApiMock.Setup(api => api.GetShowsAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync("Invalid JSON");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain("Error parsing TVMaze response: ");
    }

    [Theory, AutoData]
    public async Task Handle_ShouldReturnFailureResult_WhenSavingShowsFails(
        [Frozen] Mock<ITVMazeApi> tvMazeApiMock,
        [Frozen] Mock<IShowRepository> showRepositoryMock,
        FetchAndStoreShowsCommand command)
    {
        // Arrange
        var handler = new FetchAndStoreShowsHandler(tvMazeApiMock.Object, showRepositoryMock.Object);
        var showsJson = "[{\"id\": 1, \"name\": \"Show1\"}]";

        tvMazeApiMock.Setup(api => api.GetShowsAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(showsJson);

        showRepositoryMock.Setup(repo => repo.SaveShows(It.IsAny<IReadOnlyCollection<Show>>(), It.IsAny<CancellationToken>()))
            .Throws(new Exception("DB Error"));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Error saving to DB: DB Error");
    }

    [Theory, AutoData]
    public async Task Handle_ShouldReturnSuccessResult_WhenShowsAreSavedSuccessfully(
        [Frozen] Mock<ITVMazeApi> tvMazeApiMock,
        [Frozen] Mock<IShowRepository> showRepositoryMock,
        FetchAndStoreShowsCommand command)
    {
        // Arrange
        var handler = new FetchAndStoreShowsHandler(tvMazeApiMock.Object, showRepositoryMock.Object);
        var showsJson = "[{\"id\": 1, \"name\": \"Show1\"}, {\"id\": 2, \"name\": \"Show2\"}]";

        tvMazeApiMock.Setup(api => api.GetShowsAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(showsJson);

        showRepositoryMock.Setup(repo => repo.SaveShows(It.IsAny<IReadOnlyCollection<Show>>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<FetchAndStoreShowsResponse>();
    }
}
