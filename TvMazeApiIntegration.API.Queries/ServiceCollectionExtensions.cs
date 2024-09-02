using Carter;
using TvMazeApiIntegration.Application;
using TvMazeApiIntegration.Infrastructure;

namespace TvMazeApiIntegration.API.Queries;

public static class ServiceCollectionExtensions
{
    public static void AddQueriesApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddApplication();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCarter();
    }
}
