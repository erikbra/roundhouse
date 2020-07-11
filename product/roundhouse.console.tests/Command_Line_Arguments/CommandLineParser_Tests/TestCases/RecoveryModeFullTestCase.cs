using System.Collections.Generic;
using roundhouse.databases;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class RecoveryModeFullTestCase: TestCaseBase
    {
        public const RecoveryMode expected = RecoveryMode.Full;
        public RecoveryModeFullTestCase() : base(expected.ToString(), true) { }
        protected override IEnumerable<string> variants() => List("rcm", "recoverymode");
    }
}