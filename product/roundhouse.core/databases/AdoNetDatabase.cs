using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Dapper;
using roundhouse.model;
using Version = System.Version;

namespace roundhouse.databases
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using connections;
    using infrastructure.app;
    using infrastructure.logging;
    using parameters;

    public abstract class AdoNetDatabase : DefaultDatabase<IDbConnection>
    {
        private bool split_batches_in_ado = true;

        public override bool split_batch_statements
        {
            get { return split_batches_in_ado; }
            set { split_batches_in_ado = value; }
        }

        protected IDbTransaction transaction;

        protected virtual AdoNetConnection GetAdoNetConnection(string conn_string)
        {
            var provider_factory = get_db_provider_factory();
            IDbConnection connection = provider_factory.CreateConnection();
            connection_specific_setup(connection);

            connection.ConnectionString = conn_string;
            return new AdoNetConnection(connection);
        }

        protected abstract DbProviderFactory get_db_provider_factory();

        protected virtual void connection_specific_setup(IDbConnection connection)
        {
        }


        public override void open_admin_connection()
        {
            Log.bound_to(this)
                .log_a_debug_event_containing("Opening admin connection to '{0}'", admin_connection_string);
            admin_connection = GetAdoNetConnection(admin_connection_string);
            admin_connection.open();
        }

        public override void close_admin_connection()
        {
            Log.bound_to(this).log_a_debug_event_containing("Closing admin connection");
            if (admin_connection != null)
            {
                admin_connection.clear_pool();
                admin_connection.close();
                admin_connection.Dispose();
                admin_connection = null;
            }
        }

        public override void open_connection(bool with_transaction)
        {
            Log.bound_to(this).log_a_debug_event_containing("Opening connection to '{0}'", connection_string);
            server_connection = GetAdoNetConnection(connection_string);
            server_connection.open();

            set_repository();

            if (with_transaction)
            {
                transaction = server_connection.underlying_type().BeginTransaction();
            }
        }

        public override void close_connection()
        {
            Log.bound_to(this).log_a_debug_event_containing("Closing connection");
            if (transaction != null)
            {
                transaction.Commit();
                transaction = null;
            }

            if (server_connection != null)
            {
                server_connection.clear_pool();
                server_connection.close();
                server_connection.Dispose();
                server_connection = null;
            }
        }

        public override void rollback()
        {
            Log.bound_to(this).log_a_debug_event_containing("Rolling back changes");

            if (transaction != null)
            {
                //rollback previous transaction
                transaction.Rollback();
                server_connection.close();

                //open a new transaction
                server_connection.open();
                //use_database(database_name);
                transaction = server_connection.underlying_type().BeginTransaction();
            }
        }

        protected override void run_sql(string sql_to_run, ConnectionType connection_type,
            IList<IParameter> parameters)
        {
            if (string.IsNullOrEmpty(sql_to_run))
            {
                return;
            }

            if (transaction == null)
            {
                retry_policy.Execute(() => run_command_with(sql_to_run, connection_type, parameters));
            }
            else
            {
                run_command_with(sql_to_run, connection_type, parameters);
            }
        }
        
        protected override void run_sql(string sql_to_run, ConnectionType connection_type,
            object parameters)
        {
            if (string.IsNullOrEmpty(sql_to_run))
            {
                return;
            }
            
            var connection = get_db_connection(connection_type);
            retry_policy.Execute(() => connection.underlying_type().Execute(sql_to_run, parameters, transaction));
        }
        
        protected override IEnumerable<T> run_sql_query<T>(string sql_to_run, ConnectionType connection_type,
            object parameters)
        {
            if (string.IsNullOrEmpty(sql_to_run))
            {
                return Enumerable.Empty<T>();
            }
            
            var connection = get_db_connection(connection_type);
            return retry_policy.Execute(() => connection.underlying_type().Query<T>(sql_to_run, parameters, transaction));
        }

        private void run_command_with(string sql_to_run, ConnectionType connection_type,
            IList<IParameter> parameters)
        {
            using (IDbCommand command = setup_database_command(sql_to_run, connection_type, parameters))
            {
                command.ExecuteNonQuery();
            }
        }

        protected override object run_sql_scalar(string sql_to_run, ConnectionType connection_type,
            IList<IParameter> parameters)
        {
            object return_value = new object();
            if (string.IsNullOrEmpty(sql_to_run))
            {
                return return_value;
            }

            using (IDbCommand command = setup_database_command(sql_to_run, connection_type, null))
            {
                if (transaction == null)
                {
                    return_value = retry_policy.Execute(() => command.ExecuteScalar());
                }
                else
                {
                    return_value = command.ExecuteScalar();
                }
            }

            return return_value;
        }

        protected override string get_insert_sql<T>(T item) 
        {
            var tableName = get_table_name<T>(item);
            var columns = get_columns_with_values(item);

            var sql = new StringBuilder($@"
INSERT INTO {roundhouse_schema_name}.{tableName}
(
");
            foreach (var column in columns)
            {
                sql.Append(column.Key);
                sql.AppendLine(",");
            }

            sql.AppendLine(")");
            sql.AppendLine("VALUES(");
            
            foreach (var column in columns)
            {
                sql.Append("@");
                sql.Append(column.Key);
                sql.AppendLine(",");
            }
            sql.AppendLine(";");

            return sql.ToString();
        }

        private IDictionary<string, object> get_columns_with_values<T>(T item) =>
            typeof(T)
                .GetProperties()
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(item));

        private string get_table_name<T>(T item) =>
            item switch
            {
                ScriptsRun _ => scripts_run_table_name,
                ScriptsRunError _ => scripts_run_errors_table_name,
                Version _ => version_table_name,
                _ => throw new ArgumentException("Unknown type: " + typeof(T), nameof(item))
            };
       

        protected IConnection<IDbConnection> get_db_connection(ConnectionType connection_type) =>
            connection_type switch
            {
                ConnectionType.Default => get_open_server_connection(),
                ConnectionType.Admin => get_open_admin_connection(),
                _ => throw new ArgumentOutOfRangeException(nameof(connection_type), connection_type, "Invalid connection type: " + connection_type)
                
            };

        private IConnection<IDbConnection> get_open_server_connection()
        {
            if (server_connection == null ||
                server_connection.underlying_type().State != ConnectionState.Open)
            {
                open_connection(false);
            }

            return server_connection;
        }
        
        private IConnection<IDbConnection> get_open_admin_connection()
        {
            if (admin_connection == null ||
                admin_connection.underlying_type().State != ConnectionState.Open)
            {
                open_admin_connection();
            }

            return admin_connection;
        }
        

        protected IDbCommand setup_database_command(string sql_to_run, ConnectionType connection_type,
            IEnumerable<IParameter> parameters)
        {
            IDbCommand command = null;


            switch (connection_type)
            {
                case ConnectionType.Default:
                    if (server_connection == null ||
                        server_connection.underlying_type().State != ConnectionState.Open)
                    {
                        open_connection(false);
                    }

                    Log.bound_to(this).log_a_debug_event_containing("Setting up command for normal connection");
                    command = server_connection.underlying_type().CreateCommand();
                    command.CommandTimeout = command_timeout;
                    break;
                case ConnectionType.Admin:
                    if (admin_connection == null ||
                        admin_connection.underlying_type().State != ConnectionState.Open)
                    {
                        open_admin_connection();
                    }

                    Log.bound_to(this).log_a_debug_event_containing("Setting up command for admin connection");
                    command = admin_connection.underlying_type().CreateCommand();
                    command.CommandTimeout = admin_command_timeout;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(connection_type), connection_type, "Invalid connection type: " + connection_type);
            }

            if (parameters != null)
            {
                foreach (IParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter.underlying_type);
                }
            }

            if (connection_type != ConnectionType.Admin)
            {
                command.Transaction = transaction;
            }

            command.CommandText = sql_to_run;
            command.CommandType = CommandType.Text;


            return command;
        }
    }
}