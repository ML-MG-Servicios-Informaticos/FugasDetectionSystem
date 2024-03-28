using FugasDetectionSystem.Domain.Interfaces;
using FugasDetectionSystem.Infrastructure.Data;
using FugasDetectionSystem.Infrastructure.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace FugasDetectionSystem.Infrastructure.repositories
{
    public abstract class BaseRepository
    {
        protected string ConnectionString { get; private set; }

        protected BaseRepository(IDatabaseSettings databaseSettings)
        {
            ConnectionString = databaseSettings.ConnectionString;
        }

        protected SqlConnection GetOpenConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        protected static SqlCommand CreateCommand(SqlConnection connection, string storedProcedure, CommandType commandType = CommandType.StoredProcedure)
        {
            var command = new SqlCommand(storedProcedure, connection)
            {
                CommandType = commandType
            };
            return command;
        }

        protected static void HandleException(Exception exception)
        {
            if (exception is SqlException sqlEx)
            {
                // Manejo específico para SqlException
                LogError(sqlEx);
                throw new RepositoryException("Ocurrió un error específico de SQL.", sqlEx.Number, sqlEx);
            }
            else
            {
                // Manejo general para otras excepciones
                LogError(exception);
                throw new RepositoryException("Ocurrió un error inesperado en el repositorio.", exception);
            }
        }

        private static void LogError(Exception exception)
        {
            // Implementa tu lógica de registro aquí. Ejemplo usando un sistema de registro:
            // Log.Error(exception, "Mensaje de error con contexto adicional");
            Console.WriteLine(exception.ToString()); // Sustituye esto por tu mecanismo de registro real.
        }
    }

}


