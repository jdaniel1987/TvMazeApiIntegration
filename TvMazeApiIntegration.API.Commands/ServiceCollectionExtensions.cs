using Carter;
using TvMazeApiIntegration.Application;
using TvMazeApiIntegration.Application.Jobs;
using TvMazeApiIntegration.Infrastructure;

namespace TvMazeApiIntegration.API.Commands;

public static class ServiceCollectionExtensions
{
    public static void AddCommandsApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddApplication();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCarter();

        services.AddHostedService<FetchAndStoreShowsHostedService>();
    }
}
