using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class ScriptsRunErrorsTableNameTestCase: TestCaseBase<string>
    {
        public const string expected = "script-s-run-error-z";
        public ScriptsRunErrorsTableNameTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => 
            List("sret", "scriptsrunerrorstable", "scriptsrunerrorstablename");
    }
}