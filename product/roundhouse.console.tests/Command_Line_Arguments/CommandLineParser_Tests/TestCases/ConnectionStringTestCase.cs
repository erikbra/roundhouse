using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class ConnectionStringTestCase: TestCaseBase
    {
        public const string expected = "zooooo";
        public ConnectionStringTestCase() : base(expected, false) { }
        protected override IEnumerable<string> variants() => List("c", "cs", "connstring", "connectionstring");
    }
}