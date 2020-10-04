using System.Linq;
using FluentAssertions;
using log4net;
using Microsoft.Extensions.Logging;
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
            var logger = Substitute.For<ILogger>();
            
            var svc = new RoundhouseService(logger, new MainOperations(), new CommandLineParser(logger), false);
            svc.execute(args);

            logger.ReceivedWithAnyArgs().LogInformation(Arg.Any<string>(), Arg.Any<object>(), Arg.Any<object>());
            
            var call = logger.ReceivedCalls().FirstOrDefault();
            var arg = call.GetArguments().FirstOrDefault() as string;
            arg.Should().NotBeNull();
            arg.Should().Contain("from http://projectroundhouse.org.");
        }
        
        [Test]
        public void Is_Not_Reported_When_Version_Is_Part_Of_Another_Argument()
        {
            var args = new[] {"yeyeyeversionyeye"};
            var logger = Substitute.For<ILogger>();
            
            var svc = new RoundhouseService(logger, new MainOperations(), new CommandLineParser(logger), false);
            svc.execute(args);
            
            logger.DidNotReceiveWithAnyArgs().LogInformation(Arg.Any<string>(), Arg.Any<object>(), Arg.Any<object>());
        }
    }
}