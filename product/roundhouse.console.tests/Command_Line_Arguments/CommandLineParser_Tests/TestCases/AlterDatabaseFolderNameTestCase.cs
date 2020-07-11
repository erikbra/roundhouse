using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class AlterDatabaseFolderNameTestCase: TestCaseBase
    {
        public const string expected = "al-ter-da-ta-base";
        public AlterDatabaseFolderNameTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => 
            List("ad", "alterdatabase", "alterdatabasefolder", "alterdatabasefoldername");
    }
}