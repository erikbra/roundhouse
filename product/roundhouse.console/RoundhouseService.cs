using System;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using log4net.Core;
using log4net.Repository;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using roundhouse.infrastructure;
using roundhouse.infrastructure.app;
using roundhouse.infrastructure.app.logging;
using roundhouse.infrastructure.commandline.options;
using roundhouse.runners;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace roundhouse.console
{
    public class RoundhouseService: IHostedService 
    {
        private ILogger the_logger;

#if DEBUG
        public bool should_wait_for_keypress { get; set; } = true;
#else 
        public bool should_wait_for_keypress { get; set; }
#endif

        private IMainOperations main_operations;
        private CommandLineParser command_line_parser;
        private readonly CommandLineArguments arguments;

        public RoundhouseService(ILogger the_logger, IMainOperations main_operations, CommandLineParser command_line_parser, CommandLineArguments arguments, bool? should_wait_for_keypress)
        {
            this.the_logger = the_logger;
            this.main_operations = main_operations;
            this.command_line_parser = command_line_parser;
            this.arguments = arguments;
            this.should_wait_for_keypress = should_wait_for_keypress ?? this.should_wait_for_keypress;
        }
        
        
        public int execute(string[] args)
        {
            Log4NetAppender.configure();
            init_security_protocol();

            try
            {
                var operation = OperationParser.get_main_operation(args);
                ConfigurationPropertyHolder config = null;

                if (operation != MainOperation.ReportVersion)
                {
                    config = command_line_parser.set_up_configuration_and_build_the_container(args, get_mode(args));
                }
                
                return operation switch
                {
                    MainOperation.ReportVersion => report_version(),
                    MainOperation.RunInit => init_folder(config),
                    _ => run(get_runner(operation, config), config.Silent)
                };
            }

            catch (OptionSetException ex)
            {
                show_help(ex.Message, ex.options);
                return -1;
            }
            catch (Exception ex)
            {
                the_logger.LogError(ex, ex.Message);
                return 1;
            }
            finally
            {
                wait_for_keypress();
            }
        }

        private Program.Mode get_mode(string[] args) => OperationParser.is_init(args) ? Program.Mode.Init : Program.Mode.Normal;

        private IRunner get_runner(MainOperation operation, ConfigurationPropertyHolder config)
        {
            var migration_runner = main_operations.get_migration_runner(config);
            return operation switch
            {
                MainOperation.RunDiffUtility => main_operations.get_diff_runner(config, migration_runner),
                MainOperation.RunIsUpToDate => main_operations.get_update_check_runner(config, migration_runner),
                MainOperation.RunMigrator => migration_runner
            };
        }


        private void wait_for_keypress()
        {
            if (should_wait_for_keypress)
            {
                Console.WriteLine("{0}Press any key to continue...", Environment.NewLine);
                Console.ReadKey();
            }
        }

        private int report_version()
        {
            the_logger.LogInformation(
                "{0} - version {1} from http://projectroundhouse.org.", 
                ApplicationParameters.name, 
                VersionInformation.get_current_assembly_version());
            return 0;
        }

        private void show_help(string message, OptionSet option_set)
        {
            //Console.Error.WriteLine(message);
            the_logger.LogInformation(message);
            option_set.WriteOptionDescriptions(Console.Error);
        }

        private int init_folder(ConfigurationPropertyHolder configuration)
        {
            the_logger.LogInformation("Initializing folder for roundhouse");
            var initializer = main_operations.get_initializer();
            initializer.Initialize(configuration, ".");
            return 0;
        }
        
        // private static void change_log_level(Level level)
        // {
        //     ILoggerRepository log_repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
        //     log_repository.Threshold = level;
        //     foreach (ILogger log in log_repository.GetCurrentLoggers())
        //     {
        //         var logger = log as log4net.Repository.Hierarchy.Logger;
        //         if (logger != null)
        //         {
        //             logger.Level = level;
        //         }
        //     }
        // }


        private static void init_security_protocol()
        {
            // allow tls
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        }
        
        private int run(IRunner runner, bool silent)
        {
            if (!silent && should_wait_for_keypress)
            {
                Console.WriteLine("NOTE: Running this command will create the Roundhouse tables, if they don't exist.");
                Console.WriteLine("Please press enter when ready to kick...");
                Console.ReadLine();
            }
            
            runner.run();
            return 0;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            this.execute(this.arguments.args);
            await Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}