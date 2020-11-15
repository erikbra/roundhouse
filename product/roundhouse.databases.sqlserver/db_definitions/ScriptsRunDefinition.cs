using roundhouse.infrastructure;

namespace roundhouse.databases.sqlserver.db_definitions
{
    public class ScriptsRunDefinition: SqlServerTableDefinition, roundhouse.db_definitions.ScriptsRunDefinition
    {
        public string CreateText => $@"
{create_statement()}
(
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[version_id] [bigint] NULL,
	[script_name] [nvarchar](255) NULL,
	[text_of_script] [text] NULL,
	[text_hash] [nvarchar](512) NULL,
	[one_time_script] [bit] NULL,
	[entry_date] [datetime] NULL,
	[modified_date] [datetime] NULL,
	[entered_by] [nvarchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

{primary_key_index_text()}
";
        protected override string table_name() => ApplicationParameters.CurrentMappings.scripts_run_table_name;
        
    }
}