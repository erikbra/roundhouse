using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class RunFirstAfterUpFolderNameTestCase: TestCaseBase
    {
        public const string expected = "run-firs-tafter-u-p";
        public RunFirstAfterUpFolderNameTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => 
            List("rf", "runfirst", "runfirstfolder", "runfirstafterupdatefolder", "runfirstafterupdatefoldername");
    }
}