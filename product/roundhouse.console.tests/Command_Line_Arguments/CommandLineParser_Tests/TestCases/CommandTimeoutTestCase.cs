using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class CommandTimeoutTestCase: TestCaseBase<int>
    {
        public const int expected = 102312089;
        public CommandTimeoutTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => List("ct", "commandtimeout");
    }
}