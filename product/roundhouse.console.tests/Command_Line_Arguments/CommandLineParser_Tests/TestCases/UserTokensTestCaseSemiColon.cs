using System.Collections.Generic;
using System.Linq;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class UserTokensTestCaseSemiColon: TestCaseBase
    {
        private const string sep = ";";
        
        public static readonly IDictionary<string, string> expected = 
            new Dictionary<string, string>()
            {
                {"token1", "asfas"},
                {"token2", "11111111"},
                {"token5", "---zzdadsfasdggalkjawerløjaewlj"},
                {"token6", "'i am a pig that can fly'"},
            };
        
        public UserTokensTestCaseSemiColon() : base(Join(expected), true) { }
        protected override IEnumerable<string> variants() => List("ut", "usertokens");

        private static string Join(IDictionary<string, string> items) => 
            string.Join(sep, items.Select(item => $"{item.Key}={item.Value}"));
    }
}