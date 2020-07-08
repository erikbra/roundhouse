using System;
using System.Net;
using log4net;
using roundhouse.consoles;
using roundhouse.databases;
using roundhouse.infrastructure;
using roundhouse.infrastructure.app;
using roundhouse.infrastructure.app.tokens;
using roundhouse.infrastructure.app.logging;
using roundhouse.infrastructure.commandline.options;
using roundhouse.runners;
using System.Reflection;
using log4net.Repository;
using Newtonsoft.Json;
using System.Collections.Generic;
using log4net.Core;
using System.Text;

namespace roundhouse.console
{
    public class Program
    {
        public static ILog the_logger = LogManager.GetLogger(typeof(Program));
        public static bool should_wait_for_keypress { get; set; }
        
        public static IMainOperations main_operations { get; set; } = new MainOperations();
        public static CommandLineParser command_line_parser { get; set; } = new CommandLineParser(the_logger);
        
#if DEBUG
        static Program()
        {
            should_wait_for_keypress = true;
        }
#endif

        public static int Main(string[] args)
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
                the_logger.Error(ex.Message, ex);
                return 1;
            }
            finally
            {
                wait_for_keypress();
            }
        }

        private static Mode get_mode(string[] args) => OperationParser.is_init(args) ? Mode.Init : Mode.Normal;

        private static IRunner get_runner(MainOperation operation, ConfigurationPropertyHolder config)
        {
            var migration_runner = main_operations.get_migration_runner(config);
            return operation switch
            {
                MainOperation.RunDiffUtility => main_operations.get_diff_runner(config, migration_runner),
                MainOperation.RunIsUpToDate => main_operations.get_update_check_runner(config, migration_runner),
                MainOperation.RunMigrator => migration_runner
            };
        }


        private static void wait_for_keypress()
        {
            if (should_wait_for_keypress)
            {
                Console.WriteLine("{0}Press any key to continue...", Environment.NewLine);
                Console.ReadKey();
            }
        }

        private static int report_version()
        {
            the_logger.InfoFormat(
                "{0} - version {1} from http://projectroundhouse.org.", 
                ApplicationParameters.name, 
                VersionInformation.get_current_assembly_version());
            return 0;
        }

        public enum Mode
        {
            Normal,
            Init
        }


        private static void show_help(string message, OptionSet option_set)
        {
            //Console.Error.WriteLine(message);
            the_logger.Info(message);
            option_set.WriteOptionDescriptions(Console.Error);
        }

        private static int init_folder(ConfigurationPropertyHolder configuration)
        {
            the_logger.Info("Initializing folder for roundhouse");
            var initializer = main_operations.get_initializer();
            initializer.Initialize(configuration, ".");
            return 0;
        }
        
        private static void change_log_level(Level level)
        {
            ILoggerRepository log_repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
            log_repository.Threshold = level;
            foreach (ILogger log in log_repository.GetCurrentLoggers())
            {
                var logger = log as log4net.Repository.Hierarchy.Logger;
                if (logger != null)
                {
                    logger.Level = level;
                }
            }
        }


        private static void init_security_protocol()
        {
            // allow tls
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        }
        
        private static int run(IRunner runner, bool silent)
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

    }
}
