using Microsoft.Extensions.DependencyInjection;
using Moq;
using TvMazeApiIntegration.Infrastructure.APIs;

namespace TvMazeApiIntegration.API.Commands.IntegrationTests.Configuration;

public static class MockTvMazeApi
{
    public static void ReplaceTvMazeApi(IServiceCollection services)
    {
        var api = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(ITVMazeApi))!;                
        services.Remove(api);

        var mockTvMazeApi = new Mock<ITVMazeApi>();

        mockTvMazeApi.Setup(api => api.GetShowsAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync("[{\"id\": 1, \"name\": \"Show1\"}, {\"id\": 2, \"name\": \"Show2\"}]");

        services.AddSingleton(mockTvMazeApi.Object);
    }
}
