using System;
using System.IO;
using NUnit.Framework;

namespace roundhouse.tests.integration.databases
{
    using roundhouse.databases.mysql;
    using roundhouse.infrastructure.logging;
    using roundhouse.infrastructure.logging.custom;

    public class MySqlDatabaseSpecs
    {
        public abstract class concern_for_MySqlDatabase :  TinySpec, IDisposable
        {
            private static string RootDir = TestEnvironment.solution_root;
            
            protected static string database_name = "TestRoundhousE";
            protected static string sql_files_folder = TestEnvironment.test_script_dir("MySQL", database_name);
            
            public void Dispose()
            {
                new Migrate().Set(p =>
                {
                    p.ConnectionString = $"server=localhost;uid=root;database={database_name};";
                    p.SqlFilesDirectory = sql_files_folder;
                    p.DatabaseType = "mysql";
                    p.Drop = true;
                    p.Silent = true;
                }).Run();
            }
        }

        [Concern(typeof(MySqlDatabase))]
        public class when_running_the_migrator_with_mysql : concern_for_MySqlDatabase
        {
            protected static object result;

            public override void Context() { }
            public override void Because() 
            {
                new Migrate().Set(p =>
                {
                    p.Logger = new ConsoleLogger();
                    p.ConnectionString = $"server=localhost;uid=root;database={database_name};";
                    p.SqlFilesDirectory = sql_files_folder;
                    p.DatabaseType = "mysql";
                    p.Silent = true;
                }).Run();
            }

            [Observation]
            public void should_successfully_run()
            {
                //nothing needed here
            }
        }
    }
}