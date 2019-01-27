using System;
using System.Linq;
using System.Net;
using log4net;
using roundhouse.consoles;
using roundhouse.folders;
using roundhouse.infrastructure;
using roundhouse.infrastructure.app;
using roundhouse.infrastructure.app.logging;
using roundhouse.infrastructure.commandline.options;
using roundhouse.infrastructure.containers;
using roundhouse.infrastructure.extensions;
using roundhouse.infrastructure.filesystem;
using roundhouse.init;
using roundhouse.migrators;
using roundhouse.resolvers;
using roundhouse.runners;
using System.Reflection;
using log4net.Repository;
using log4net.Core;

namespace roundhouse.console
{
    public class Program
    {
        private static readonly ILog the_logger = LogManager.GetLogger(typeof(Program));

        public static int Main(string[] args)
        {
            int exit_code = 0;
            
            configure_logging();
            init_security_protocol();
            
            var command_line_parser = new CommandLineParser(args, the_logger);
            var operation_type = command_line_parser.get_operation_type();

            if (operation_type == OperationType.PrintVersion)
            {
                report_version();
                return exit_code;
            }
            else
            {
                var configuration = command_line_parser.parse_arguments_and_set_up_configuration(get_mode(operation_type));
                set_up_configuration_and_build_the_container(configuration);
                exit_code = run_command(operation_type, configuration);
            }
          
            
            catch (Exception ex)
            {
                the_logger.Error(ex.Message, ex);
                exit_code = 1;
            }
            finally
            {
#if DEBUG
           wait_for_user_input();
#endif
            
            return exit_code;
        }
        

        private static void configure_logging() => Log4NetAppender.configure();

        private static Mode get_mode(OperationType operation_type)
            => operation_type == OperationType.Init ? Mode.Init : Mode.Normal;

        private static void wait_for_user_input()
        {
            System.Console.WriteLine("Press any key to continue...");
            System.Console.ReadKey();
        }

        private static int run_command(OperationType operation_type, ConfigurationPropertyHolder configuration)
        {  
            int exit_code = 0;

            try
            {
                switch (operation_type)
                {
                    case OperationType.Diff:
                        run_diff_utility(configuration);
                        break;
                    case OperationType.UpdateCheck:
                        run_update_check(configuration);
                        break;
                    case OperationType.Init:
                        init_folder(configuration);
                        break;
                    default:
                        run_migrator(configuration);
                        break;
                }
            }
            catch (Exception ex)
            {
                the_logger.Error(ex.Message, ex);
                exit_code = 1;
            }

            return exit_code;
        }
        

        private static void report_version()
        {
            string version = VersionInformation.get_current_assembly_version();
            the_logger.InfoFormat("{0} - version {1} from http://projectroundhouse.org.", ApplicationParameters.name,
                version);
        }

        public enum Mode
        {
            Normal,
            Init
        }

        public static void set_up_configuration_and_build_the_container(ConfigurationPropertyHolder configuration)
        {
            ApplicationConfiguraton.set_defaults_if_properties_are_not_set(configuration);
            ApplicationConfiguraton.build_the_container(configuration);
        }

        public static void init_folder(ConfigurationPropertyHolder configuration)
        {
            the_logger.Info("Initializing folder for roundhouse");
            Container.get_an_instance_of<Initializer>().Initialize(configuration, ".");
            Environment.Exit(0);
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

        public static void run_migrator(ConfigurationPropertyHolder configuration)
        {
            RoundhouseMigrationRunner migration_runner = get_migration_runner(configuration);
            migration_runner.run();

            if (!configuration.Silent)
            {
                Console.WriteLine("{0}Please press enter to continue...", Environment.NewLine);
                Console.Read();
            }
        }

        private static void run_diff_utility(ConfigurationPropertyHolder configuration)
        {
            bool silent = configuration.Silent;

            RoundhouseRedGateCompareRunner diff_runner =
                get_diff_runner(configuration, get_migration_runner(configuration));
            diff_runner.run();

            if (!silent)
            {
                Console.WriteLine("{0}Please press enter to continue...", Environment.NewLine);
                Console.Read();
            }
        }

        public static void init_security_protocol()
        {
            // allow tls
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        }

        private static void run_update_check(ConfigurationPropertyHolder configuration)
        {
            if (!configuration.Silent)
            {
                Console.WriteLine("NOTE: Running this command will create the Roundhouse tables, if they don't exist.");
                Console.WriteLine("Please press enter when ready to kick...");
                Console.ReadLine();
            }

            // Info and warn level logging is turned off, in order to make it easy to use the output of this command.
            change_log_level(Level.Error);

            RoundhouseUpdateCheckRunner update_check_runner =
                get_update_check_runner(configuration, get_migration_runner(configuration));
            update_check_runner.run();

            if (!configuration.Silent)
            {
                Console.WriteLine("{0}Please press enter to continue...", Environment.NewLine);
                Console.Read();
            }
        }

        private static RoundhouseMigrationRunner get_migration_runner(ConfigurationPropertyHolder configuration)
        {
            return new RoundhouseMigrationRunner(
                configuration.RepositoryPath,
                Container.get_an_instance_of<environments.EnvironmentSet>(),
                Container.get_an_instance_of<KnownFolders>(),
                Container.get_an_instance_of<FileSystemAccess>(),
                Container.get_an_instance_of<DatabaseMigrator>(),
                Container.get_an_instance_of<VersionResolver>(),
                configuration.Silent,
                configuration.Drop,
                configuration.DoNotCreateDatabase,
                configuration.WithTransaction,
                configuration);
        }

        private static RoundhouseRedGateCompareRunner get_diff_runner(ConfigurationPropertyHolder configuration,
            RoundhouseMigrationRunner migration_runner)
        {
            return new RoundhouseRedGateCompareRunner(
                Container.get_an_instance_of<KnownFolders>(),
                Container.get_an_instance_of<FileSystemAccess>(),
                configuration, migration_runner);
        }

        private static RoundhouseUpdateCheckRunner get_update_check_runner(ConfigurationPropertyHolder configuration,
            RoundhouseMigrationRunner migration_runner)
        {
            return new RoundhouseUpdateCheckRunner(
                Container.get_an_instance_of<environments.EnvironmentSet>(),
                Container.get_an_instance_of<KnownFolders>(),
                Container.get_an_instance_of<FileSystemAccess>(),
                Container.get_an_instance_of<DatabaseMigrator>(),
                configuration,
                migration_runner);
        }
    }
}