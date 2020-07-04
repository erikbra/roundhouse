using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class CommandTimeoutAdminTestCase: TestCaseBase
    {
        public const int expected = 100_000_111;
        public CommandTimeoutAdminTestCase() : base(expected.ToString(), true) { }
        protected override IEnumerable<string> variants() => List("cta", "commandtimeoutadmin");
    }
}