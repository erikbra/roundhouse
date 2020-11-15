// using System.Collections.Generic;
// using static roundhouse.console.tests.ListHelpers;
//
// namespace roundhouse.console.tests.Command_Line_Arguments
// {
//     public class UserTokensTestCaseComma: TestCaseBase
//     {
//         private const string sep = ",";
//         
//         public static readonly IEnumerable<string> expected = List("token1=asfas", "token2=asfnas", "key=aøsdfaoih", "kney=fsa097234hsag");
//         public UserTokensTestCaseComma() : base(Join(expected), true) { }
//         protected override IEnumerable<string> variants() => List("ut", "usertokens");
//
//         private static string Join(IEnumerable<string> values) => string.Join(sep, values);
//     }
// }