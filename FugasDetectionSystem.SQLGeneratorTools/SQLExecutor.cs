using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.AccessControl;

namespace FugasDetectionSystem.SQLGeneratorTools
{
    public class SQLExecutor
    {
        public string connectionString = "";
        public SQLExecutor()
        {
            // Recuperar variables de entorno
            string dbServer = "dbokfugasdesa.database.windows.net,1433";
            string dbName = "db-okfugas-desa";
            string dbUser = "adminfugas";
            string dbPassword = "Marlen.4014##";

            // Construir la cadena de conexión
            connectionString = $"Server={dbServer};Database={dbName};User Id={dbUser};Password={dbPassword};";
        }

        public async Task<bool> ExecuteScriptAsync(string directoryPath)
        {
            foreach (string file in Directory.GetFiles(directoryPath, "*.sql"))
            {
                Console.WriteLine($"Ejecutando: {file}");
                try
                {
                    string script = await File.ReadAllTextAsync(file);
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        await conn.OpenAsync();
                        using (SqlCommand command = new SqlCommand(script, conn))
                        {
                            await command.ExecuteNonQueryAsync();
                        }
                    }
                    Console.WriteLine("Ejecutado exitosamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al ejecutar el script: {ex.Message}");
                    return false;
                }
            }

            Console.WriteLine("Proceso completado.");
            return true;
        }


        public async Task<DataTable> GetTablesAsync()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                return await Task.Run(() => connection.GetSchema("Tables"));
            }
        }


        public async Task<DataTable> GetStoredProceduresAsync(string tableName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT * FROM sys.procedures WHERE name LIKE @TableName", connection);
                command.Parameters.AddWithValue("@TableName", "%" + tableName + "%");

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable procedures = new DataTable();
                    await Task.Run(() => adapter.Fill(procedures));
                    return procedures;
                }
            }
        }

        public async Task<List<ColumnDetails>> GetTableColumnsAsync(string tableName)
        {
            var columnDetailsList = new List<ColumnDetails>();
            var primaryKeys = await GetPrimaryKeyColumnsAsync(tableName); // Obtener las claves primarias

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var query = @"
                    SELECT 
                        COLUMN_NAME, 
                        DATA_TYPE, 
                        IS_NULLABLE 
                    FROM INFORMATION_SCHEMA.COLUMNS 
                    WHERE TABLE_NAME = @TableName
                    ORDER BY ORDINAL_POSITION";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TableName", tableName);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var details = new ColumnDetails
                            {
                                ColumnName = reader.GetString(reader.GetOrdinal("COLUMN_NAME")),
                                DataType = reader.GetString(reader.GetOrdinal("DATA_TYPE")),
                                IsNullable = reader.GetString(reader.GetOrdinal("IS_NULLABLE")) == "YES",
                                IsPrimaryKey = primaryKeys.Contains(reader.GetString(reader.GetOrdinal("COLUMN_NAME")))
                            };
                            columnDetailsList.Add(details);
                        }
                    }
                }
            }

            return columnDetailsList;
        }


        public async Task<List<string>> GetPrimaryKeyColumnsAsync(string tableName)
        {
            List<string> primaryKeyColumns = new List<string>();

            string query = @"
                    SELECT 
                        KU.column_name as PRIMARYKEYCOLUMN
                    FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC
                    INNER JOIN
                        INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KU
                          ON TC.CONSTRAINT_TYPE = 'PRIMARY KEY' AND
                             TC.CONSTRAINT_NAME = KU.CONSTRAINT_NAME
                    WHERE KU.table_name = @TableName
                    ORDER BY KU.ORDINAL_POSITION;";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TableName", tableName);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            primaryKeyColumns.Add(reader.GetString(0));
                        }
                    }
                }
            }

            return primaryKeyColumns;
        }



    }

}
/*
 *         public bool ExecuteScript(string directoryPath)
        {
            foreach (string file in Directory.GetFiles(directoryPath, "*.sql"))
            {
                Console.WriteLine($"Ejecutando: {file}");
                try
                {
                    string script = File.ReadAllText(file);
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlCommand command = new SqlCommand(script, conn))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                    Console.WriteLine("Ejecutado exitosamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al ejecutar el script: {ex.Message}");
                    return false;
                }
            }

            Console.WriteLine("Proceso completado.");
            return true;
        }

        public DataTable GetTables()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataTable tables = connection.GetSchema("Tables");
                return tables;
            }
        }

        public DataTable GetStoredProcedures(string tableName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM sys.procedures WHERE name LIKE @TableName", connection);
                command.Parameters.AddWithValue("@TableName", "%" + tableName + "%");
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable procedures = new DataTable();
                adapter.Fill(procedures);
                return procedures;
            }
        }
*/