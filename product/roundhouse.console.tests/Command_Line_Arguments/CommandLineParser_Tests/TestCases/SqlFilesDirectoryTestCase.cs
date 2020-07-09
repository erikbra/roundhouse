using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class SqlFilesDirectoryTestCase: TestCaseBase<string>
    {
        public const string expected = "sssssssssss";
        public SqlFilesDirectoryTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => List("f", "files","sqlfilesdirectory");
    }
}