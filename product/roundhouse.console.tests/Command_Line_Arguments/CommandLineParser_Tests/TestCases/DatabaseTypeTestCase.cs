using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class DatabaseTypeTestCase: TestCaseBase<string>
    {
        public const string expected = "supadupabase";
        public DatabaseTypeTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => List("dt", "dbt", "databasetype");
    }
}