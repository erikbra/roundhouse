using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class TriggersFolderNameTestCase: TestCaseBase<string>
    {
        public const string expected = "pull-the-trigger";
        public TriggersFolderNameTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => 
            List("trg", "triggers", "triggersfolder", "triggersfoldername");
    }
}