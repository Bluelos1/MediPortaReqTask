using MediportaTask.Interface;

namespace MediportaTask.Service;

public class FetcherInitializator : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public FetcherInitializator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var tagsFetcher = scope.ServiceProvider.GetRequiredService<ITagsFetcher>();
            await tagsFetcher.InitializeTags();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
