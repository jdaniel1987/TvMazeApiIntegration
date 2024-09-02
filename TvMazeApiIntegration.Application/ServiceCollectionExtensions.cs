using Microsoft.Extensions.DependencyInjection;
using Refit;
using System.Reflection;
using TvMazeApiIntegration.Infrastructure.APIs;

namespace TvMazeApiIntegration.Application;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddRefitClient<ITVMazeApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.tvmaze.com"));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}
