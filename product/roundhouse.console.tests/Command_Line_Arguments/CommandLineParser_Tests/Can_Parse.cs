using FluentAssertions;
using log4net;
using NSubstitute;
using NUnit.Framework;
using roundhouse.consoles;
using roundhouse.infrastructure.app;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class Can_Parse
    {
        private CommandLineParser parser;
        private ILog the_logger;

        [SetUp]
        public void SetUp()
        {
            the_logger = Substitute.For<ILog>();
            parser = new CommandLineParser(the_logger);
        }

        private ConfigurationPropertyHolder get_configuration(string[] args)
        {
            ConfigurationPropertyHolder cfg = new DefaultConfiguration();
            parser.parse_arguments_and_set_up_configuration(cfg, args, Program.Mode.Normal);
            return cfg;
        }

        [TestCaseSource(typeof(DatabaseNameTestCase))]
        public void DatabaseName(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.DatabaseName.Should().Be(DatabaseNameTestCase.expected);
        }

        [TestCaseSource(typeof(ConnectionStringTestCase))]
        public void ConnectionString(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.ConnectionString.Should().Be(ConnectionStringTestCase.expected);
        }

        [TestCaseSource(typeof(ConnectionStringAdminTestCase))]
        public void ConnectionStringAdmin(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.ConnectionStringAdmin.Should().Be(ConnectionStringAdminTestCase.expected);
        }

        [TestCaseSource(typeof(ServerNameTestCase))]
        public void ServerName(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.ServerName.Should().Be(ServerNameTestCase.expected);
        }

        [TestCaseSource(typeof(SqlFilesDirectoryTestCase))]
        public void SqlFilesDirectory(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.SqlFilesDirectory.Should().Be(SqlFilesDirectoryTestCase.expected);
        }

        [TestCaseSource(typeof(CommandTimeoutTestCase))]
        public void CommandTimeout(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.CommandTimeout.Should().Be(CommandTimeoutTestCase.expected);
        }

        [TestCaseSource(typeof(CommandTimeoutAdminTestCase))]
        public void CommandTimeoutAdmin(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.CommandTimeoutAdmin.Should().Be(CommandTimeoutAdminTestCase.expected);
        }

        [TestCaseSource(typeof(AccessTokenTestCase))]
        public void AccessToken(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.AccessToken.Should().Be(AccessTokenTestCase.expected);
        }

        [TestCaseSource(typeof(DatabaseTypeTestCase))]
        public void DatabaseType(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.DatabaseType.Should().Be(DatabaseTypeTestCase.expected);
        }

        [TestCaseSource(typeof(RepositoryPathTestCase))]
        public void RepositoryPath(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.RepositoryPath.Should().Be(RepositoryPathTestCase.expected);
        }

        [TestCaseSource(typeof(VersionTestCase))]
        public void Version(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.Version.Should().Be(VersionTestCase.expected);
        }

        [TestCaseSource(typeof(VersionFileTestCase))]
        public void VersionFile(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.VersionFile.Should().Be(VersionFileTestCase.expected);
        }

        [TestCaseSource(typeof(VersionXPathTestCase))]
        public void VersionXPath(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.VersionXPath.Should().Be(VersionXPathTestCase.expected);
        }

        [TestCaseSource(typeof(RunAfterCreateDatabaseFolderNameTestCase))]
        public void RunAfterCreateDatabaseFolderName(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.RunAfterCreateDatabaseFolderName.Should().Be(RunAfterCreateDatabaseFolderNameTestCase.expected);
        }

        [TestCaseSource(typeof(RunBeforeUpFolderNameTestCase))]
        public void RunBeforeUpFolderName(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.RunBeforeUpFolderName.Should().Be(RunBeforeUpFolderNameTestCase.expected);
        }

        [TestCaseSource(typeof(UpFolderNameTestCase))]
        public void UpFolderName(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.UpFolderName.Should().Be(UpFolderNameTestCase.expected);
        }

        [TestCaseSource(typeof(DownFolderNameTestCase))]
        public void DownFolderName(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.DownFolderName.Should().Be(DownFolderNameTestCase.expected);
        }

        [TestCaseSource(typeof(RunFirstAfterUpFolderNameTestCase))]
        public void RunFirstAfterUpFolderName(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.RunFirstAfterUpFolderName.Should().Be(RunFirstAfterUpFolderNameTestCase.expected);
        }

        [TestCaseSource(typeof(FunctionsFolderNameTestCase))]
        public void FunctionsFolderName(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.FunctionsFolderName.Should().Be(FunctionsFolderNameTestCase.expected);
        }
        
        [TestCaseSource(typeof(ViewsFolderNameTestCase))]
        public void ViewsFolderName(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.ViewsFolderName.Should().Be(ViewsFolderNameTestCase.expected);
        }
        
        [TestCaseSource(typeof(SprocsFolderNameTestCase))]
        public void SprocsFolderName(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.SprocsFolderName.Should().Be(SprocsFolderNameTestCase.expected);
        }
        
        [TestCaseSource(typeof(TriggersFolderNameTestCase))]
        public void TriggersFolderName(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.TriggersFolderName.Should().Be(TriggersFolderNameTestCase.expected);
        }
        
        [TestCaseSource(typeof(IndexesFolderNameTestCase))]
        public void IndexesFolderName(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.IndexesFolderName.Should().Be(IndexesFolderNameTestCase.expected);
        }
        
        [TestCaseSource(typeof(RunAfterOtherAnyTimeScriptsFolderNameTestCase))]
        public void RunAfterOtherAnyTimeScriptsFolderName(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.RunAfterOtherAnyTimeScriptsFolderName.Should().Be(RunAfterOtherAnyTimeScriptsFolderNameTestCase.expected);
        }
        
        [TestCaseSource(typeof(PermissionsFolderNameTestCase))]
        public void PermissionsFolderName(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.PermissionsFolderName.Should().Be(PermissionsFolderNameTestCase.expected);
        }
        
        [TestCaseSource(typeof(BeforeMigrationFolderNameTestCase))]
        public void BeforeMigrationFolderName(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.BeforeMigrationFolderName.Should().Be(BeforeMigrationFolderNameTestCase.expected);
        }
        
        
        [TestCaseSource(typeof(AfterMigrationFolderNameTestCase))]
        public void AfterMigrationFolderName(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.AfterMigrationFolderName.Should().Be(AfterMigrationFolderNameTestCase.expected);
        }

        [TestCaseSource(typeof(SchemaNameTestCase))]
        public void SchemaName(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.SchemaName.Should().Be(SchemaNameTestCase.expected);
        }
        
        [TestCaseSource(typeof(VersionTableNameTestCase))]
        public void VersionTableName(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.VersionTableName.Should().Be(VersionTableNameTestCase.expected);
        }
        
        [TestCaseSource(typeof(ScriptsRunTableNameTestCase))]
        public void ScriptsRunTableName(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.ScriptsRunTableName.Should().Be(ScriptsRunTableNameTestCase.expected);
        }
        
        [TestCaseSource(typeof(ScriptsRunErrorsTableNameTestCase))]
        public void ScriptsRunErrorsTableName(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.ScriptsRunErrorsTableName.Should().Be(ScriptsRunErrorsTableNameTestCase.expected);
        }
        
        [TestCaseSource(typeof(EnvironmentNamesTestCaseComma))]
        public void EnvironmentNamesComma(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.EnvironmentNames.Should().BeEquivalentTo(EnvironmentNamesTestCaseComma.expected);
        }
         
        [TestCaseSource(typeof(EnvironmentNamesTestCaseSemiColon))]
        public void EnvironmentNamesSemiColon(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.EnvironmentNames.Should().BeEquivalentTo(EnvironmentNamesTestCaseSemiColon.expected);
        }
        
    }
}