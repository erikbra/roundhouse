using roundhouse.infrastructure;

namespace roundhouse.databases.mysql.db_definitions
{
    public class VersionDefinition: MySqlTableDefinition, roundhouse.db_definitions.VersionDefinition
    {
        public string CreateText => $@"
{create_statement()}
(
	[id] [bigint] NOT NULL AUTO_INCREMENT,
	[repository_path] [nvarchar](255) NULL,
	[version] [nvarchar](50) NULL,
	[entry_date] [datetime] NULL,
	[modified_date] [datetime] NULL,
	[entered_by] [nvarchar](50) NULL,
    {primary_key_index_text()}
);
";
        protected override string table_name() => ApplicationParameters.CurrentMappings.version_table_name;
    }
}