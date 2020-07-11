using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class FunctionsFolderNameTestCase: TestCaseBase
    {
        public const string expected = "fuuuun-ctions";
        public FunctionsFolderNameTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => 
            List("fu", "functions", "functionsfolder", "functionsfoldername");
    }
}