using Microsoft.Extensions.DependencyInjection;
using TvMazeApiIntegration.API.Queries.IntegrationTests.Configuration;
using TvMazeApiIntegration.Infrastructure.Data;

namespace TvMazeApiIntegration.API.Queries.IntegrationTests;
public abstract class ApiBaseTests
{
    protected ApiBaseTests()
    {
        var app = new CustomWebApplicationFactory(services =>
        {
        });
        var scopeProvider = app.Services.GetService<IServiceScopeFactory>()!.CreateScope().ServiceProvider!;
        ApiClient = app.CreateClient();
        ReadOnlyDbContext = scopeProvider.GetService<ReadOnlyDatabaseContext>()!;
        WriteReadDbContext = scopeProvider.GetService<WriteReadDatabaseContext>()!;
    }

    public HttpClient ApiClient { get; init; }

    public ReadOnlyDatabaseContext ReadOnlyDbContext { get; init; }

    public WriteReadDatabaseContext WriteReadDbContext { get; init; }
}
