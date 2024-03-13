using FugasDetectionSystem.Domain.Entities;
using FugasDetectionSystem.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FugasDetectionSystem.Infrastructure.repositories
{
    public class DisponibilidadTecnicoRepository : IDisponibilidadTecnicoRepository
    {
        private readonly string _connectionString;

        public DisponibilidadTecnicoRepository(IDatabaseSettings databaseSettings)
        {
            _connectionString = databaseSettings.ConnectionString;
        }

        public List<DisponibilidadTecnico> GetDisponibilidadesTecnico()
        {
            var disponibilidadesTecnico = new List<DisponibilidadTecnico>();

            try
            {
                using var connection = new SqlConnection(_connectionString);

                connection.Open();

                using var command = new SqlCommand("ConsultarDisponibilidadesTecnico", connection);

                // Especifica que el tipo de comando es un procedimiento almacenado.
                command.CommandType = CommandType.StoredProcedure;

                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var disponibilidadTecnico = new DisponibilidadTecnico
                    {
                        DisponibilidadID = reader.GetInt32(reader.GetOrdinal("DisponibilidadID")),
                        TecnicoID = reader.GetInt32(reader.GetOrdinal("TecnicoID")),
                        Fecha = reader.IsDBNull(reader.GetOrdinal("Fecha")) ? string.Empty : reader.GetString(reader.GetOrdinal("Fecha")),
                        HoraInicio = reader.IsDBNull(reader.GetOrdinal("HoraInicio")) ? string.Empty : reader.GetString(reader.GetOrdinal("HoraInicio")),
                        HoraFin = reader.IsDBNull(reader.GetOrdinal("HoraFin")) ? string.Empty : reader.GetString(reader.GetOrdinal("HoraFin"))
                    };

                    disponibilidadesTecnico.Add(disponibilidadTecnico);
                }
            }
            catch (SqlException ex)
            {
                // Maneja la excepción de SQL aquí.
                Console.WriteLine($"Ocurrió un error de SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Maneja cualquier otro tipo de excepción aquí.
                Console.WriteLine($"Ocurrió un error inesperado: {ex.Message}");
            }

            return disponibilidadesTecnico;
        }

        public DisponibilidadTecnico GetDisponibilidadTecnico(int disponibilidadTecnicoId)
        {
            DisponibilidadTecnico disponibilidadTecnico = new();

            try
            {
                using var connection = new SqlConnection(_connectionString);

                connection.Open();

                using var command = new SqlCommand("ConsultarDisponibilidadTecnicoPorID", connection);

                // Especifica que el tipo de comando es un procedimiento almacenado.
                command.CommandType = CommandType.StoredProcedure;

                // Añade los parámetros esperados por el procedimiento almacenado.
                command.Parameters.AddWithValue("@DisponibilidadID", disponibilidadTecnicoId);

                using var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    disponibilidadTecnico = new DisponibilidadTecnico
                    {
                        DisponibilidadID = reader.GetInt32(reader.GetOrdinal("DisponibilidadID")),
                        TecnicoID = reader.GetInt32(reader.GetOrdinal("TecnicoID")),
                        Fecha = reader.IsDBNull(reader.GetOrdinal("Fecha")) ? string.Empty : reader.GetString(reader.GetOrdinal("Fecha")),
                        HoraInicio = reader.IsDBNull(reader.GetOrdinal("HoraInicio")) ? string.Empty : reader.GetString(reader.GetOrdinal("HoraInicio")),
                        HoraFin = reader.IsDBNull(reader.GetOrdinal("HoraFin")) ? string.Empty : reader.GetString(reader.GetOrdinal("HoraFin"))
                    };
                }
            }
            catch (SqlException ex)
            {
                // Maneja la excepción de SQL aquí.
                Console.WriteLine($"Ocurrió un error de SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Maneja cualquier otro tipo de excepción aquí.
                Console.WriteLine($"Ocurrió un error inesperado: {ex.Message}");
            }

            return disponibilidadTecnico;
        }

        public void AddDisponibilidadTecnico(DisponibilidadTecnico disponibilidadTecnico)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);

                connection.Open();

                using var command = new SqlCommand("InsertarDisponibilidadTecnico", connection);

                // Especifica que el tipo de comando es un procedimiento almacenado.
                command.CommandType = CommandType.StoredProcedure;

                // Añade los parámetros esperados por el procedimiento almacenado.
                command.Parameters.AddWithValue("@TecnicoID", disponibilidadTecnico.TecnicoID);
                command.Parameters.AddWithValue("@Fecha", disponibilidadTecnico.Fecha ?? string.Empty);
                command.Parameters.AddWithValue("@HoraInicio", disponibilidadTecnico.HoraInicio ?? string.Empty);
                command.Parameters.AddWithValue("@HoraFin", disponibilidadTecnico.HoraFin ?? string.Empty);

                // Añade un parámetro para recibir el valor de retorno del procedimiento almacenado.
                var returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                command.ExecuteNonQuery();

                int result = (int)returnParameter.Value;
                if (result == 0)
                {
                    // Manejar el caso en el que no se pudo insertar el técnico.
                    Console.WriteLine("No se pudo insertar la disponibilidad.");
                }
                else
                {
                    Console.WriteLine("Disponibilidad insertada con éxito.");
                }

            }
            catch (SqlException ex)
            {
                // Maneja la excepción de SQL aquí.
                Console.WriteLine($"Ocurrió un error de SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Maneja cualquier otro tipo de excepción aquí.
                Console.WriteLine($"Ocurrió un error inesperado: {ex.Message}");
            }
        }

        public void UpdateDisponibilidadTecnico(DisponibilidadTecnico disponibilidadTecnico)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);

                connection.Open();

                using var command = new SqlCommand("ActualizarDisponibilidadTecnico", connection);

                // Especifica que el tipo de comando es un procedimiento almacenado.
                command.CommandType = CommandType.StoredProcedure;

                // Añade los parámetros esperados por el procedimiento almacenado.
                command.Parameters.AddWithValue("@DisponibilidadID", disponibilidadTecnico.DisponibilidadID);
                command.Parameters.AddWithValue("@TecnicoID", disponibilidadTecnico.TecnicoID);
                command.Parameters.AddWithValue("@Fecha", disponibilidadTecnico.Fecha ?? string.Empty);
                command.Parameters.AddWithValue("@HoraInicio", disponibilidadTecnico.HoraInicio ?? string.Empty);
                command.Parameters.AddWithValue("@HoraFin", disponibilidadTecnico.HoraFin ?? string.Empty);

                // Añade un parámetro para recibir el valor de retorno del procedimiento almacenado.
                var returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                command.ExecuteNonQuery();

                int result = (int)returnParameter.Value;
                if (result == 0)
                {
                    // Manejar el caso en el que no se actualice ningún registro, podría ser que el TecnicoID no exista.
                    Console.WriteLine("No se encontró la disponibilidad para actualizar.");
                }
                else
                {
                    Console.WriteLine("Disponibilidad actualizada con éxito.");
                }
            }
            catch (SqlException ex)
            {
                // Maneja la excepción de SQL aquí.
                Console.WriteLine($"Ocurrió un error de SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Maneja cualquier otro tipo de excepción aquí.
                Console.WriteLine($"Ocurrió un error inesperado: {ex.Message}");
            }
        }

        public void DeleteDisponibilidadTecnico(int disponibilidadTecnicoId)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);

                connection.Open();

                using var command = new SqlCommand("EliminarDisponibilidadTecnico", connection);

                // Especifica que el tipo de comando es un procedimiento almacenado.
                command.CommandType = CommandType.StoredProcedure;

                // Añade los parámetros esperados por el procedimiento almacenado.
                command.Parameters.AddWithValue("@DisponibilidadID", disponibilidadTecnicoId);

                // Añade un parámetro para recibir el valor de retorno del procedimiento almacenado.
                var returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                command.ExecuteNonQuery();

                int result = (int)returnParameter.Value;
                if (result == 0)
                {
                    // Manejar el caso en el que no se elimine ningún registro, podría ser que el TecnicoID no exista.
                    Console.WriteLine("No se encontró la disponibilidad para eliminar.");
                }
                else
                {
                    Console.WriteLine("Disponibilidad eliminada con éxito.");
                }
            }
            catch (SqlException ex)
            {
                // Maneja la excepción de SQL aquí.
                Console.WriteLine($"Ocurrió un error de SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Maneja cualquier otro tipo de excepción aquí.
                Console.WriteLine($"Ocurrió un error inesperado: {ex.Message}");
            }
        }
    }
}
