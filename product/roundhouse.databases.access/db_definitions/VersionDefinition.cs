using roundhouse.infrastructure;

namespace roundhouse.databases.access.db_definitions
{
    public class VersionDefinition: AccessTableDefinition, roundhouse.db_definitions.VersionDefinition
    {
        public string CreateText => $@"
{create_statement()}
(
	[id] COUNTER,
	[repository_path] CHAR,
	[version] CHAR,
	[entry_date] DATETIME,
	[modified_date] DATETIME,
	[entered_by] CHAR,
    {primary_key_index_text()}
)";
        protected override string table_name() => ApplicationParameters.CurrentMappings.version_table_name;
    }
}