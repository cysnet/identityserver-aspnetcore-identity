using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace web2
{
    public class GracePeriodManagerService : BackgroundService
    {

        private readonly ILogger _logger;
        public GracePeriodManagerService(ILogger<GracePeriodManagerService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Hello World! - {DateTime.Now}");

                await Task.Delay(5000, stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            // Run your graceful clean-up actions
        }
    }
}
