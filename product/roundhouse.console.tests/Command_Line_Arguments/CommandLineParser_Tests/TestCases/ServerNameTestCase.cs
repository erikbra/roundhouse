using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class ServerNameTestCase: TestCaseBase
    {
        public const string expected = "sf0uasfas";
        public ServerNameTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => List("s", "server", "servername", "instance", "instancename");
    }
}