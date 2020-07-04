using System.Linq;
using FluentAssertions;
using log4net;
using NSubstitute;
using NUnit.Framework;
using roundhouse.console;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class Version
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Is_Reported_When_Version_Is_Specified()
        {
            var args = new[] {"--version"};
            var logger = Substitute.For<ILog>();

            Program.the_logger = logger;
            Program.should_wait_for_keypress = false;
            Program.Main(args);
            
            logger.ReceivedWithAnyArgs().InfoFormat(Arg.Any<string>(), Arg.Any<object>(), Arg.Any<object>());
            
            var call = logger.ReceivedCalls().FirstOrDefault();
            var arg = call.GetArguments().FirstOrDefault() as string;
            arg.Should().NotBeNull();
            arg.Should().Contain("from http://projectroundhouse.org.");
        }
        
        [Test]
        public void Is_Not_Reported_When_Version_Is_Part_Of_Another_Argument()
        {
            var args = new[] {"yeyeyeversionyeye"};
            var logger = Substitute.For<ILog>();

            Program.the_logger = logger;
            Program.should_wait_for_keypress = false;
            Program.Main(args);
            
            logger.DidNotReceiveWithAnyArgs().InfoFormat(Arg.Any<string>(), Arg.Any<object>(), Arg.Any<object>());
        }
    }
}