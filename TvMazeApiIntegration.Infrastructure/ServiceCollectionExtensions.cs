using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using TvMazeApiIntegration.Infrastructure.Data;
using TvMazeApiIntegration.Domain.Repositories;
using TvMazeApiIntegration.Domain.Interfaces;

namespace TvMazeApiIntegration.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var readOnlyConnectionString = configuration.GetConnectionString("ReadOnlyDB");
        var writeReadConnectionString = configuration.GetConnectionString("WriteReadDB");

        services.AddDbContextFactory<ReadOnlyDatabaseContext>(options => options
            .UseLazyLoadingProxies()
            .UseSqlServer(readOnlyConnectionString));
        services.AddDbContextFactory<WriteReadDatabaseContext>(options => options
            .UseLazyLoadingProxies()
            .UseSqlServer(writeReadConnectionString));

        services.AddTransient<IShowRepository, ShowRepository>();

        return services;
    }
}
