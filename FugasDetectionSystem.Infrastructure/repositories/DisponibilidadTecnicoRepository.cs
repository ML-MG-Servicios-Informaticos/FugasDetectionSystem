using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using FugasDetectionSystem.Domain.Entities;
using FugasDetectionSystem.Domain.Interfaces;
using FugasDetectionSystem.Infrastructure.Exceptions;
using FugasDetectionSystem.Infrastructure.repositories;

namespace FugasDetectionSystem.Infrastructure.Repositories
{
    public class DisponibilidadTecnicoRepository : BaseRepository, IDisponibilidadTecnicoRepository
    {
        public DisponibilidadTecnicoRepository(IDatabaseSettings databaseSettings) : base(databaseSettings)
        {
        }

        private static DisponibilidadTecnico MapToDisponibilidadTecnico(SqlDataReader reader)
        {
            return new DisponibilidadTecnico
            {
                DisponibilidadID = reader.GetInt32(reader.GetOrdinal("DisponibilidadID")),
                TecnicoID = reader.GetInt32(reader.GetOrdinal("TecnicoID")),
                Fecha = reader.IsDBNull(reader.GetOrdinal("Fecha")) ? string.Empty : reader.GetString(reader.GetOrdinal("Fecha")),
                HoraInicio = reader.IsDBNull(reader.GetOrdinal("HoraInicio")) ? string.Empty : reader.GetString(reader.GetOrdinal("HoraInicio")),
                HoraFin = reader.IsDBNull(reader.GetOrdinal("HoraFin")) ? string.Empty : reader.GetString(reader.GetOrdinal("HoraFin")),
            };
        }

        public async Task<IEnumerable<DisponibilidadTecnico>> GetAllAsync()
        {
            var disponibilidades = new List<DisponibilidadTecnico>();

            try
            {
                using var connection = GetOpenConnection();
                using var command = CreateCommand(connection, "ConsultarDisponibilidadesTecnico");
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    disponibilidades.Add(MapToDisponibilidadTecnico(reader));
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return disponibilidades;
        }

        public async Task<DisponibilidadTecnico> GetByIdAsync(int disponibilidadTecnicoId)
        {
            DisponibilidadTecnico disponibilidadTecnico = null;

            try
            {
                using var connection = GetOpenConnection();
                using var command = CreateCommand(connection, "ConsultarDisponibilidadTecnicoPorID");
                command.Parameters.AddWithValue("@DisponibilidadID", disponibilidadTecnicoId);

                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    disponibilidadTecnico = MapToDisponibilidadTecnico(reader);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return disponibilidadTecnico;
        }

        public async Task AddAsync(DisponibilidadTecnico disponibilidadTecnico)
        {
            try
            {
                using var connection = GetOpenConnection();
                using var command = CreateCommand(connection, "InsertarDisponibilidadTecnico");
                command.Parameters.AddWithValue("@TecnicoID", disponibilidadTecnico.TecnicoID);
                command.Parameters.AddWithValue("@Fecha", disponibilidadTecnico.Fecha ?? string.Empty);
                command.Parameters.AddWithValue("@HoraInicio", disponibilidadTecnico.HoraInicio ?? string.Empty);
                command.Parameters.AddWithValue("@HoraFin", disponibilidadTecnico.HoraFin ?? string.Empty);

                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        public async Task UpdateAsync(DisponibilidadTecnico disponibilidadTecnico)
        {
            try
            {
                using var connection = GetOpenConnection();
                using var command = CreateCommand(connection, "ActualizarDisponibilidadTecnico");
                command.Parameters.AddWithValue("@DisponibilidadID", disponibilidadTecnico.DisponibilidadID);
                command.Parameters.AddWithValue("@TecnicoID", disponibilidadTecnico.TecnicoID);
                command.Parameters.AddWithValue("@Fecha", disponibilidadTecnico.Fecha ?? string.Empty);
                command.Parameters.AddWithValue("@HoraInicio", disponibilidadTecnico.HoraInicio ?? string.Empty);
                command.Parameters.AddWithValue("@HoraFin", disponibilidadTecnico.HoraFin ?? string.Empty);

                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        public async Task DeleteAsync(int disponibilidadTecnicoId)
        {
            try
            {
                using var connection = GetOpenConnection();
                using var command = CreateCommand(connection, "EliminarDisponibilidadTecnico");
                command.Parameters.AddWithValue("@DisponibilidadID", disponibilidadTecnicoId);

                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
