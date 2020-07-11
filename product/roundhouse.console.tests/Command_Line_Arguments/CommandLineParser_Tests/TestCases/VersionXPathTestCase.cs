using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class VersionXPathTestCase: TestCaseBase<string>
    {
        public const string expected = "ver-si-on-x-path";
        public VersionXPathTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => List("vx", "versionxpath");
    }
}