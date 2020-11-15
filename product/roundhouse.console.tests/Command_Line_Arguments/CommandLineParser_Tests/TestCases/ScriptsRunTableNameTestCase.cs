using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class ScriptsRunTableNameTestCase: TestCaseBase
    {
        public const string expected = "script-s-run";
        public ScriptsRunTableNameTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => 
            List("srt", "scriptsruntable", "scriptsruntablename");
    }
}