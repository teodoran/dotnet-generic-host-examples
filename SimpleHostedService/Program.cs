using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Examples.SimpleHostedService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var hostBuilder = new HostBuilder();
            var host = hostBuilder
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<MyHostedService>();
                })
                .ConfigureLogging((hostContext, configLogging) =>
                {
                    configLogging.AddConsole();
                    configLogging.AddDebug();
                });

            await host.RunConsoleAsync();
        }
    }
}
