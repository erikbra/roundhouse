using roundhouse.db_definitions;
using roundhouse.infrastructure.app;

namespace roundhouse.databases.sqlserver.db_definitions
{
    public class SqlServerSchemaDefinitions : ISchemaDefinitions
    {
        private readonly Database database;

        public SqlServerSchemaDefinitions(Database database)
        {
            this.database = database;
        }

        public void CreateRoundhouseSchemaTables()
        {
            database.run_sql(new VersionDefinition().CreateText, ConnectionType.Admin);
            database.run_sql(new ScriptsRunDefinition().CreateText, ConnectionType.Admin);
            database.run_sql(new ScriptsRunErrorsDefinition().CreateText, ConnectionType.Admin);
        }
    }
}