using roundhouse.infrastructure;

namespace roundhouse.databases.mysql.db_definitions
{
    public abstract class MySqlTableDefinition 
    {
        protected string create_statement() => $@"
CREATE TABLE IF NOT EXISTS {schema_and_table_name()}";
        
        protected string schema_and_table_name() => $"{schema_name()}_{table_name()}";

        protected string schema_name() => ApplicationParameters.CurrentMappings.roundhouse_schema_name;
        protected abstract string table_name();

        protected string primary_key_index_text() => @"primary key(id)";
    }
}