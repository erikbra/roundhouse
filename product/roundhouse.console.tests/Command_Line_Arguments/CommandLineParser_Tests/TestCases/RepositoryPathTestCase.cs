using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class RepositoryPathTestCase: TestCaseBase
    {
        public const string expected = "rep-os-i-tory";
        public RepositoryPathTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => List("r", "repo", "repositorypath");
    }
}