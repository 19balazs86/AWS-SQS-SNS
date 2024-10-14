namespace WorkerAWS.Workers;

public sealed class WorkerSenderSQS : BackgroundService
{
    private readonly ILogger<WorkerSenderSQS> _logger;

    public WorkerSenderSQS(ILogger<WorkerSenderSQS> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            await Task.Delay(1000, stoppingToken);
        }
    }
}
