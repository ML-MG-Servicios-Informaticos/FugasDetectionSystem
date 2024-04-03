using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.AccessControl;
using System.Text;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace FugasDetectionSystem.SQLGeneratorTools
{
    public class SQLExecutor
    {
        private readonly string connectionString = string.Empty;

        private readonly Dictionary<string, CRUDOperationResult> operationResults;

        public SQLExecutor()
        {
            // Recuperar variables de entorno
            string dbServer = "dbokfugasdesa.database.windows.net,1433";
            string dbName = "db-okfugas-desa";
            string dbUser = "adminfugas";
            string dbPassword = "Marlen.4014##";

            // Construir la cadena de conexión
            connectionString = $"Server={dbServer};Database={dbName};User Id={dbUser};Password={dbPassword};";

            operationResults = new Dictionary<string, CRUDOperationResult>();
        }

        public async Task<bool> ExecuteScriptAsync(string script)
        {
            Console.WriteLine("Ejecutando script SQL...");
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand command = new SqlCommand(script, conn))
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                }
                Console.WriteLine("Script ejecutado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al ejecutar el script: {ex.Message}");
                return false;
            }

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

        public async Task<string> GenerateInsertProcedureAsync(string tableName)
        {
            var columns = await GetTableColumnsAsync(tableName);
            var primaryKeys = await GetPrimaryKeyColumnsAsync(tableName);

            var insertParameters = new StringBuilder();
            var columnNames = new StringBuilder();
            var columnValues = new StringBuilder();

            foreach (var column in columns.Where(c => !primaryKeys.Contains(c.ColumnName)))
            {
                insertParameters.AppendLine($"    @{column.ColumnName} {column.DataType},");
                columnNames.AppendLine($"    {column.ColumnName},");
                columnValues.AppendLine($"    @{column.ColumnName},");
            }

            // Remove trailing commas
            insertParameters = insertParameters.Remove(insertParameters.Length - 3, 1);
            columnNames = columnNames.Remove(columnNames.Length - 3, 1);
            columnValues = columnValues.Remove(columnValues.Length - 3, 1);

            string template = await File.ReadAllTextAsync("Plantillas\\PathToInsertTemplate.sql");
            string procedure = template
                .Replace("{TableName}", tableName)
                .Replace("{InsertParameters}", insertParameters.ToString())
                .Replace("{ColumnNames}", columnNames.ToString())
                .Replace("{ColumnValues}", columnValues.ToString());

            return procedure;
        }

        public async Task<string> GenerateUpdateProcedureAsync(string tableName)
        {
            var columns = await GetTableColumnsAsync(tableName);
            var primaryKeys = await GetPrimaryKeyColumnsAsync(tableName);

            var updateParameters = new StringBuilder();
            var updateSet = new StringBuilder();

            foreach (var column in columns)
            {
                updateParameters.AppendLine($"    @{column.ColumnName} {column.DataType},");
                if (!primaryKeys.Contains(column.ColumnName))
                {
                    updateSet.AppendLine($"    {column.ColumnName} = @{column.ColumnName},");
                }
            }

            // Remove trailing commas
            updateParameters = updateParameters.Remove(updateParameters.Length - 3, 1);
            updateSet = updateSet.Remove(updateSet.Length - 3, 1);

            // Assuming the primary key is a single column for simplicity; adjust if using composite keys
            string primaryKeyWhereClause = $"WHERE {primaryKeys.First()} = @{primaryKeys.First()}";

            string template = await File.ReadAllTextAsync("Plantillas\\PathToUpdateTemplate.sql");
            string procedure = template
                .Replace("{TableName}", tableName)
                .Replace("{UpdateParameters}", updateParameters.ToString())
                .Replace("{UpdateSet}", updateSet.ToString())
                .Replace("{PrimaryKeyWhereClause}", primaryKeyWhereClause);

            return procedure;
        }

        public async Task<string> GenerateDeleteProcedureAsync(string tableName)
        {
            var primaryKeys = await GetPrimaryKeyColumnsAsync(tableName);

            // Preparar los parámetros para la cláusula WHERE basada en las claves primarias
            var deleteParameters = new StringBuilder();
            var primaryKeyWhereClause = new StringBuilder("WHERE ");

            foreach (var pkColumn in primaryKeys)
            {
                // Asumiendo que las columnas tienen un tipo genérico; podrías necesitar ajustar esto
                deleteParameters.AppendLine($"    @{pkColumn} INT,"); // Ajusta el tipo de datos según sea necesario
                primaryKeyWhereClause.Append($"{pkColumn} = @{pkColumn} AND ");
            }

            // Remove trailing "AND "
            if (primaryKeyWhereClause.Length > 0)
            {
                primaryKeyWhereClause.Length -= 5;
            }

            string template = await File.ReadAllTextAsync("Plantillas\\PathToDeleteTemplate.sql");
            string procedure = template
                .Replace("{TableName}", tableName)
                .Replace("{DeleteParameters}", deleteParameters.ToString())
                .Replace("{PrimaryKeyWhereClause}", primaryKeyWhereClause.ToString());

            return procedure;
        }

        public async Task<string> GenerateSelectAllProcedureAsync(string tableName)
        {
            var columns = await GetTableColumnsAsync(tableName);
            var columnNames = new StringBuilder();

            foreach (var column in columns)
            {
                columnNames.Append($"[{column.ColumnName}], ");
            }

            // Remove trailing comma and space
            if (columnNames.Length > 0)
            {
                columnNames.Length -= 2;
            }

            string template = await File.ReadAllTextAsync("Plantillas\\PathToSelectAllTemplate.sql");
            string procedure = template
                .Replace("{TableName}", tableName)
                .Replace("{ColumnNames}", columnNames.ToString());

            return procedure;
        }

        public async Task<string> GenerateSelectByIdProcedureAsync(string tableName)
        {
            var columns = await GetTableColumnsAsync(tableName);
            var primaryKeys = await GetPrimaryKeyColumnsAsync(tableName);

            if (primaryKeys.Count != 1)
            {
                throw new Exception("La tabla debe tener exactamente una clave primaria para generar un Select By ID.");
            }

            var columnNames = new StringBuilder();
            foreach (var column in columns)
            {
                columnNames.Append($"[{column.ColumnName}], ");
            }

            // Remove trailing comma and space
            if (columnNames.Length > 0)
            {
                columnNames.Length -= 2;
            }

            string primaryKey = primaryKeys[0];
            var primaryKeyParameter = columns.FirstOrDefault(c => c.ColumnName == primaryKey);

            if (primaryKeyParameter == null)
            {
                throw new Exception("No se encontró la clave primaria en la lista de columnas.");
            }

            string template = await File.ReadAllTextAsync("Plantillas\\PathToSelectByIdTemplate.sql");
            string procedure = template
                .Replace("{TableName}", tableName)
                .Replace("{ColumnNames}", columnNames.ToString())
                .Replace("{PrimaryKey}", primaryKey)
                .Replace("{PrimaryKeyParameter}", $"@{primaryKey} {primaryKeyParameter.DataType}");

            return procedure;
        }


        public async Task<CRUDOperationResult> CreateProceduresforTableAsync(string tableName)
        {
            if (operationResults.ContainsKey(tableName))
            {
                return operationResults[tableName];
            }


            var operationResult = new CRUDOperationResult
            {
                OperationId = Guid.NewGuid(),
                TableName = tableName,
                QuerysString = new Dictionary<string, string>(),
            };

            try
            {
                // Generar y guardar el procedimiento INSERT
                string insertProcedure = await GenerateInsertProcedureAsync(tableName);
                operationResult.QuerysString.Add("Insert", insertProcedure);
                operationResult.Messages.Add("Insert", "Procedimiento INSERT generado con éxito.");

                string updateProcedure = await GenerateUpdateProcedureAsync(tableName);
                operationResult.QuerysString.Add("Update", updateProcedure);
                operationResult.Messages.Add("Update", "Procedimiento UPDATE generado con éxito.");

                string deleteProcedure = await GenerateDeleteProcedureAsync(tableName);
                operationResult.QuerysString.Add("Delete", deleteProcedure);
                operationResult.Messages.Add("Delete", "Procedimiento DELETE generado con éxito.");


                string selectAllProcedure = await GenerateSelectAllProcedureAsync(tableName);
                operationResult.QuerysString.Add("SelectAll", selectAllProcedure);
                operationResult.Messages.Add("SelectAll", "Procedimiento SELECT ALL generado con éxito.");

                string selectByIdProcedure = await GenerateSelectByIdProcedureAsync(tableName);
                operationResult.QuerysString.Add("SelectById", selectByIdProcedure);
                operationResult.Messages.Add("SelectById", "Procedimiento SELECT BY ID generado con éxito.");

                operationResults.Add(tableName, operationResult);
            }
            catch (Exception ex)
            {
                operationResult.Messages.Add("Error", $"Error al generar procedimientos: {ex.Message}");
            }

            return operationResult;
        }

        public CRUDOperationResult GetOperationResult(string operationId)
        {
            if (operationResults.TryGetValue(operationId, out CRUDOperationResult result))
            {
                return result;
            }
            return null; // O maneja esto de otra manera si prefieres
        }

        public async Task<CustomQueryResult> ExecuteCustomQueryAsync(CustomQuery customQuery)
        {
            var result = new CustomQueryResult { QueryName = customQuery.QueryName };

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(customQuery.SqlText, connection))
                    {
                        // Añade los parámetros al comando SQL
                        foreach (var param in customQuery.Parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }

                        // Ejecutar la consulta. Aquí puedes usar ExecuteNonQueryAsync, ExecuteReaderAsync, etc., según la naturaleza de la consulta.
                        await command.ExecuteNonQueryAsync();
                    }
                }

                result.IsSuccessful = true;
                result.Message = "La consulta personalizada se ejecutó con éxito.";
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.Message = $"Error al ejecutar la consulta personalizada: {ex.Message}";
            }

            return result;
        }

        public void ExecuteScriptWithGO(string script)
        {
            // Crea la conexión al servidor
            var serverConnection = new ServerConnection
            {
                ConnectionString = connectionString
            };

            // Crea una instancia de Server con la conexión
            var server = new Server(serverConnection);

            try
            {
                // Ejecuta el script que contiene 'GO'
                server.ConnectionContext.ExecuteNonQuery(script);
                Console.WriteLine("Script ejecutado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al ejecutar el script: {ex.Message}");
            }
            finally
            {
                // Asegúrate de desconectar la conexión al servidor
                serverConnection.Disconnect();
            }
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