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

        public void AddTecnico(Tecnico tecnico)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"
                            INSERT INTO Tecnicos (Nombre, Apellido, Especialidad, CorreoElectronico, Telefono) 
                            VALUES (@Nombre, @Apellido, @Especialidad, @CorreoElectronico, @Telefono);
                        ";

                        command.Parameters.AddWithValue("@Nombre", tecnico.Nombre ?? string.Empty);
                        command.Parameters.AddWithValue("@Apellido", tecnico.Apellido ?? string.Empty);
                        command.Parameters.AddWithValue("@Especialidad", tecnico.Especialidad ?? string.Empty);
                        command.Parameters.AddWithValue("@CorreoElectronico", tecnico.CorreoElectronico ?? string.Empty);
                        command.Parameters.AddWithValue("@Telefono", tecnico.Telefono ?? string.Empty);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Ocurrió un error al intentar añadir un técnico: {ex.Message}");
            }
        }

        public List<Tecnico> GetTecnicos()
        {
            var tecnicos = new List<Tecnico>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT TecnicoID, Nombre, Apellido, Especialidad, CorreoElectronico, Telefono FROM Tecnicos;";

                using (var reader = command.ExecuteReader())
                {
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
            }

            return tecnicos;
        }

        public Tecnico GetTecnico(int tecnicoId)
        {
            Tecnico tecnico = new Tecnico();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT TecnicoID, Nombre, Apellido, Especialidad, CorreoElectronico, Telefono FROM Tecnicos WHERE TecnicoID = @TecnicoID;";
                command.Parameters.AddWithValue("@TecnicoID", tecnicoId);

                using (var reader = command.ExecuteReader())
                {
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
                        return tecnico;
                    }
                }
            }

            return tecnico;
        }

        public void UpdateTecnico(Tecnico tecnico)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                                        UPDATE Tecnicos
                                        SET Nombre = @Nombre, 
                                            Apellido = @Apellido, 
                                            Especialidad = @Especialidad, 
                                            CorreoElectronico = @CorreoElectronico, 
                                            Telefono = @Telefono
                                        WHERE TecnicoID = @TecnicoID;
                                    ";

                command.Parameters.AddWithValue("@TecnicoID", tecnico.TecnicoID);
                command.Parameters.AddWithValue("@Nombre", tecnico.Nombre ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Apellido", tecnico.Apellido ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Especialidad", tecnico.Especialidad ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CorreoElectronico", tecnico.CorreoElectronico ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Telefono", tecnico.Telefono ?? (object)DBNull.Value);

                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    // Opcional: Manejar el caso en el que no se actualice ningún registro, podría ser que el TecnicoID no exista.
                    Console.WriteLine("No se encontró el técnico para actualizar.");
                }
            }
        }

        public void DeleteTecnico(int tecnicoId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Tecnicos WHERE TecnicoID = @TecnicoID;";

                command.Parameters.AddWithValue("@TecnicoID", tecnicoId);

                int affectedRows = command.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    // Opcional: Manejar el caso en el que no se elimine ningún registro,
                    // podría ser que el TecnicoID no exista.
                    Console.WriteLine("No se encontró el técnico para eliminar.");
                }
            }
        }

        // Los métodos GetTecnicos, GetTecnico, UpdateTecnico, y DeleteTecnico también necesitarán ser ajustados
        // para reflejar el esquema de la tabla y las columnas correctas.
        // Asegúrate de modificar las consultas y parámetros según la definición de tu tabla `Tecnicos`.
    }
}
