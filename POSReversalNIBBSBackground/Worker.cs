using POSReversalNIBBSBackground.Services;

namespace POSReversalNIBBSBackground
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                DbHelper dbHelper = new DbHelper();
              var allTransaction=  dbHelper.GetAll();
                if(allTransaction.Count()>0)
                    dbHelper.UpdateAllTheRecords(allTransaction);

                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}