using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WebApi
{
    public class LogHelper
    {
        public static ILogger<Program> Logger()
        {
            var provider = new ServiceCollection()
               .AddLogging(x => x.SetMinimumLevel(0))
               .BuildServiceProvider();

            var factory = provider.GetService<ILoggerFactory>();
            factory.AddConsole();
            factory.AddDebug();

            var logger = provider.GetService<ILoggerFactory>()?.CreateLogger<Program>();
            return logger;
        }
    }
}
