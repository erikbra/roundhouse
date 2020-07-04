using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class DatabaseNameTestCase: TestCaseBase
    {
        public const string expected = "fslnaslnadsf";
        public DatabaseNameTestCase() : base(expected, false) { }
        protected override IEnumerable<string> variants() => List("d", "db", "database", "databasename");
    }
}