using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class AccessTokenTestCase: TestCaseBase
    {
        public const string expected = "azzezztååken";
        public AccessTokenTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => List("accesstoken");
    }
}