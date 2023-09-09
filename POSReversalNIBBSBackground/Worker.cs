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
                var allTransaction = dbHelper.GetAll();
                if (allTransaction.Count() > 0)
                    dbHelper.UpdateAllTheRecords(allTransaction);

                DbReversal dbReversal = new DbReversal();
                var allReversal = dbReversal.GetAll();
                if (allReversal.Count() > 0)
                    dbReversal.UpdateAllTheRecords(allReversal);

                DRP dRP = new DRP();
                var allSendDRP = dRP.GetAll();
                if (allSendDRP.Count() > 0)
                    await dRP.SendToDRP();

                await Task.Delay(10000, stoppingToken); // Delay for 10 seconds
            }

            //while (true) // Run indefinitely
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    DbHelper dbHelper = new DbHelper();
            //    var allTransaction = dbHelper.GetAll();
            //    if (allTransaction.Count() > 0)
            //        dbHelper.UpdateAllTheRecords(allTransaction);

            //    DbReversal dbReversal = new DbReversal();
            //    var allReversal = dbReversal.GetAll();
            //    if (allReversal.Count() > 0)
            //        dbReversal.UpdateAllTheRecords(allReversal);

            //    await Task.Delay(10000); // Delay for 10 seconds
            //}
        }
    }
}