using roundhouse.infrastructure;

namespace roundhouse.databases.sqlserver.db_definitions
{
    public class ScriptsRunErrorsDefinition: SqlServerTableDefinition, roundhouse.db_definitions.ScriptsRunErrorsDefinition
    {
        public string CreateText => $@"
{create_statement()}
(
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[repository_path] [nvarchar](255) NULL,
	[version] [nvarchar](50) NULL,
	[script_name] [nvarchar](255) NULL,
	[text_of_script] [ntext] NULL,
	[erroneous_part_of_script] [ntext] NULL,
	[error_message] [ntext] NULL,
	[entry_date] [datetime] NULL,
	[modified_date] [datetime] NULL,
	[entered_by] [nvarchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

{primary_key_index_text()}
";
        protected override string table_name() => ApplicationParameters.CurrentMappings.scripts_run_errors_table_name;
    }
}