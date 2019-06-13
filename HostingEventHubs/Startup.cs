using Examples.Host.Generic;
using Examples.HostingEventHubs.EventHubs;
using Hafslund.Configuration;
using Hafslund.Telemetry;
using Microsoft.Azure.EventHubs.Processor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Examples.HostingEventHubs
{
    public class Startup : IStartup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services, IHostingEnvironment environment)
        {
            var writeToConsole = Configuration.EnsureHasValue("logging-configuration:write-to-console");
            var loggingConfig = new HafslundNettTelemetryLoggingConfig
            {
                WriteToConsole = bool.Parse(writeToConsole),
            };

            services.AddStandardHafslundNettTelemetryLogging(loggingConfig);

            services.AddTransient<IPayloadProcessor, MessageProcessor>();
            services.AddTransient<IEventProcessorFactory, ProcessorFactory>();
            services.AddSingleton<EventProcessorHost>(provider =>
                new EventProcessorHost(
                    eventHubPath: Configuration.EnsureHasValue("processor-configuration:eventhub-path"),
                    consumerGroupName: Configuration.EnsureHasValue("processor-configuration:consumergroup-name"),
                    eventHubConnectionString: Configuration.EnsureConnectionString("processor-configuration:eventhub-connectionstring"),
                    storageConnectionString: Configuration.EnsureConnectionString("processor-configuration:storage-connectionstring"),
                    leaseContainerName: Configuration.EnsureHasValue("processor-configuration:lease-containername")));

            services.AddHostedService<MessageProcessorService>();
        }
    }
}
