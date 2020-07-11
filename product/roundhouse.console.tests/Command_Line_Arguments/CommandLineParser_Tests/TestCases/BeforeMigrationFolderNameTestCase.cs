using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class BeforeMigrationFolderNameTestCase: TestCaseBase
    {
        public const string expected = "be-fore-mi-gr-ation-folder";
        public BeforeMigrationFolderNameTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => 
            List("bmg", "beforemig", "beforemigrationfolder", "beforemigrationfoldername");
    }
}