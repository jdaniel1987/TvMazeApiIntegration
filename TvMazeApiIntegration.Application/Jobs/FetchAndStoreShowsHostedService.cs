using MediatR;
using Microsoft.Extensions.Hosting;
using TvMazeApiIntegration.Application.Commands.FetchAndStoreShows;

namespace TvMazeApiIntegration.Application.Jobs;

public class FetchAndStoreShowsHostedService : IHostedService, IDisposable
{
    private readonly IMediator _mediator;
    private readonly Timer _timer;

    public FetchAndStoreShowsHostedService(IMediator mediator)
    {
        _mediator = mediator;
        _timer = new Timer(DoWork!, null, TimeSpan.FromSeconds(30), TimeSpan.FromHours(1));
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async void DoWork(object state)
    {
        var command = new FetchAndStoreShowsCommand();
        await _mediator.Send(command);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
        GC.SuppressFinalize(this);
    }
}
