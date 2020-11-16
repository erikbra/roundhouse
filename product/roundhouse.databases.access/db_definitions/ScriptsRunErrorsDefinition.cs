using roundhouse.infrastructure;

namespace roundhouse.databases.access.db_definitions
{
    public class ScriptsRunErrorsDefinition: AccessTableDefinition, roundhouse.db_definitions.ScriptsRunErrorsDefinition
    {
        public string CreateText => $@"
{create_statement()}
(
	[id] COUNTER,
	[repository_path] CHAR,
	[version] CHAR,
	[script_name] CHAR,
	[text_of_script] MEMO,
	[erroneous_part_of_script] MEMO,
	[error_message] MEMO,
	[entry_date] DATETIME,
	[modified_date] DATETIME,
	[entered_by] CHAR,
    {primary_key_index_text()}
)";
        protected override string table_name() => ApplicationParameters.CurrentMappings.scripts_run_errors_table_name;
    }
}