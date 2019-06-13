using System.Threading;
using System.Threading.Tasks;
using Hafslund.Telemetry;
using Microsoft.Extensions.Hosting;

namespace Examples.AspNetInspiredStartup
{
    public class MyHostedService : IHostedService
    {
        private readonly ITelemetryInsightsLogger _logger;

        public MyHostedService(ITelemetryInsightsLogger logger)
        {
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.TrackEvent("MyHostedService Started");

            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.TrackEvent("MyHostedService Stopped");

            await Task.CompletedTask;
        }
    }
}