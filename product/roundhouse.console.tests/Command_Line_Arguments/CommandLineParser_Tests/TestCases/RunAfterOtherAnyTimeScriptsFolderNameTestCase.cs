using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class RunAfterOtherAnyTimeScriptsFolderNameTestCase: TestCaseBase<string>
    {
        public const string expected = "run-after-a-ny-oth-er-anytime-scripts";
        public RunAfterOtherAnyTimeScriptsFolderNameTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => 
            List("ra", "runAfterOtherAnyTimeScripts", "runAfterOtherAnyTimeScriptsfolder", "runAfterOtherAnyTimeScriptsfoldername");
    }
}