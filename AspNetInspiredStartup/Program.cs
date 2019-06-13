using System.Threading.Tasks;
using Examples.Host.Generic;
using Hafslund.Configuration;
using Microsoft.Extensions.Hosting;

namespace Examples.AspNetInspiredStartup
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args)
                .RunConsoleAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            GenericHost
                .CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) => config.AddSecrets())
                .UseStartup(config => new Startup(config));
    }
}
