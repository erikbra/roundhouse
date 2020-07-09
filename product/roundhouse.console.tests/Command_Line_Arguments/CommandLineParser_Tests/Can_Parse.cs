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
        
        [TestCaseSource(typeof(DatabaseNameTestCase)) ]
        public void DatabaseName(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.DatabaseName.Should().Be(DatabaseNameTestCase.expected);
        }
        
        [TestCaseSource(typeof(ConnectionStringTestCase)) ]
        public void ConnectionString(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.ConnectionString.Should().Be(ConnectionStringTestCase.expected);
        }
    
        [TestCaseSource(typeof(ConnectionStringAdminTestCase)) ]
        public void ConnectionStringAdmin(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.ConnectionStringAdmin.Should().Be(ConnectionStringAdminTestCase.expected);
        }
        
        [TestCaseSource(typeof(ServerNameTestCase)) ]
        public void ServerName(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.ServerName.Should().Be(ServerNameTestCase.expected);
        }
        
        [TestCaseSource(typeof(SqlFilesDirectoryTestCase)) ]
        public void SqlFilesDirectory(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.SqlFilesDirectory.Should().Be(SqlFilesDirectoryTestCase.expected);
        }
        
        [TestCaseSource(typeof(CommandTimeoutTestCase)) ]
        public void CommandTimeout(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.CommandTimeout.Should().Be(CommandTimeoutTestCase.expected);
        }
        
        [TestCaseSource(typeof(CommandTimeoutAdminTestCase)) ]
        public void CommandTimeoutAdmin(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.CommandTimeoutAdmin.Should().Be(CommandTimeoutAdminTestCase.expected);
        }
           
        [TestCaseSource(typeof(AccessTokenTestCase)) ]
        public void AccessToken(params string[] args)
        {
            var cfg = get_configuration(args);
            cfg.AccessToken.Should().Be(AccessTokenTestCase.expected);
        }
    }
}