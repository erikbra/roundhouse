using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class EnvironmentNamesTestCaseSemiColon: TestCaseBase
    {
        private const string sep = ";";
        
        public static readonly IEnumerable<string> expected = List("foo", "bar", "test", "production");
        public EnvironmentNamesTestCaseSemiColon() : base(Join(expected), true) { }
        protected override IEnumerable<string> variants() => 
            List("env", "environment", "environmentname", "envs", "environments", "environmentnames");

        private static string Join(IEnumerable<string> values) => string.Join(sep, values);
    }
}