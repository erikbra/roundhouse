using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class ViewsFolderNameTestCase: TestCaseBase<string>
    {
        public const string expected = "fuuuun-ctions";
        public ViewsFolderNameTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => 
            List("vw", "views", "viewsfolder", "viewsfoldername");
    }
}