using roundhouse.folders;
using roundhouse.infrastructure.app;
using roundhouse.infrastructure.containers;
using roundhouse.infrastructure.filesystem;
using roundhouse.init;
using roundhouse.migrators;
using roundhouse.resolvers;
using roundhouse.runners;

namespace roundhouse.console
{
    public class MainOperations: IMainOperations
    {
        public RoundhouseMigrationRunner get_migration_runner(ConfigurationPropertyHolder configuration)
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

        public RoundhouseRedGateCompareRunner get_diff_runner(ConfigurationPropertyHolder configuration, RoundhouseMigrationRunner migration_runner)
        {
            return new RoundhouseRedGateCompareRunner(
                Container.get_an_instance_of<KnownFolders>(),
                Container.get_an_instance_of<FileSystemAccess>(),
                configuration, migration_runner);
        }

        public RoundhouseUpdateCheckRunner get_update_check_runner(ConfigurationPropertyHolder configuration, RoundhouseMigrationRunner migration_runner)
        {
            return new RoundhouseUpdateCheckRunner(
                Container.get_an_instance_of<environments.EnvironmentSet>(),
                Container.get_an_instance_of<KnownFolders>(),
                Container.get_an_instance_of<FileSystemAccess>(),
                Container.get_an_instance_of<DatabaseMigrator>(), 
                configuration,
                migration_runner);
        }

        public Initializer get_initializer() => Container.get_an_instance_of<Initializer>();
    }

    public interface IMainOperations
    {
        RoundhouseMigrationRunner get_migration_runner(ConfigurationPropertyHolder configuration);
        RoundhouseRedGateCompareRunner get_diff_runner(ConfigurationPropertyHolder configuration, RoundhouseMigrationRunner migration_runner);
        RoundhouseUpdateCheckRunner get_update_check_runner(ConfigurationPropertyHolder configuration, RoundhouseMigrationRunner migration_runner);
        Initializer get_initializer();
    }

    public enum MainOperation
    {
        ReportVersion,
        RunDiffUtility,
        RunIsUpToDate,
        RunInit,
        RunMigrator
    }
}