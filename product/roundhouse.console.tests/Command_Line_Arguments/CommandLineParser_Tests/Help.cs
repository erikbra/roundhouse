using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using log4net;
using NHibernate.Cfg.ConfigurationSchema;
using NSubstitute;
using NUnit.Framework;
using roundhouse.consoles;
using roundhouse.infrastructure.app;
using roundhouse.infrastructure.commandline.options;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class Help
    {
        private CommandLineParser parser;
        private ILog the_logger;

        [SetUp]
        public void SetUp()
        {
            the_logger = Substitute.For<ILog>();
            parser = new CommandLineParser(the_logger);
        }

        [TestCaseSource(nameof(HelpCases))]
        public void  with_value(string val)
        {
            var args = new[] {val};
            ConfigurationPropertyHolder cfg = new DefaultConfiguration();
            
            var ex = Assert.Throws<OptionSetException>( () =>
                parser.parse_arguments_and_set_up_configuration(cfg, args, Program.Mode.Normal)
            );

            ex.Message.Should().StartWith("rh.exe");
            the_logger.ReceivedWithAnyArgs().Info(Arg.Any<string>());
        }

        static IEnumerable<string> HelpCases()
        {
            return
                from f in List("-", "/", "--")
                from a in List("?", "h", "help")
                select $"{f}{a}";
        }
        
    }
}