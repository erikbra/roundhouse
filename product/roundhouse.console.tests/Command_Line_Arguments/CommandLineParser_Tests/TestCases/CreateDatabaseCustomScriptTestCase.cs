using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class CreateDatabaseCustomScriptTestCase: TestCaseBase
    {
        public const string expected = "'a very custom script located in a secret place'";
        public CreateDatabaseCustomScriptTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => List("cds", "createdatabasescript", "createdatabasecustomscript");
    }
}