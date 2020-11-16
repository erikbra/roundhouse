using roundhouse.infrastructure;

namespace roundhouse.databases.access.db_definitions
{
    public class ScriptsRunDefinition: AccessTableDefinition, roundhouse.db_definitions.ScriptsRunDefinition
    {
        public string CreateText => $@"
{create_statement()}
(
	[id] COUNTER,
	[version_id] INTEGER,
	[script_name] CHAR,
	[text_of_script] MEMO,
	[text_hash] MEMO,
	[one_time_script] BIT,
	[entry_date] DATETIME,
	[modified_date] DATETIME,
	[entered_by] CHAR,
    {primary_key_index_text()}
)";
        protected override string table_name() => ApplicationParameters.CurrentMappings.scripts_run_table_name;
        
    }
}