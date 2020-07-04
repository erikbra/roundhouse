using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class UpFolderNameTestCase: TestCaseBase
    {
        public const string expected = "up-fol-der";
        public UpFolderNameTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => 
            List("u", "up", "upfolder", "upfoldername");
    }
}