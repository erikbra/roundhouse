using System.Linq;
using FluentAssertions;
using log4net;
using NSubstitute;
using NUnit.Framework;
using roundhouse.infrastructure.app;
using roundhouse.runners;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class Main_Modi_Operandi
    {
        [Test]
        public void Version()
        {
            var args = new[] {"--version"};
            var logger = Substitute.For<ILog>();

            Program.the_logger = logger;
            Program.should_wait_for_keypress = false;
            
            int result = Program.Main(args);
            result.Should().Be(0);
            
            logger.ReceivedWithAnyArgs().InfoFormat(Arg.Any<string>(), Arg.Any<object>(), Arg.Any<object>());
            var call = logger.ReceivedCalls().FirstOrDefault();
            var arg = call.GetArguments().FirstOrDefault() as string;
            arg.Should().NotBeNull();
            arg.Should().Contain("from http://projectroundhouse.org.");
        }
        
        [Test]
        public void Redgate_Diff()
        {
            var args = new[] {"--rh.redgate.diff", "-d", "jalla"};
            var logger = Substitute.For<ILog>();

            Program.the_logger = logger;
            Program.should_wait_for_keypress = false;
            var main_operations = Substitute.For<IMainOperations>();
            Program.main_operations = main_operations;
            
            var result = Program.Main(args);
            //result.Should().Be(0);

            main_operations.ReceivedWithAnyArgs().
                get_diff_runner(
                    Arg.Any<ConfigurationPropertyHolder>(), 
                    Arg.Any<RoundhouseMigrationRunner>());
        }
        
        [Test]
        public void IsUptoDate()
        {
            var args = new[] {"--isuptodate", "-d", "jalla"};
            var logger = Substitute.For<ILog>();

            Program.the_logger = logger;
            Program.should_wait_for_keypress = false;
            var main_operations = Substitute.For<IMainOperations>();
            Program.main_operations = main_operations;
            
            var result = Program.Main(args);
            //result.Should().Be(0);

            main_operations.ReceivedWithAnyArgs().
                get_update_check_runner(
                    Arg.Any<ConfigurationPropertyHolder>(), 
                    Arg.Any<RoundhouseMigrationRunner>());
        }
                
        [Test]
        public void Init()
        {
            var args = new[] {"init", "-d", "jalla"};
            var logger = Substitute.For<ILog>();

            Program.the_logger = logger;
            Program.should_wait_for_keypress = false;
            var main_operations = Substitute.For<IMainOperations>();
            Program.main_operations = main_operations;
            
            var result = Program.Main(args);
            //result.Should().Be(0);

            main_operations.ReceivedWithAnyArgs().get_initializer();
        }
        
                        
        [Test]
        public void Migrator()
        {
            var args = new[] {"-d", "jalla"};
            var logger = Substitute.For<ILog>();

            Program.the_logger = logger;
            Program.should_wait_for_keypress = false;
            var main_operations = Substitute.For<IMainOperations>();
            Program.main_operations = main_operations;
            
            var result = Program.Main(args);
            //result.Should().Be(0);

            main_operations.ReceivedWithAnyArgs().get_migration_runner(
                Arg.Any<ConfigurationPropertyHolder>());
        }
    }
}