using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class RestoreTimeoutTestCase: TestCaseBase
    {
        public const int expected = 834;
        public RestoreTimeoutTestCase() : base(expected.ToString(), true) { }
        protected override IEnumerable<string> variants() => List("rt", "restoretimeout");
    }
}