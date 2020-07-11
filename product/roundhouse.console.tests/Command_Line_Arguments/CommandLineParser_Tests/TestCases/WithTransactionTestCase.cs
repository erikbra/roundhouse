using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class WithTransactionTestCase: FlagTestCaseBase
    {
        public const bool expected = true;
        protected override IEnumerable<string> variants() => List("t", "trx", "transaction", "wt", "withtransaction");
    }
}