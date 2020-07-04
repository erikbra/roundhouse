using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class DownFolderNameTestCase: TestCaseBase
    {
        public const string expected = "up-fol-der";
        public DownFolderNameTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => 
            List("do", "down", "downfolder", "downfoldername");
    }
}