using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace roundhouse.console
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging(l => l.AddConsole())
                .AddSingleton<RoundhouseService>()
                .AddSingleton(new CommandLineArguments {args = args})
                .BuildServiceProvider();
            
            await Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<RoundhouseService>();
                })
                .RunConsoleAsync();

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");

            //do the actual work here
            // var service = serviceProvider.GetService<RoundhouseService>();
            // var return_value = service.execute(args);

            logger.LogDebug("All done!");

            return 0;
        }

        public enum Mode
        {
            Normal,
            Init
        }

    }
}
