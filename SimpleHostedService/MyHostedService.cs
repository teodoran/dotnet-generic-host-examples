using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Examples.SimpleHostedService
{
    public class MyHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly Random _random;
        private Timer _timer;

        public MyHostedService(ILogger<MyHostedService> logger)
        {
            _logger = logger;
            _random = new Random();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("MyHostedService Started");

            _timer = new Timer(
                callback: DoWork,
                state: null,
                dueTime: TimeSpan.Zero,
                period: TimeSpan.FromSeconds(2));

            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("MyHostedService Stopped");

            _timer?.Change(Timeout.Infinite, 0);

            await Task.CompletedTask;
        }

        public void Dispose()
        {
            _logger.LogInformation("MyHostedService Disposed");

            _timer?.Dispose();
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("MyHostedService is working");
        }
    }
}