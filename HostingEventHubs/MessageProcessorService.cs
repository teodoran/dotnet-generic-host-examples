using System;
using System.Threading;
using System.Threading.Tasks;
using Hafslund.Telemetry;
using Microsoft.Azure.EventHubs.Processor;
using Microsoft.Extensions.Hosting;

namespace Examples.HostingEventHubs
{
    public class MessageProcessorService : IHostedService
    {
        private readonly ITelemetryInsightsLogger _logger;
        private readonly IEventProcessorFactory _factory;
        private readonly EventProcessorHost _host;
        private readonly EventProcessorOptions _options;

        public MessageProcessorService(ITelemetryInsightsLogger logger, IEventProcessorFactory factory, EventProcessorHost host)
        {
            _logger = logger;
            _factory = factory;
            _host = host;

            _options = new EventProcessorOptions
            {
                MaxBatchSize = 200,
                PrefetchCount = 500,
                ReceiveTimeout = TimeSpan.FromMinutes(2),
            };
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _options.SetExceptionHandler(args =>
            {
                _logger.TrackException(args.Exception, new
                {
                    Partition = args.PartitionId,
                });
            });

            await _host.RegisterEventProcessorFactoryAsync(_factory, _options);

            _logger.TrackEvent("MessageEventProcessorServiceStarted");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _host.UnregisterEventProcessorAsync();

            _logger.TrackEvent("MessageEventProcessorServiceStopped");
        }
    }
}