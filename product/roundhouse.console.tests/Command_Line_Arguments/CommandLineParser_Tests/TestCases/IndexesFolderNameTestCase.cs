using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class IndexesFolderNameTestCase: TestCaseBase<string>
    {
        public const string expected = "pull-the-trigger";
        public IndexesFolderNameTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => 
            List("ix", "indexes", "indexesfolder", "indexesfoldername");
    }
}