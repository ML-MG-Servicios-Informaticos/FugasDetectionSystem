using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using FugasDetectionSystem.Domain.Entities;
using FugasDetectionSystem.Domain.Interfaces;
using FugasDetectionSystem.Infrastructure.repositories;
using FugasDetectionSystem.Infrastructure.Exceptions;

namespace FugasDetectionSystem.Domain.Repositories
{
    public class TecnicoRepository(IDatabaseSettings databaseSettings) : BaseRepository(databaseSettings), ITecnicoRepository
    {
        private static Tecnico MapToTecnico(SqlDataReader reader)
        {
            return new Tecnico
            {
                TecnicoID = reader.GetInt32(reader.GetOrdinal("TecnicoID")),
                Nombre = reader.IsDBNull(reader.GetOrdinal("Nombre")) ? string.Empty : reader.GetString(reader.GetOrdinal("Nombre")),
                Apellido = reader.IsDBNull(reader.GetOrdinal("Apellido")) ? string.Empty : reader.GetString(reader.GetOrdinal("Apellido")),
                Especialidad = reader.IsDBNull(reader.GetOrdinal("Especialidad")) ? string.Empty : reader.GetString(reader.GetOrdinal("Especialidad")),
                CorreoElectronico = reader.IsDBNull(reader.GetOrdinal("CorreoElectronico")) ? string.Empty : reader.GetString(reader.GetOrdinal("CorreoElectronico")),
                Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? string.Empty : reader.GetString(reader.GetOrdinal("Telefono"))
            };
        }

        public async Task<IEnumerable<Tecnico>> GetAllAsync()
        {
            var tecnicos = new List<Tecnico>();

            try
            {
                using var connection = GetOpenConnection();
                using var command = CreateCommand(connection, "ConsultarTecnicos");
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    tecnicos.Add(MapToTecnico(reader));
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return tecnicos;
        }

        public async Task<Tecnico> GetByIdAsync(int tecnicoId)
        {
            Tecnico tecnico = null;

            try
            {
                using var connection = GetOpenConnection();
                using var command = CreateCommand(connection, "ConsultarTecnicoPorID");
                command.Parameters.AddWithValue("@TecnicoID", tecnicoId);

                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    tecnico = MapToTecnico(reader);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return tecnico;
        }

        public async Task AddAsync(Tecnico tecnico)
        {
            try
            {
                using var connection = GetOpenConnection();
                using var command = CreateCommand(connection, "InsertarTecnico");

                // Añade los parámetros esperados por el procedimiento almacenado.
                command.Parameters.AddWithValue("@Nombre", tecnico.Nombre ?? string.Empty);
                command.Parameters.AddWithValue("@Apellido", tecnico.Apellido ?? string.Empty);
                command.Parameters.AddWithValue("@Especialidad", tecnico.Especialidad ?? string.Empty);
                command.Parameters.AddWithValue("@CorreoElectronico", tecnico.CorreoElectronico ?? string.Empty);
                command.Parameters.AddWithValue("@Telefono", tecnico.Telefono ?? string.Empty);

                var returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                await command.ExecuteNonQueryAsync();

                int result = (int)returnParameter.Value;
                // Manejar el resultado como sea necesario
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        public async Task UpdateAsync(Tecnico tecnico)
        {
            try
            {
                using var connection = GetOpenConnection();
                using var command = CreateCommand(connection, "ActualizarTecnico");

                // Especifica que el tipo de comando es un procedimiento almacenado y añade los parámetros.
                command.Parameters.AddWithValue("@TecnicoID", tecnico.TecnicoID);
                command.Parameters.AddWithValue("@Nombre", tecnico.Nombre);
                command.Parameters.AddWithValue("@Apellido", tecnico.Apellido );
                command.Parameters.AddWithValue("@Especialidad", tecnico.Especialidad);
                command.Parameters.AddWithValue("@CorreoElectronico", tecnico.CorreoElectronico);
                command.Parameters.AddWithValue("@Telefono", tecnico.Telefono);

                var returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                await command.ExecuteNonQueryAsync();

                int result = (int)returnParameter.Value;
                // Manejar el resultado como sea necesario
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        public async Task DeleteAsync(int tecnicoId)
        {
            try
            {
                using var connection = GetOpenConnection();
                using var command = CreateCommand(connection, "EliminarTecnico");

                command.Parameters.AddWithValue("@TecnicoID", tecnicoId);

                var returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                await command.ExecuteNonQueryAsync();

                int result = (int)returnParameter.Value;
                // Manejar el resultado como sea necesario
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
