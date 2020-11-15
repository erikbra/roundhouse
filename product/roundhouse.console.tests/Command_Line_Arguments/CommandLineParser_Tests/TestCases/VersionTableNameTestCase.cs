using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class VersionTableNameTestCase: TestCaseBase
    {
        public const string expected = "be-fore-mi-gr-ation-folder";
        public VersionTableNameTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => 
            List("vt", "versiontable", "versiontablename");
    }
}