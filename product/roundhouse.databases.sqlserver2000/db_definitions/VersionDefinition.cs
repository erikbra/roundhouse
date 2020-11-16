using roundhouse.infrastructure;

namespace roundhouse.databases.sqlserver2000.db_definitions
{
    public class VersionDefinition: SqlServerTableDefinition, roundhouse.db_definitions.VersionDefinition
    {
        public string CreateText => $@"
{create_statement()}
(
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[repository_path] [nvarchar](255) NULL,
	[version] [nvarchar](50) NULL,
	[entry_date] [datetime] NULL,
	[modified_date] [datetime] NULL,
	[entered_by] [nvarchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

{primary_key_index_text()}
";
        protected override string table_name() => ApplicationParameters.CurrentMappings.version_table_name;
    }
}