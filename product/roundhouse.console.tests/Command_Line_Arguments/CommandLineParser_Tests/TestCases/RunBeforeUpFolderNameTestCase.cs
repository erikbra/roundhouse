using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class RunBeforeUpFolderNameTestCase: TestCaseBase<string>
    {
        public const string expected = "run-before-folda-up";
        public RunBeforeUpFolderNameTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => 
            List("rb", "runbefore", "runbeforeupfolder", "runbeforeupfoldername");
    }
}