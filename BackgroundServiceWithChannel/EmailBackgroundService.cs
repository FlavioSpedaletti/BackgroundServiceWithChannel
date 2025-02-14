namespace BackgroundServiceWithChannel
{
    public class EmailBackgroundService(ILogger<EmailBackgroundService> logger, EmailQueueService queue) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await queue.DequeueAsync();
                logger.LogInformation("EmailBackgroundService find item at queue. Processing...");
                await workItem();
                logger.LogInformation("EmailBackgroundService is done.");
            }
        }
    }
}
