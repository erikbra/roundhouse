using System.Collections.Generic;
using System.Text;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class DefaultEncodingTestCase: TestCaseBase
    {
        private const string encoding = "iso8859-1";
        public static readonly Encoding expected = Encoding.GetEncoding(encoding);
        public DefaultEncodingTestCase() : base(encoding, true) { }
        protected override IEnumerable<string> variants() => List("defaultencoding");
    }
}