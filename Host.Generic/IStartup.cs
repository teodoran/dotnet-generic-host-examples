using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Examples.Host.Generic
{
    public interface IStartup
    {
        void ConfigureServices(IServiceCollection services, IHostingEnvironment environment);
    }
}
