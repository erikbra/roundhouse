using System;
using System.IO;
using System.Linq;

namespace roundhouse.tests.integration
{
    public static class TestEnvironment
    {
        private static string GetSolutionRoot()
        {
            var thisDir = new DirectoryInfo(AppDomain.CurrentDomain.SetupInformation.ApplicationBase);

           DirectoryInfo dir = thisDir;

           do
           {
               dir = dir.Parent;
           } while (dir.Name != "product");

           return dir.Parent.FullName;
        }

        public static string solution_root { get; } = GetSolutionRoot();

        public static string test_script_root => Path.Combine(solution_root, "db");

        public static string test_script_dir(string db_type, string db_name) =>
            Path.Combine(test_script_root, db_type, db_name);

    }
}