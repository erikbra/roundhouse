using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class ConfigurationFileTestCase: TestCaseBase
    {
        public const string expected = "C:\\config\\configfile.json";
        public ConfigurationFileTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => List("cf", "configfile", "configurationfile");
    }
}