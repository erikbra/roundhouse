using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class SprocsFolderNameTestCase: TestCaseBase<string>
    {
        public const string expected = "s-p-r-o-c-s";
        public SprocsFolderNameTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => 
            List("sp", "sprocs", "sprocsfolder", "sprocsfoldername");
    }
}