using System;
using System.Linq;
using roundhouse.infrastructure.extensions;

namespace roundhouse.console
{
    public static class OperationParser
    {
        public static MainOperation get_main_operation(string[] args) =>
            args switch
            {
                { } when is_report_version(args) => MainOperation.ReportVersion,
                { } when is_redgate_diff(args) => MainOperation.RunDiffUtility,
                { } when is_isuptodate(args) => MainOperation.RunIsUpToDate,
                { } when is_init(args) => MainOperation.RunInit,
                _ => MainOperation.RunMigrator,
            };

        private static bool is_report_version(string[] args) => 
            args.Length == 1 && args[0].TrimStart('-').Equals("version", StringComparison.InvariantCultureIgnoreCase);

        private static bool is_isuptodate(string[] args) => string.Join("|", args).to_lower().Contains("isuptodate");
        private static bool is_redgate_diff(string[] args) => string.Join("|", args).to_lower().Contains("rh.redgate.diff");
        public static bool is_init(string[] args) => args.Any() && args[0] == "init";

    }
}