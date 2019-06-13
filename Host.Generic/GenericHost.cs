using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Examples.Host.Generic
{
    public static class GenericHost
    {
        public static IHostBuilder CreateDefaultBuilder(string[] args) =>
            new HostBuilder()
                .ConfigureAppConfiguration((context, config) => config
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args));

        public static IHostBuilder UseStartup<TStartup>(this IHostBuilder hostBuilder, Func<IConfiguration, TStartup> factory)
            where TStartup : IStartup =>
                hostBuilder
                    .ConfigureServices((context, services) =>
                    {
                        context.HostingEnvironment.SetEnvironmentNameToAspNetCoreEnvironment();

                        var startup = factory(context.Configuration);
                        startup.ConfigureServices(services, context.HostingEnvironment);
                    });

        /*
         * Generic Host isn't updating IHostingEnvironment based on the ASPNETCORE_ENVIRONMENT environment variable.
         * This causes confusion when using interface members like IsProduction() on IHostingEnvironment.
         * To remedy this, we'll update EnvironmentName based on ASPNETCORE_ENVIRONMENT here.
         *
         * This issue should be resolved in ASP.NET Core 3: https://github.com/aspnet/AspNetCore/issues/4150
         */
        private static void SetEnvironmentNameToAspNetCoreEnvironment(this IHostingEnvironment environment)
        {
            var aspNetCoreEnvironment = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (string.IsNullOrEmpty(aspNetCoreEnvironment))
            {
                environment.EnvironmentName = EnvironmentName.Production;
            }
            else
            {
                environment.EnvironmentName = aspNetCoreEnvironment;
            }
        }
    }
}
