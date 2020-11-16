using roundhouse.infrastructure;

namespace roundhouse.databases.sqlserver2000.db_definitions
{
    public abstract class SqlServerTableDefinition 
    {
        protected string create_statement() => $@"
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '{schema_name()}' AND TABLE_NAME = '{table_name()}')
CREATE TABLE {schema_and_table_name()}";
        
        protected string schema_and_table_name() => $"{schema_name()}.{table_name()}";

        protected string schema_name() => ApplicationParameters.CurrentMappings.roundhouse_schema_name;
        protected abstract string table_name();

        protected string primary_key_index_text() => $@"
ALTER TABLE {schema_and_table_name()} ADD PRIMARY KEY CLUSTERED 
(
	[id] ASC
) WITH (
    PAD_INDEX = OFF, 
    STATISTICS_NORECOMPUTE = OFF, 
    SORT_IN_TEMPDB = OFF, 
    IGNORE_DUP_KEY = OFF, 
    ONLINE = OFF, 
    ALLOW_ROW_LOCKS = ON, 
    ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
";
        
    }
}