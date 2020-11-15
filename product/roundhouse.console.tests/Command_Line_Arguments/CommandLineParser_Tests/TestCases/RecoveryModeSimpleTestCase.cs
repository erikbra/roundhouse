using System.Collections.Generic;
using roundhouse.databases;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class RecoveryModeSimpleTestCase: TestCaseBase
    {
        public const RecoveryMode expected = RecoveryMode.Simple;
        public RecoveryModeSimpleTestCase() : base(expected.ToString().ToLower(), true) { }
        protected override IEnumerable<string> variants() => List("rcm", "recoverymode");
    }
}