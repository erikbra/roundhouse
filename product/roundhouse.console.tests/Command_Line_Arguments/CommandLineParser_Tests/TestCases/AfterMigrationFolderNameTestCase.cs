using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class AfterMigrationFolderNameTestCase: TestCaseBase
    {
        public const string expected = "be-fore-mi-gr-ation-folder";
        public AfterMigrationFolderNameTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => 
            List("amg", "aftermig", "aftermigrationfolder", "aftermigrationfoldername");
    }
}