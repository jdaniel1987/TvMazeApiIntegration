using Carter;
using Refit;
using TvMazeApiIntegration.API.Queries.Contracts;
using TvMazeApiIntegration.Application;
using TvMazeApiIntegration.Infrastructure;

namespace TvMazeApiIntegration.API.Queries;

public static class ServiceCollectionExtensions
{
    public static void AddQueriesApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddApplication();
        services.AddRefitClient<IQueryEndpoints>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://your-api-url.com"));

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCarter();
    }
}
