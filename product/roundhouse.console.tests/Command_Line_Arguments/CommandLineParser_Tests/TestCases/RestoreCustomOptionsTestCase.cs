using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class RestoreCustomOptionsTestCase: TestCaseBase
    {
        public const string expected = "MOVE='somewhere to another'";
        public RestoreCustomOptionsTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => List("rco", "restoreoptions", "restorecustomoptions");
    }
}