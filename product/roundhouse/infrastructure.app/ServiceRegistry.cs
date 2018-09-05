using System;
using Lamar;
using roundhouse.cryptography;
using roundhouse.databases;
using roundhouse.environments;
using roundhouse.folders;
using roundhouse.infrastructure.app.builders;
using roundhouse.infrastructure.filesystem;
using roundhouse.infrastructure.logging;
using roundhouse.infrastructure.logging.custom;
using roundhouse.init;
using roundhouse.migrators;
using roundhouse.resolvers;

namespace roundhouse.infrastructure.app
{
    [CLSCompliant(false)]
    public class ServiceRegistry: Lamar.ServiceRegistry
    {
        public ServiceRegistry(ConfigurationPropertyHolder configuration_property_holder, Logger logger)
        {
            For<ConfigurationPropertyHolder>().Use(configuration_property_holder);
            For<FileSystemAccess>().Use(context => new DotNetFileSystemAccess(configuration_property_holder)).Singleton();
            For<Database>().Use(context => DatabaseBuilder.build(context.GetInstance<FileSystemAccess>(), configuration_property_holder)).Singleton();
            For<KnownFolders>().Use(context => KnownFoldersBuilder.build(context.GetInstance<FileSystemAccess>(), configuration_property_holder)).Singleton();
            For<LogFactory>().Use<MultipleLoggerLogFactory>().Singleton();
            //For<Logger>().Singleton().Use(context => LogBuilder.build(context.GetInstance<FileSystemAccess>(), configuration_property_holder));
            For<Logger>().Use(logger);
            For<CryptographicService>().Use<MD5CryptographicService>().Singleton();
            For<DatabaseMigrator>()
                .Use(context => new DefaultDatabaseMigrator(
                    context.GetInstance<Database>(), 
                    context.GetInstance<CryptographicService>(), configuration_property_holder))
                .Singleton()
                ;
            For<VersionResolver>()
                .Use(
                    context => VersionResolverBuilder.build(context.GetInstance<FileSystemAccess>(), configuration_property_holder))
                .Singleton();
            For<EnvironmentSet>().Use(new DefaultEnvironmentSet(configuration_property_holder)).Singleton();
            For<Initializer>().Use<FileSystemInitializer>().Singleton();
        }
        
    }
}