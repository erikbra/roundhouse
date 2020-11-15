using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class VersionTestCase: TestCaseBase
    {
        public const string expected = "ver-si-ånn";
        public VersionTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => List("v", "version");
    }
}