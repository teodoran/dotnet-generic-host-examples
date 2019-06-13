using System;
using Microsoft.Extensions.Logging;

namespace Examples.SimpleConsoleService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = CreateLogger();
            using (var simpleService = new MySimpleService(logger))
            {
                simpleService.Start();

                Console.ReadKey();
            }

            logger.LogInformation("MySimpleService Stopped");
        }

        private static ILogger<MySimpleService> CreateLogger()
        {
            var loggerFactory = new LoggerFactory();

            #pragma warning disable 618
            loggerFactory.AddConsole().AddDebug();
            #pragma warning restore 618

            return loggerFactory.CreateLogger<MySimpleService>();
        }
    }
}
