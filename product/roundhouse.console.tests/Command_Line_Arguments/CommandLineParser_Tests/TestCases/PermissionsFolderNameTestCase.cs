using System.Collections.Generic;
using static roundhouse.console.tests.ListHelpers;

namespace roundhouse.console.tests.Command_Line_Arguments
{
    public class PermissionsFolderNameTestCase: TestCaseBase<string>
    {
        public const string expected = "may-i-have-permission-please";
        public PermissionsFolderNameTestCase() : base(expected, true) { }
        protected override IEnumerable<string> variants() => 
            List("p", "permissions", "permissionsfolder", "permissionsfoldername");
    }
}