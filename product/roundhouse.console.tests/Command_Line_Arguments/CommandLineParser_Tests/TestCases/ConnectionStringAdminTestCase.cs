using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class ConnectionStringAdminTestCase: TestCaseBase<string>
    {
        public const string expected = "sofiasf0å8";
        public ConnectionStringAdminTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => List("csa", "connstringadmin", "connectionstringadministration");
    }
}