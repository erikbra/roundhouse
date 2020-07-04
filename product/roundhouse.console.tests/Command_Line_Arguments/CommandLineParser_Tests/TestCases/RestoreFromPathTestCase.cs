using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class RestoreFromPathTestCase: TestCaseBase
    {
        public const string expected = "it-s-da-path";
        public RestoreFromPathTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => List("rfp", "restorefrom", "restorefrompath");
    }
}