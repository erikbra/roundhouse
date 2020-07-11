using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class RunAfterCreateDatabaseFolderNameTestCase: TestCaseBase<string>
    {
        public const string expected = "run-after-you-have-humbly-created-the-storage";
        public RunAfterCreateDatabaseFolderNameTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => 
            List("racd", "runaftercreatedatabase", "runaftercreatedatabasefolder", "runaftercreatedatabasefoldername");
    }
}