using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
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
        public bool TableExists()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT name FROM sqlite_master WHERE type='table' AND name='tecnicos';";
                using (var command = new SqliteCommand(query, connection))
                {
                    var result = command.ExecuteScalar();
                    return result != null;
                }
            }
        }

        public void CreateTable()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                CREATE TABLE IF NOT EXISTS tecnicos (
                    idtecnico INTEGER PRIMARY KEY,
                    nombres TEXT NOT NULL,
                    apellidos TEXT NOT NULL,
                    email TEXT NOT NULL,
                    telefono TEXT NOT NULL,
                    disponibilidad TEXT
                );
                ";
                command.ExecuteNonQuery();
            }
        }

        public void AddTecnico(Tecnico tecnico)
        {
            try
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                                                INSERT INTO tecnicos (nombres, apellidos, email, telefono, disponibilidad) 
                                                VALUES ($nombres, $apellidos, $email, $telefono, $disponibilidad);
                                                ";

                        // Añade los parámetros al comando SQL
                        command.Parameters.AddWithValue("$nombres", tecnico.Nombres ?? string.Empty);
                        command.Parameters.AddWithValue("$apellidos", tecnico.Apellidos ?? string.Empty);
                        command.Parameters.AddWithValue("$email", tecnico.Email ?? string.Empty);
                        command.Parameters.AddWithValue("$telefono", tecnico.Telefono ?? string.Empty);
                        command.Parameters.AddWithValue("$disponibilidad", tecnico.DisponibilidadJson ?? "{}"); // Asegura un valor por defecto de JSON vacío

                        // Ejecuta el comando
                        int affectedRows = command.ExecuteNonQuery();
                        if (affectedRows == 0)
                        {
                            // Manejar el caso en que no se inserte ningún registro, si es necesario
                            Console.WriteLine("No se pudo insertar el técnico.");
                        }
                    }
                }
            }
            catch (SqliteException ex)
            {
                // Maneja el error, por ejemplo, registrando el error en un archivo de log
                Console.WriteLine($"Ocurrió un error al intentar añadir un técnico: {ex.Message}");
            }
        }

        public List<Tecnico> GetTecnicos()
        {
            var tecnicos = new List<Tecnico>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM tecnicos;";
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    tecnicos.Add(new Tecnico
                    {
                        IdTecnico = reader.GetInt32(reader.GetOrdinal("idtecnico")),
                        Nombres = reader.GetString(reader.GetOrdinal("nombres")),
                        Apellidos = reader.GetString(reader.GetOrdinal("apellidos")),
                        Email = reader.GetString(reader.GetOrdinal("email")),
                        Telefono = reader.GetString(reader.GetOrdinal("telefono")),
                        DisponibilidadJson = reader.IsDBNull(reader.GetOrdinal("disponibilidad")) ? null : reader.GetString(reader.GetOrdinal("disponibilidad")),
                    });
                }
            }

            return tecnicos;
        }

        public Tecnico GetTecnico(int idTecnico)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM tecnicos WHERE idtecnico = $idtecnico;";
                command.Parameters.AddWithValue("$idtecnico", idTecnico);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Tecnico
                        {
                            IdTecnico = reader.GetInt32(reader.GetOrdinal("idtecnico")),
                            Nombres = reader.GetString(reader.GetOrdinal("nombres")),
                            Apellidos = reader.GetString(reader.GetOrdinal("apellidos")),
                            Email = reader.GetString(reader.GetOrdinal("email")),
                            Telefono = reader.GetString(reader.GetOrdinal("telefono")),
                            DisponibilidadJson = reader.IsDBNull(reader.GetOrdinal("disponibilidad")) ? null : reader.GetString(reader.GetOrdinal("disponibilidad")),
                        };
                    }
                }
            }
            return null; // Retorna null si no se encuentra el técnico
        }

        public void UpdateTecnico(Tecnico tecnico)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                                        UPDATE tecnicos 
                                        SET nombres = $nombres, apellidos = $apellidos, email = $email, telefono = $telefono, disponibilidad = $disponibilidad
                                        WHERE idtecnico = $idtecnico;
                                        ";
                command.Parameters.AddWithValue("$idtecnico", tecnico.IdTecnico);
                command.Parameters.AddWithValue("$nombres", tecnico.Nombres);
                command.Parameters.AddWithValue("$apellidos", tecnico.Apellidos);
                command.Parameters.AddWithValue("$email", tecnico.Email);
                command.Parameters.AddWithValue("$telefono", tecnico.Telefono);
                command.Parameters.AddWithValue("$disponibilidad", tecnico.DisponibilidadJson);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteTecnico(int idTecnico)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM tecnicos WHERE idtecnico = $idtecnico;";
                command.Parameters.AddWithValue("$idtecnico", idTecnico);

                command.ExecuteNonQuery();
            }
        }

    }
}
