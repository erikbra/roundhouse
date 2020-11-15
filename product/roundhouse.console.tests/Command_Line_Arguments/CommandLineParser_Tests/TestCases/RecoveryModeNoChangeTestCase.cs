using System.Collections.Generic;
using roundhouse.databases;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class RecoveryModeNoChangeTestCase: TestCaseBase
    {
        public const RecoveryMode expected = RecoveryMode.NoChange;
        public RecoveryModeNoChangeTestCase() : base(expected.ToString(), true) { }
        protected override IEnumerable<string> variants() => List("rcm", "recoverymode");
    }
}