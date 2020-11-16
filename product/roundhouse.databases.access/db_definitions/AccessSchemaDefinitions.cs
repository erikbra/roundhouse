using roundhouse.db_definitions;
using roundhouse.infrastructure.app;

namespace roundhouse.databases.access.db_definitions
{
    public class AccessSchemaDefinitions : ISchemaDefinitions
    {
        private readonly Database database;

        public AccessSchemaDefinitions(Database database)
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