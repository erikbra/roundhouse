using roundhouse.infrastructure;

namespace roundhouse.databases.access.db_definitions
{
    public abstract class AccessTableDefinition 
    {
        protected string create_statement() => $@"
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '{schema_name()}' AND TABLE_NAME = '{table_name()}')
CREATE TABLE {schema_and_table_name()}";
        
        protected string schema_and_table_name() => $"{schema_name()}.{table_name()}";

        protected string schema_name() => ApplicationParameters.CurrentMappings.roundhouse_schema_name;
        protected abstract string table_name();

        protected string primary_key_index_text() => $@"CONSTRAINT [{table_name()}_PrimaryKey] PRIMARY KEY ([id])";
        
    }
}