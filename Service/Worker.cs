using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SenderMQ.Service
{
    public class Worker : BackgroundService
    {
        private int count = 0;

        private readonly Sender sender = new("neon");

        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (count < 10001)
            {
                sender.SendMessage($"Test message for queue: {DateTimeOffset.Now}");

                _logger.LogInformation($"Worker {count} running at: {DateTimeOffset.Now}");
                count++;
            }
            return Task.CompletedTask;
        }
    }
}
