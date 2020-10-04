using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace roundhouse.console
{
    public class Program
    {
        public static int Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging(l => l.AddConsole())
                .AddSingleton<RoundhouseService>()
                .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");

            //do the actual work here
            var service = serviceProvider.GetService<RoundhouseService>();
            var return_value = service.execute(args);

            logger.LogDebug("All done!");

            return return_value;
        }

        public enum Mode
        {
            Normal,
            Init
        }

    }
}
