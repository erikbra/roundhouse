using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class OutputPathTestCase: TestCaseBase
    {
        public const string expected = "/some/folder";
        public OutputPathTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => List("o", "output", "outputpath");
    }
}