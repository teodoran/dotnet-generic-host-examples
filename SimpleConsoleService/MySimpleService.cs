using System;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace Examples.SimpleConsoleService
{
    public class MySimpleService : IDisposable
    {
        private readonly ILogger _logger;
        private readonly Random _random;
        private Timer _timer;

        public MySimpleService(ILogger<MySimpleService> logger)
        {
            _logger = logger;
            _random = new Random();
        }

        public void Start()
        {
            _logger.LogInformation("MySimpleService Started");

            _timer = new Timer(
                callback: DoWork,
                state: null,
                dueTime: TimeSpan.Zero,
                period: TimeSpan.FromSeconds(2));
        }

        public void Dispose()
        {
            _logger.LogInformation("MySimpleService Disposed");

            _timer?.Dispose();
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("MySimpleService is working");
        }
    }
}