using Examples.Host.Generic;
using Hafslund.Configuration;
using Hafslund.Telemetry;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Examples.AspNetInspiredStartup
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

            services.AddHostedService<MyHostedService>();
        }
    }
}
