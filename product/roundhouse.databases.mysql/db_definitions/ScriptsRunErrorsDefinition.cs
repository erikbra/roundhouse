using roundhouse.infrastructure;

namespace roundhouse.databases.mysql.db_definitions
{
    public class ScriptsRunErrorsDefinition: MySqlTableDefinition, roundhouse.db_definitions.ScriptsRunErrorsDefinition
    {
        public string CreateText => $@"
{create_statement()}
(
	[id] [bigint] NOT NULL AUTO_INCREMENT,
	[repository_path] [nvarchar](255) NULL,
	[version] [nvarchar](50) NULL,
	[script_name] [nvarchar](255) NULL,
	[text_of_script] [ntext] NULL,
	[erroneous_part_of_script] [ntext] NULL,
	[error_message] [ntext] NULL,
	[entry_date] [datetime] NULL,
	[modified_date] [datetime] NULL,
	[entered_by] [nvarchar](50) NULL,
    {primary_key_index_text()}
);
";
        protected override string table_name() => ApplicationParameters.CurrentMappings.scripts_run_errors_table_name;
    }
}