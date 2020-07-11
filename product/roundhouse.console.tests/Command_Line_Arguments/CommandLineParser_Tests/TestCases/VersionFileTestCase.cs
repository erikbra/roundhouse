using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class VersionFileTestCase: TestCaseBase<string>
    {
        public const string expected = "ver-si-ånn-fail";
        public VersionFileTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => List("vf", "versionfile");
    }
}