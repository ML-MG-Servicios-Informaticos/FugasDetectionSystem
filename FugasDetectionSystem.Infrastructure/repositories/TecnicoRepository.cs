using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using FugasDetectionSystem.Domain.Entities;
using FugasDetectionSystem.Domain.Interfaces;

namespace FugasDetectionSystem.Domain.Repositories
{
    public class TecnicoRepository : ITecnicoRepository
    {
        private readonly string _connectionString;

        public TecnicoRepository(IDatabaseSettings databaseSettings)
        {
            _connectionString = databaseSettings.ConnectionString;
        }

        public List<Tecnico> GetTecnicos()
        {
            var tecnicos = new List<Tecnico>();

            try
            {
                using var connection = new SqlConnection(_connectionString);

                connection.Open();

                using var command = new SqlCommand("ConsultarTecnicos", connection);

                // Especifica que el tipo de comando es un procedimiento almacenado.
                command.CommandType = CommandType.StoredProcedure;

                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var tecnico = new Tecnico
                    {
                        TecnicoID = reader.GetInt32(reader.GetOrdinal("TecnicoID")),
                        Nombre = reader.IsDBNull(reader.GetOrdinal("Nombre")) ? string.Empty : reader.GetString(reader.GetOrdinal("Nombre")),
                        Apellido = reader.IsDBNull(reader.GetOrdinal("Apellido")) ? string.Empty : reader.GetString(reader.GetOrdinal("Apellido")),
                        Especialidad = reader.IsDBNull(reader.GetOrdinal("Especialidad")) ? string.Empty : reader.GetString(reader.GetOrdinal("Especialidad")),
                        CorreoElectronico = reader.IsDBNull(reader.GetOrdinal("CorreoElectronico")) ? string.Empty : reader.GetString(reader.GetOrdinal("CorreoElectronico")),
                        Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? string.Empty : reader.GetString(reader.GetOrdinal("Telefono"))
                    };

                    tecnicos.Add(tecnico);
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

            return tecnicos;
        }

        public Tecnico GetTecnico(int tecnicoId)
        {
            Tecnico tecnico = new();

            try
            {
                using var connection = new SqlConnection(_connectionString);

                connection.Open();

                using var command = new SqlCommand("ConsultarTecnicoPorID", connection);

                // Especifica que el tipo de comando es un procedimiento almacenado.
                command.CommandType = CommandType.StoredProcedure;

                // Añade los parámetros esperados por el procedimiento almacenado.
                command.Parameters.AddWithValue("@TecnicoID", tecnicoId);

                using var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    tecnico = new Tecnico
                    {
                        TecnicoID = reader.GetInt32(reader.GetOrdinal("TecnicoID")),
                        Nombre = reader.IsDBNull(reader.GetOrdinal("Nombre")) ? string.Empty : reader.GetString(reader.GetOrdinal("Nombre")),
                        Apellido = reader.IsDBNull(reader.GetOrdinal("Apellido")) ? string.Empty : reader.GetString(reader.GetOrdinal("Apellido")),
                        Especialidad = reader.IsDBNull(reader.GetOrdinal("Especialidad")) ? string.Empty : reader.GetString(reader.GetOrdinal("Especialidad")),
                        CorreoElectronico = reader.IsDBNull(reader.GetOrdinal("CorreoElectronico")) ? string.Empty : reader.GetString(reader.GetOrdinal("CorreoElectronico")),
                        Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? string.Empty : reader.GetString(reader.GetOrdinal("Telefono"))
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

            return tecnico;
        }

        public void AddTecnico(Tecnico tecnico)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);

                connection.Open();

                using var command = new SqlCommand("InsertarTecnico", connection);

                // Especifica que el tipo de comando es un procedimiento almacenado.
                command.CommandType = CommandType.StoredProcedure;

                // Añade los parámetros esperados por el procedimiento almacenado.
                command.Parameters.AddWithValue("@Nombre", tecnico.Nombre ?? string.Empty);
                command.Parameters.AddWithValue("@Apellido", tecnico.Apellido ?? string.Empty);
                command.Parameters.AddWithValue("@Especialidad", tecnico.Especialidad ?? string.Empty);
                command.Parameters.AddWithValue("@CorreoElectronico", tecnico.CorreoElectronico ?? string.Empty);
                command.Parameters.AddWithValue("@Telefono", tecnico.Telefono ?? string.Empty);

                // Añade un parámetro para recibir el valor de retorno del procedimiento almacenado.
                var returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                command.ExecuteNonQuery();

                int result = (int)returnParameter.Value;
                if (result == 0)
                {
                    // Manejar el caso en el que no se pudo insertar el técnico.
                    Console.WriteLine("No se pudo insertar el técnico.");
                }
                else
                {
                    Console.WriteLine("Técnico insertado con éxito.");
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

        public void UpdateTecnico(Tecnico tecnico)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);

                connection.Open();

                using var command = new SqlCommand("ActualizarTecnico", connection);

                // Especifica que el tipo de comando es un procedimiento almacenado.
                command.CommandType = CommandType.StoredProcedure;

                // Añade los parámetros esperados por el procedimiento almacenado.
                command.Parameters.AddWithValue("@TecnicoID", tecnico.TecnicoID);
                command.Parameters.AddWithValue("@Nombre", tecnico.Nombre ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Apellido", tecnico.Apellido ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Especialidad", tecnico.Especialidad ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CorreoElectronico", tecnico.CorreoElectronico ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Telefono", tecnico.Telefono ?? (object)DBNull.Value);

                // Añade un parámetro para recibir el valor de retorno del procedimiento almacenado.
                var returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                command.ExecuteNonQuery();

                int result = (int)returnParameter.Value;
                if (result == 0)
                {
                    // Manejar el caso en el que no se actualice ningún registro, podría ser que el TecnicoID no exista.
                    Console.WriteLine("No se encontró el técnico para actualizar.");
                }
                else
                {
                    Console.WriteLine("Técnico actualizado con éxito.");
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

        public void DeleteTecnico(int tecnicoId)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);

                connection.Open();

                using var command = new SqlCommand("EliminarTecnico", connection);

                // Especifica que el tipo de comando es un procedimiento almacenado.
                command.CommandType = CommandType.StoredProcedure;

                // Añade los parámetros esperados por el procedimiento almacenado.
                command.Parameters.AddWithValue("@TecnicoID", tecnicoId);

                // Añade un parámetro para recibir el valor de retorno del procedimiento almacenado.
                var returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                command.ExecuteNonQuery();

                int result = (int)returnParameter.Value;
                if (result == 0)
                {
                    // Manejar el caso en el que no se elimine ningún registro, podría ser que el TecnicoID no exista.
                    Console.WriteLine("No se encontró el técnico para eliminar.");
                }
                else
                {
                    Console.WriteLine("Técnico eliminado con éxito.");
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
